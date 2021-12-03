using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjController : MonoBehaviour
{
    public string DefaultAnimationName;
    public int MissionEmotion, MissionNum;
    public GameObject ExitButton;
    private Animator m_Animator;
    private GameObject m_NPCObject, m_PlayerObject, m_MissionObject;
    private PyServer m_Server;
    private List<float> m_ScoreList = new List<float>();
    private float mf_MissionTimeElapsed = 0f;
    private bool mb_Success = false;

    void Start()
    {
        m_NPCObject = transform.Find("NPC").gameObject;
        m_Animator = m_NPCObject.GetComponent<Animator>();
        m_Animator.SetBool(DefaultAnimationName, true);

        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();

        ExitButton.SetActive(false);
    }

    void Update()
    {
        mb_Success = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(MissionNum);
        float dist = Vector3.Distance(m_NPCObject.transform.position, m_PlayerObject.transform.position);

        if(!mb_Success && dist < 5f)
        {
            ThrowMission();
        }

        
        ExitButton.SetActive(mb_Success);

        int color = mb_Success ? 1 : 0;
        this.GetComponent<ColorController>().SetColor(color);
    }

    void ThrowMission()
    {
        mf_MissionTimeElapsed += Time.deltaTime;
        
        float score = m_Server.GetScore(MissionEmotion);
        if(mf_MissionTimeElapsed < 1f)
        {
            m_ScoreList.Add(score);
        }
        else
        {
            m_ScoreList.Add(score);
            m_ScoreList.RemoveAt(0);

            float avg_score = GetAverageScore();
            if(avg_score > m_Server.GetThreshold(MissionEmotion))
            {
                m_MissionObject.GetComponent<MissionController>().SetMissionSuccess(MissionNum);
                mf_MissionTimeElapsed = 0f;
                m_ScoreList.Clear();
            }
        }
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
