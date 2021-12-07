using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionText : MonoBehaviour
{
    public List<GameObject> ObjectList = new List<GameObject>();
    private List<SupervisorController> m_SuperList = new List<SupervisorController>();
    private List<HintmanMission> m_HintList = new List<HintmanMission>();
    private string[] m_EmotionNames = { "ANGRY", "DISGUST", "FEAR", "HAPPY", "SAD", "SUPRISED", "NEUTRAL" };

    void Start()
    {
        for(int i = 0; i < ObjectList.Count; ++i)
        {
            SupervisorController s = ObjectList[i].GetComponent<SupervisorController>();
            HintmanMission h = ObjectList[i].GetComponent<HintmanMission>();
            if(s != null)
            {
                m_SuperList.Add(s);
            }
            else if(h != null)
            {
                m_HintList.Add(h);
            }
        }
    }

    void Update()
    {
        bool show = false;
        int emotionNum = -1;
        for(int i = 0; i < m_SuperList.Count; ++i)
        {
            if(m_SuperList[i].ShowMessage)
            {
                show = true;
                emotionNum = m_SuperList[i].MissionEmotionNum;
            }
        }
        for(int i = 0; i < m_HintList.Count; ++i)
        {
            if(m_HintList[i].ShowMessage)
            {
                show = true;
                emotionNum = m_HintList[i].HintMissionNum;
            }
        }
        if(show)
        {
            gameObject.GetComponent<Text>().text = "Make " + m_EmotionNames[emotionNum] + " Expression!";
        }
        else
        {
            gameObject.GetComponent<Text>().text = "";
        }
    }
}
