using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using System.Threading;

#if !UNITY_EDITOR
using System.Threading.Tasks;
#endif

public class ReadFromPythonServer : MonoBehaviour {

#if !UNITY_EDITOR
    private Windows.Networking.Sockets.StreamSocket socket;
#endif

    public Text controllerPositions;

    private string ipAddress = "131.202.243.56";
    private string port = "5555";
    private string dataFromServer = null;
    private string msg = null;

    private bool readyToReceive = false;
    private bool connection = false;

    private StreamReader reader;
    


    // Use this for initialization
    void Start () {
#if !UNITY_EDITOR
        UWPConnection();
#endif
    }

    // Update is called once per frame
    void Update () {
#if !UNITY_EDITOR
        if (connection == true){
            UWPListen();
        }
#endif
    }

#if !UNITY_EDITOR
    private async void UWPConnection()
    {
        try
        {
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(ipAddress);
            await socket.ConnectAsync(serverHost, port);

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn, Encoding.UTF8);
            connection = true;
        }

        catch (Exception e)
        {
            controllerPositions.text = "error";
        }
    }
#endif

#if !UNITY_EDITOR
    private void UWPListen()
    {
        try
        {
            dataFromServer = reader.ReadLine();
            if (dataFromServer == "start"){
                msg = "";
                while(true){
                    dataFromServer = reader.ReadLine();
                    if (dataFromServer == "end"){
                        controllerPositions.text = "data: " + msg;
                        break;
                    }
                    else {
                        msg = msg + dataFromServer;
                    }
                }
            }
            else if (dataFromServer == "end"){
                controllerPositions.text = "data: " + msg;
            }
            else{
                msg = msg + dataFromServer;
            }
        }

        catch (Exception e)
        {
            controllerPositions.text = "did not read, error: " + e.ToString();
        }
    }
#endif
}
