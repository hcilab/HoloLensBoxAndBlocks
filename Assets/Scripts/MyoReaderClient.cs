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
    private string returnedString = null;

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

    // Update is called once per frame
    void Update () {

#if !UNITY_EDITOR
        if (connection){
            returnedString = ListenForDataUWP();
        }

        else {
            returnedString = "0.0000,0.0000,0.0";
        }
#else
        returnedString = ListenForDataUnity();
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
    string ListenForDataUnity()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        string dataString = Encoding.UTF8.GetString(bytes, 0, data);
        return dataString;
    }
#else
    private string ListenForDataUWP()
    {
        try
        {
            string dataString = reader.ReadLine();
            return dataString;
        }

        catch (Exception e)
        {
            return "0.0000,0.0000,0.0";
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
