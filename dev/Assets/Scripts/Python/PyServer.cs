using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PyServer : MonoBehaviour
{
    private TcpListener m_Server;
    private Thread m_ServerThread;
    private TcpClient m_ConnectedClient;
    private float[] mf_EmotionScores = new float[7];
    private string[] ms_EmotionNames = {"Angry", "Disgust", "Fear", "Happy", "Sad", "Surprised", "Neutral"};
    private string[] ms_FileNames = { "angry.csv", "disgust.csv", "fear.csv", "happy.csv", "sad.csv", "surprised.csv", "neutral.csv" };
    private StreamWriter[] m_Files = new StreamWriter[7];
    private float[] mf_Threshold = { 0.5f, 0.05f, 0.2f, 0.6f, 0.5f, 0.5f, 0.7f };
    private bool mb_Connected = false;

    // Mission variables
    private float mf_MissionTimeElapsed, mf_ScoreSum;
    public float MissionTimeElapsed
    {
        get { return mf_MissionTimeElapsed; }
    }
    private Queue<float> m_ScoreQueue = new Queue<float>();
    private bool mb_MissionSuccess;
    public bool MissionSuccess
    {
        get { return mb_MissionSuccess; }
    }

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
        OrganizeFiles();
        m_ServerThread = new Thread(new ThreadStart(ListenRequests));
        m_ServerThread.IsBackground = true;
        m_ServerThread.Start();
    }

    void OnDestroy()
    {
        try
        {
            for(int i = 0; i < m_Files.Length; ++i)
            {
                m_Files[i].Close();
            }
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

    public void ThrowMission(int emotionNum)
    {
        mf_MissionTimeElapsed += Time.deltaTime;
        
        float score = mf_EmotionScores[emotionNum];
        if(mf_MissionTimeElapsed < 1f)
        {
            m_ScoreQueue.Enqueue(score);
            mf_ScoreSum += score;
        }
        else
        {
            float dequeue_val = m_ScoreQueue.Dequeue();
            m_ScoreQueue.Enqueue(score);
            mf_ScoreSum += (score - dequeue_val);

            float avg_score = mf_ScoreSum / m_ScoreQueue.Count;
            Debug.Log("score: " + score.ToString());
            Debug.Log("avg: " + avg_score.ToString());
            mb_MissionSuccess = avg_score > mf_Threshold[emotionNum] ? true : false;
        }

        WriteLog(emotionNum);
    }

    public void ClearMissionSettings()
    {
        mf_ScoreSum = 0f;
        mf_MissionTimeElapsed = 0f;
        mb_MissionSuccess = false;
        m_ScoreQueue.Clear();
    }

    void OrganizeFiles()
    {
        string dirPath = Directory.GetCurrentDirectory() + "\\PlaytestLog\\";
        if(!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        for(int i = 0; i < m_Files.Length; ++i)
        {
            ms_FileNames[i] = dirPath + ms_FileNames[i];
            m_Files[i] = new StreamWriter(ms_FileNames[i]);
            m_Files[i].WriteLine("angry,disgust,fear,happy,sad,surprised,neutral");
        }
    }

    void WriteLog(int idx)
    {
        m_Files[idx].WriteLine("{0},{1},{2},{3},{4},{5},{6}", mf_EmotionScores[0].ToString(), mf_EmotionScores[1].ToString(), mf_EmotionScores[2].ToString(), mf_EmotionScores[3].ToString(), mf_EmotionScores[4].ToString(), mf_EmotionScores[5].ToString(), mf_EmotionScores[6].ToString());
    }

    public float GetScore(int idx) => mf_EmotionScores[idx];
    public float GetThreshold(int idx) => mf_Threshold[idx];
    public string GetName(int idx) => ms_EmotionNames[idx];
    public bool GetConnected() => mb_Connected;
}
