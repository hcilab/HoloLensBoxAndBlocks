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

public class MyoReaderClient : MonoBehaviour {


    private string host = "127.0.0.1";
    private int port = 12345;
    private float leftReading;
    private float rightReading;

    TcpClient socketClient;

    // Use this for initialization
    void Start () {
        ConnectSocket();
    }
	
	// Update is called once per frame
	void Update () {
        string returnedString = ListenForData();
        Debug.Log(returnedString);
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
        return dataString;
    }
}
