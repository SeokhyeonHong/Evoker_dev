using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PyServer : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;
    private TcpClient connectedClient;
    private float[] emotionScores;
    private string[] emotionNames;
    public bool bConnected = false;
    
    public enum eEmotion
    {
        ANGRY, DISGUST, FEAR, HAPPY, SAD, SUPRIRSE, NEUTRAL
    }
    public void Start()
    {
        emotionScores = new float[7];
        emotionNames = new string[] {"angry", "disgust", "fear", "happy", "sad", "surprise", "neutral"};
        serverThread = new Thread(new ThreadStart(ListenRequests));
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    public void OnDestroy()
    {
        try {
            serverThread.Abort();
            server.Stop();
        }
        catch(Exception e) {
            Debug.Log(e.ToString());
        }
    }

    void ListenRequests()
    {
        try {
            int port = 50003;
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            server.Start();
            Debug.Log("Server is Listening!");

            Byte[] bytes = new Byte[1024];
            while(true) {
                using(connectedClient = server.AcceptTcpClient()) {
                    using(NetworkStream ns = connectedClient.GetStream()) {
                        int length;
                        bConnected = false;
                        while((length = ns.Read(bytes, 0, bytes.Length)) != 0) {
                            var incomingData = new byte[length];
							Array.Copy(bytes, 0, incomingData, 0, length);
							
                            // Convert byte array to float
                            for(int i = 0; i < length / 4; ++i) {
                                string strFloat = Encoding.ASCII.GetString(incomingData, i * 4, 4);
                                emotionScores[i] = float.Parse(strFloat);
                            }

                            bConnected = true;
                        }
                    }
                }
            }
        }
        catch(SocketException e) {
            Debug.Log("SocketException: " + e.ToString());
        }
        finally {
            server.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetScore(int idx) => emotionScores[idx];
    public string GetName(int idx) => emotionNames[idx];
}
