﻿using System.Collections;
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
#endif

    NetworkClient myClient;

    public Vector3 controllerPos;
    public Quaternion controllerQuat;
    public bool calibrated = false;

    public GameObject parentObject;

    private string host = "127.0.0.1";
    private string ipAddress = "131.202.243.56";
    private string portUWP = "5555";
    private bool connection = false;
    private int port = 5555;
    private Vector3 posOffset;
    private float yOffset;
    private Quaternion rotOffset;
    private StreamReader reader;
    private string returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";

#if UNITY_EDITOR
    TcpClient socketClient;
#endif

    void Start()
    {
#if !UNITY_EDITOR
        ConnectSocketUWP();
#else
        ConnectSocketUnity();
#endif
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (connection){
            ListenForDataUWP();
            //returnedString = ListenForDataUWP();
        }

        else {
            returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
        }
#else
        ListenForDataUnity();
        //returnedString = ListenForDataUnity();
#endif
        StringToCoordinates(returnedString);

        if (Input.GetKeyDown("space"))
        {
            AlignAxes();
        }

        if (calibrated)
        {
            transform.localPosition = controllerPos + posOffset;
            transform.localRotation = controllerQuat;
        }
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
#endif

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

#if UNITY_EDITOR
    void ListenForDataUnity()
    {
        int data;
        byte[] bytes = new byte[socketClient.ReceiveBufferSize];
        NetworkStream stream = socketClient.GetStream();
        data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
        //string dataString = Encoding.UTF8.GetString(bytes, 0, data);
        //return dataString;
        returnedString = Encoding.UTF8.GetString(bytes, 0, data);
    }
#endif

#if !UNITY_EDITOR
    private async void ListenForDataUWP()
    {
        try
        {
            returnedString = reader.ReadLine();
            //string dataString = reader.ReadLine();
            //return dataString;
        }

        catch (Exception e)
        {
            //return "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
            returnedString = "0.0000 0.0000 0.0000 0.0000 0.0000 0.0000 0.0000";
        }
    }
#endif

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

        controllerPos = new Vector3(xPos, yPos, -zPos);
        controllerQuat = new Quaternion();
        controllerQuat.Set(-xQuat, -yQuat, zQuat, wQuat);
    }

    public void AlignAxes() {
        if (!calibrated)
        {
            yOffset = controllerQuat.eulerAngles.y;
            parentObject.transform.Rotate(0, -yOffset, 0);

            rotOffset = Quaternion.Inverse(controllerQuat);

            transform.parent = parentObject.transform;
            posOffset = transform.localPosition - controllerPos;
            
            calibrated = true;
        }
    }
}
