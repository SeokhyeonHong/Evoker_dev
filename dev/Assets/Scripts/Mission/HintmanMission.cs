using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HintmanMission : MonoBehaviour
{
    private GameObject m_PlayerObject, m_MissionObject, m_HintObject;
    private PyServer m_Server;
    private SpeechController m_SC;
    private AudioSource m_AudioSource;
    private bool mb_MissionSuccess = false;
    public int HintEmotionNum = 5;

    void Start()
    {
        m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        m_HintObject = transform.Find("HintObject").gameObject;
        m_SC = GetComponent<SpeechController>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        m_HintObject.SetActive(mb_MissionSuccess);
        
        float distance = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        if(!mb_MissionSuccess)
        {
            if(distance < 5f)
            {
                m_SC.SetSpeechActive(true);
                m_SC.ShowSpeech();
                if(m_SC.InMission)
                {
                    m_Server.ThrowMission(HintEmotionNum);
                    if(m_Server.MissionSuccess)
                    {
                        mb_MissionSuccess = true;
                        m_Server.ClearMissionSettings();
                        m_SC.IncreaseSpeechNum();
                        m_AudioSource.Play();
                    }
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
            if(distance < 5f)
            {
                m_SC.SetSpeechActive(true);
                m_SC.ShowSpeech();
            }
            else
            {
                m_SC.SetSpeechActive(false);
            }

            Vector3 target = m_PlayerObject.transform.position;
            target.y = m_HintObject.transform.position.y;
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
