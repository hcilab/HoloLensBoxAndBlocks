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

public class UnityClient : MonoBehaviour {

    NetworkClient myClient;

    public Vector3 controllerPos;
    public Quaternion controllerQuat;

    private string host = "127.0.0.1";
    private int port = 5555;
    
    TcpClient socketClient;

    void Start()
    {
        ConnectSocket();
    }

    private void Update()
    {
        string returnedString = ListenForData();
        StringToCoordinates(returnedString);

        transform.position = controllerPos;
        transform.rotation = controllerQuat;
    }


    void ConnectSocket()
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

    string ListenForData()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        string dataString = Encoding.UTF8.GetString(bytes, 0, data);
        //Debug.Log("data: " + dataString);
        return dataString;
    }

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

        //Debug.Log(xPosString + ' ' + yPosString + ' ' + zPosString + ' ' + wQuatString + ' ' + xQuatString + ' ' + yQuatString + ' ' + zQuatString);
        //Debug.Log(xPos.ToString());

        controllerPos = new Vector3(xPos, yPos, -zPos);
        controllerQuat = new Quaternion();
        controllerQuat.Set(-xQuat, -yQuat, zQuat, wQuat);
    }

}
