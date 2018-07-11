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

public class MyoReaderClient : MonoBehaviour {

#if !UNITY_EDITOR
    private Windows.Networking.Sockets.StreamSocket socket;
#endif

    private string host = "127.0.0.1";
    private string ipAddress = "131.202.243.56";
    private int port = 12345;
    private string portUWP = "12345";
    public float leftReading;
    public float rightReading;
    public bool connection = false;

    private StreamReader reader;
    TcpClient socketClient;

    // Use this for initialization
    void Start () {
#if !UNITY_EDITOR
        ConnectSocketUWP();
#else
        ConnectSocketUnity();
#endif
    }

    // Update is called once per frame
    void Update () {

#if !UNITY_EDITOR
        if (connection){
            string returnedString = ListenForDataUWP();
        }

        else {
            string returnedString = "0.0000 0.0000 0.0";
        }
#else
        string returnedString = ListenForDataUnity();
#endif
        StringToFloats(returnedString);
    }

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

    string ListenForDataUnity()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        string dataString = Encoding.UTF8.GetString(bytes, 0, data);
        return dataString;
    }

    void StringToFloats(string inputString)
    {
        string[] splitReading = inputString.Split(',');

        string leftSide = splitReading[0];
        string rightSide = splitReading[1];

        leftReading = Single.Parse(leftSide);
        rightReading = Single.Parse(rightSide);
    }
}
