using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// index = ANGRY, DISGUST, FEAR, HAPPY, SAD, SUPRIRSE, NEUTRAL

public class HintmanMission : MonoBehaviour
{
    private Quaternion m_NewQuat;
    private GameObject m_PlayerObject, m_MissionObject, m_HintObject, m_MissionTextObject;
    private PyServer m_Server;
    private float mf_MinX, mf_MinZ, mf_MaxX, mf_MaxZ;
    private float mf_NewX, mf_NewZ, mf_Angle, mf_Speed;
    private List<float> m_ScoreList = new List<float>();
    private float mf_MissionTimeElapsed = 0f;
    private bool mb_MissionSuccess = false;
    private int[] mi_EmotionToIdx = {2, 3, 4, 0, 1};
    private string[] m_EmotionNames = { "ANGRY", "DISGUST", "FEAR", "HAPPY", "SAD", "SUPRISED", "NEUTRAL" };
    public int HintMissionNum = 5;



    void Start()
    {
        m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        m_MissionTextObject = GameObject.Find("Canvas/MissionText");
        m_HintObject = transform.Find("HintObject").gameObject;
    }

    void Update()
    {
        m_HintObject.SetActive(mb_MissionSuccess);

        float distance = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        if(!mb_MissionSuccess && distance < 5f)
        {
            ThrowMission();
            m_MissionTextObject.SetActive(true);
        }
        else
        {
            m_MissionTextObject.SetActive(false);
            if(mb_MissionSuccess)
            {
                mf_MissionTimeElapsed = 0f;
                Vector3 target = m_PlayerObject.transform.position;
                target.y += 3;
                if(distance < 5f)
                {
                    m_HintObject.transform.position = Vector3.MoveTowards(m_HintObject.transform.position, target, Time.deltaTime * 3f);
                }
                else
                {
                    m_HintObject.transform.position = target;
                }
            }
        }
    }


    void ThrowMission()
    {
        m_MissionTextObject.GetComponent<Text>().text = "Make " + m_EmotionNames[HintMissionNum] + " Expression!";
        float score = m_Server.GetScore(HintMissionNum);
        if(mf_MissionTimeElapsed < 1f)
        {
            m_ScoreList.Add(score);
        }
        else
        {
            m_ScoreList.Add(score);
            m_ScoreList.RemoveAt(0);

            float avg_score = GetAverageScore();
            if(avg_score > 0.5f)
            {
                mb_MissionSuccess = true;
                mf_MissionTimeElapsed = 0f;
            }
        }
        mf_MissionTimeElapsed += Time.deltaTime;
    }

    float GetAverageScore()
    {
        float ret = 0f;
        for(int i = 0; i < m_ScoreList.Count; ++i)
        {
            ret += m_ScoreList[i];
        }
        ret /= m_ScoreList.Count;
        return ret;
    }
}
