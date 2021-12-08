using System;
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
    private float[] mf_EmotionScores = new float[7];
    private string[] ms_EmotionNames = {"Angry", "Disgust", "Fear", "Happy", "Sad", "Surprised", "Neutral"};
    
    private float[] mf_Threshold = { 0.5f, 0.05f, 0.2f, 0.6f, 0.5f, 0.5f, 0.7f };
    private bool mb_Connected = false;
    
    public enum eEmotion
    {
        ANGRY, DISGUST, FEAR, HAPPY, SAD, SUPRIRSE, NEUTRAL
    }
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Server");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {
        m_ServerThread = new Thread(new ThreadStart(ListenRequests));
        m_ServerThread.IsBackground = true;
        m_ServerThread.Start();
    }

    void OnDestroy()
    {
        try
        {
            m_ServerThread.Abort();
            m_Server.Stop();
        }
        catch(Exception e)
        {
            Debug.Log("PyServer OnDestroy(): " + e.ToString());
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
                        mb_Connected = false;
                        while((length = ns.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
							Array.Copy(bytes, 0, incomingData, 0, length);
							
                            // Convert byte array to float
                            for(int i = 0; i < length / 4; ++i)
                            {
                                string strFloat = Encoding.ASCII.GetString(incomingData, i * 4, 4);
                                mf_EmotionScores[i] = float.Parse(strFloat);
                                // Debug.Log(mf_EmotionScores[i]);
                            }

                            mb_Connected = true;
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

    void Update()
    {
        
    }

    public float GetScore(int idx) => mf_EmotionScores[idx];
    public float GetThreshold(int idx) => mf_Threshold[idx];
    public string GetName(int idx) => ms_EmotionNames[idx];
    public bool GetConnected() => mb_Connected;
}
