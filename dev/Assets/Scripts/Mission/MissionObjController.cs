using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionObjController : MonoBehaviour
{
    public int MissionEmotion, MissionNum;
    private GameObject m_NPCObject, m_PlayerObject, m_MissionObject;
    private PyServer m_Server;
    private SpeechController m_SC;
    private List<float> m_ScoreList = new List<float>();
    private float mf_MissionTimeElapsed = 0f;

    void Start()
    {
        m_NPCObject = transform.Find("NPC").gameObject;

        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();

        m_SC = this.GetComponent<SpeechController>();
    }

    void Update()
    {
        bool success = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(MissionNum);
        float dist = Vector3.Distance(m_NPCObject.transform.position, m_PlayerObject.transform.position);

        if(!success)
        {
            if(dist < 5f)
            {
                m_SC.SetSpeechActive(true);
                m_SC.ShowSpeech();
                if(m_SC.InMission)
                {
                    m_Server.ThrowMission(MissionEmotion);
                    if(m_Server.MissionSuccess)
                    {
                        GetComponent<AudioSource>().Play();
                        m_MissionObject.GetComponent<MissionController>().SetMissionSuccess(MissionNum);
                        m_Server.ClearMissionSettings();
                        m_SC.IncreaseSpeechNum();
                    }
                }
            }
            else
            {
                m_SC.SpeechNum = 0;
                m_SC.SetSpeechActive(false);
            }
        }
        else
        {
            m_SC.SpeechNum = 2;
            m_SC.SetSpeechActive(true);
            m_SC.ShowSpeech();
            if(Input.GetKey(KeyCode.Return))
            {
                Exit();
            }
        }

        int color = success ? 1 : 0;
        this.GetComponent<ColorController>().SetColor(color);
    }

    public void Exit()
    {
        m_MissionObject.GetComponent<MissionController>().InMission = false;
        m_PlayerObject.transform.position = m_MissionObject.GetComponent<MissionController>().EntryPosition;
        SceneManager.LoadScene("Main");
    }
}
