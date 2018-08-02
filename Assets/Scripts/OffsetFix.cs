using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

#if !UNITY_EDITOR
using System.Threading.Tasks;
#endif

public class OffsetFix : MonoBehaviour
{
#if !UNITY_EDITOR
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task socketListenTask;
#endif

    public Vector3 controllerPos;
    public Quaternion controllerQuat;
    public bool calibrated = false;
    public GameObject parentObject;
    public GameObject boxAndBlocks;
    public GameObject TextManagerObject;

    TextManager textManager;
    NetworkClient myClient;

    private string host = "127.0.0.1";
    private string ipAddress = "131.202.243.56";
    private string portUWP = "5555";
    private string returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
    private bool connection = false;
    private int port = 5555;
    private float yOffset;
    private float yAxisOffset;
    private float yControllerOffset;
    private Vector3 posOffset;
    private Quaternion rotOffset;
    private StreamReader reader;

#if UNITY_EDITOR
    TcpClient socketClient;
#endif

    /// <summary>
    /// calls either method depending on if in Unity editor or as UWP app to Established a TCP socket connection.
    /// </summary>
    void Start()
    {
#if !UNITY_EDITOR
        ConnectSocketUWP();
#else
        ConnectSocketUnity();
#endif
        TextManagerObject = GameObject.Find("TextManager");
        textManager = TextManagerObject.GetComponent<TextManager>();

    }

    /// <summary>
    /// called at a fixed interval. If TCP socket connection has been made, a thread is created to read from the socket.
    /// read string from the TCP connection is sent to StringToCoordinates method.
    /// </summary>
    private void FixedUpdate()
    {
#if !UNITY_EDITOR
        if (connection)
        {
            if(socketListenTask == null || socketListenTask.IsCompleted)
            {
                socketListenTask = Task.Run(() => ListenForDataUWP());
            }
        }

        else 
        {
            returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
        }
#else
        ListenForDataUnity();
#endif
        StringToCoordinates(returnedString);

        if (calibrated)
        {
            transform.localPosition = controllerPos + posOffset;

            if (textManager.rightHand)
            {
                transform.localRotation = controllerQuat * Quaternion.Euler(0, 180, 0);
            }

            else
            {
                transform.localRotation = controllerQuat;
            }
        }
    }

    /// <summary>
    ///  checks to see if the spacebar is pressed. If it is, calls AlignAxes method and advanced the game state to ArmAligned.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AlignAxes();
            textManager.gameState = GameState.ArmAligned;
        }
    }

    /// <summary>
    /// Established a TCP socket connection to receive packets from another port.
    /// This is used when running from the Unity editor.
    /// </summary>
#if UNITY_EDITOR
    void ConnectSocketUnity()
    {
        IPAddress ipAddress = IPAddress.Parse(host);

        socketClient = new TcpClient();
        try
        {
            socketClient.Connect(ipAddress, port);
        }

        catch
        {
            Debug.Log("error when connecting to server socket");
        }
    }
#endif

    /// <summary>
    /// established a TCP socket connection to receive packets from another device.
    /// This is used when running as a UWP app on the HoloLens.
    /// </summary>
#if !UNITY_EDITOR
    private async void ConnectSocketUWP()
    {
        try
        {
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(ipAddress);
            await socket.ConnectAsync(serverHost, portUWP);

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn, Encoding.UTF8);
            connection = true;
        }

        catch (Exception e)
        {
            //do something
        }
    }
#endif

    /// <summary>
    /// Reads data from the TCP socket connection. This string is stored in the variable returnedString.
    /// </summary>
#if UNITY_EDITOR
    void ListenForDataUnity()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        returnedString = Encoding.UTF8.GetString(bytes, 0, data);
    }
#endif

    /// <summary>
    /// Reads data from the TCP socket connection. This string is stored in the variable returnedString. 
    /// </summary>
#if !UNITY_EDITOR
    private void ListenForDataUWP()
    {
        try
        {
            returnedString = reader.ReadLine();
        }

        catch (Exception e)
        {
            returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
        }
    }
#endif

    /// <summary>
    /// Splits inputString into strings that were seperated by a ' '. Assigns each split string to a new 
    /// string that represents the different readings from the Vive controller. 
    /// </summary>
    void StringToCoordinates(string inputString)
    {
        string[] splitCoords = inputString.Split(' ');

        string xPosString = splitCoords[0];
        string yPosString = splitCoords[1];
        string zPosString = splitCoords[2];
        string wQuatString = splitCoords[3];
        string xQuatString = splitCoords[4];
        string yQuatString = splitCoords[5];
        string zQuatString = splitCoords[6];

        float xPos = Single.Parse(xPosString);
        float yPos = Single.Parse(yPosString);
        float zPos = Single.Parse(zPosString);
        float wQuat = Single.Parse(wQuatString);
        float xQuat = Single.Parse(xQuatString);
        float yQuat = Single.Parse(yQuatString);
        float zQuat = Single.Parse(zQuatString);

        controllerPos = new Vector3(xPos, yPos,-zPos);
        controllerQuat = new Quaternion();
        controllerQuat.Set(-xQuat, -yQuat, zQuat, wQuat);
    }

    /// <summary>
    /// Aligns the axes of the empty game object parentObject (ViveAxes in game heirarchy) with the 
    /// axes of the base station and controller. Then makes the controller game object a child of that 
    /// empty game oject so that all motion and rotation in the real world is the same as in the game world.
    /// </summary>
    public void AlignAxes() {
        if (!calibrated)
        {
            yAxisOffset = controllerQuat.eulerAngles.y;
            yControllerOffset = transform.rotation.eulerAngles.y;

            if(transform.localScale.z == 1)
            {
                //is the right hand
                yOffset = (yControllerOffset - yAxisOffset) - 180;
            }

            else
            {
                //is the left hand
                yOffset = (yControllerOffset - yAxisOffset);
            }
            
            parentObject.transform.Rotate(0, yOffset, 0);
            rotOffset = Quaternion.Inverse(transform.rotation);
            transform.parent = parentObject.transform;
            posOffset = transform.localPosition - controllerPos;
            calibrated = true;
        }
    }
}
