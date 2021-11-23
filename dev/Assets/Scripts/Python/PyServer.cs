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
    private TcpListener m_Server;
    private Thread m_ServerThread;
    private TcpClient m_ConnectedClient;
    private float[] mf_EmotionScores;
    private string[] ms_EmotionNames;
    public bool bConnected = false;
    
    public enum eEmotion
    {
        ANGRY, DISGUST, FEAR, HAPPY, SAD, SUPRIRSE, NEUTRAL
    }
    public void Start()
    {
        mf_EmotionScores = new float[7];
        ms_EmotionNames = new string[] {"Angry", "Disgust", "Fear", "Happy", "Sad", "Surprised", "Neutral"};
        m_ServerThread = new Thread(new ThreadStart(ListenRequests));
        m_ServerThread.IsBackground = true;
        m_ServerThread.Start();
    }

    public void OnDestroy()
    {
        try
        {
            m_ServerThread.Abort();
            m_Server.Stop();
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void ListenRequests()
    {
        try
        {
            int port = 50003;
            m_Server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            m_Server.Start();
            Debug.Log("Server is Listening!");

            Byte[] bytes = new Byte[1024];
            while(true) {
                using(m_ConnectedClient = m_Server.AcceptTcpClient())
                {
                    using(NetworkStream ns = m_ConnectedClient.GetStream())
                    {
                        int length;
                        bConnected = false;
                        while((length = ns.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
							Array.Copy(bytes, 0, incomingData, 0, length);
							
                            // Convert byte array to float
                            for(int i = 0; i < length / 4; ++i)
                            {
                                string strFloat = Encoding.ASCII.GetString(incomingData, i * 4, 4);
                                mf_EmotionScores[i] = float.Parse(strFloat);
                            }

                            bConnected = true;
                        }
                    }
                }
            }
        }
        catch(SocketException e)
        {
            Debug.Log("SocketException: " + e.ToString());
        }
        finally
        {
            m_Server.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetScore(int idx) => mf_EmotionScores[idx];
    public string GetName(int idx) => ms_EmotionNames[idx];
}
