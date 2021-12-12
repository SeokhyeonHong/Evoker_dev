using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionObjController : MonoBehaviour
{
    public int MissionEmotion, MissionNum;
    public GameObject TextObject;
    private Text m_Text;
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

        m_Text = TextObject.GetComponent<Text>();
        m_SC = this.GetComponent<SpeechController>();
    }

    void Update()
    {
        bool success = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(MissionNum);
        float dist = Vector3.Distance(m_NPCObject.transform.position, m_PlayerObject.transform.position);

        if(!success)
        {
            m_Text.text = "";
            if(dist < 5f)
            {
                m_SC.SetSpeechActive(true);
                m_SC.ShowSpeech();
                m_Server.ThrowMission(MissionEmotion);
                if(m_Server.MissionSuccess)
                {
                    m_MissionObject.GetComponent<MissionController>().SetMissionSuccess(MissionNum);
                    m_Server.ClearMissionSettings();
                }
            }
            else
            {
                m_SC.SetSpeechActive(false);
                m_SC.SpeechNum = 0;
            }
        }
        else
        {
            m_Text.text = "Mission Success!\nPress Enter to Exit!";
            m_SC.SetSpeechActive(false);
            if(Input.GetKeyDown(KeyCode.Return))
            {
                m_MissionObject.GetComponent<MissionController>().InMission = false;
                m_PlayerObject.transform.position = m_MissionObject.GetComponent<MissionController>().EntryPosition;
                SceneManager.LoadScene("Main");
            }
        }

        int color = success ? 1 : 0;
        this.GetComponent<ColorController>().SetColor(color);
    }
}
