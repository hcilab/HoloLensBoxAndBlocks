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
    private Task socketListenTask;
#endif

    public float leftReading;
    public float rightReading;
    public bool connection = false;

    private int port = 12345;
    private string host = "127.0.0.1";
    private string ipAddress = "131.202.243.56";
    private string portUWP = "12345";
    private string returnedString = "0.0000,0.0000,0.0";
    private string readString = null;
    private StreamReader reader;

#if UNITY_EDITOR
    TcpClient socketClient;
#endif

    // Use this for initialization
    void Start () {
#if !UNITY_EDITOR
        ConnectSocketUWP();
#else
        ConnectSocketUnity();
#endif
    }

    void FixedUpdate () {

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
            returnedString = "0.0000,0.0000,0.0";
        }
#else
        ListenForDataUnity();
#endif
        StringToFloats(returnedString);
    }

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
#else
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

#if UNITY_EDITOR
    void ListenForDataUnity()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        returnedString = Encoding.UTF8.GetString(bytes, 0, data);
    }
#else
    private void ListenForDataUWP()
    {
        try
        {
            returnedString = reader.ReadLineAsync();
            //string dataString = reader.ReadLine();
            //return dataString;
        }

        catch (Exception e)
        {
            returnedString = "0.0000,0.0000,0.0";
            //return "0.0000,0.0000,0.0";
        }
    }
#endif

    void StringToFloats(string inputString)
    {
        string[] splitReading = inputString.Split(',');

        string leftSide = splitReading[0];
        string rightSide = splitReading[1];

        leftReading = Single.Parse(leftSide);
        rightReading = Single.Parse(rightSide);
    }
}
