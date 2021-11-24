using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public int emotionNum = 0;
    private int mi_Success = 0;
    private int mi_FailCount = 0;
    private float mf_ElapsedTime = 0.0f;
    private List<bool> m_SuccessList;
    private GameObject m_PlayerObject, m_ServerObject, m_NeighborObject;
    private Text m_Text;

    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
        m_ServerObject = GameObject.Find("ServerObject");
        m_NeighborObject = transform.Find("Neighbor").gameObject;

        m_SuccessList = new List<bool>();

        m_Text = GameObject.Find("Canvas/MissionMessage").GetComponent<Text>();
        m_Text.text = "";
    }

    void Update()
    {
        if(mi_Success == 0)
        {
            DoMission();
        }
        
    }

    void DoMission()
    {
        if(mi_FailCount > 3)
        {
            m_Text.text = "Failed Mission";
            return;
        }

        float dist = Vector3.Distance(m_NeighborObject.transform.position, m_PlayerObject.transform.position);
        if(dist > 5.0f)
        {
            return;
        }

        PyServer ps = m_ServerObject.GetComponent<PyServer>();
        if(ps.GetConnected())
        {
            
            float score = ps.GetScore(emotionNum);
            bool success = score < 0.5f ? false : true;
            
            if(mf_ElapsedTime > 1.0f)
            {
                m_SuccessList.Add(success);
                m_SuccessList.RemoveAt(0);
            }
            else
            {
                m_SuccessList.Add(success);
                m_Text.text = "Make " + ps.GetName(emotionNum) + " Expression!";
            }

            float s_rate = SuccessRate();
            if(s_rate > 0.7f)
            {
                mi_Success = 1;
                GetComponent<ColorController>().Success();
                m_Text.text = "";
                m_SuccessList.Clear();
                return;
            }
        }
        else
        {
            m_Text.text = "Server Not Connected!";
        }
    }

    float SuccessRate()
    {
        List<bool> trueList = m_SuccessList.FindAll(x=> x == true);
        return 1.0f * trueList.Count / m_SuccessList.Count;
    }
}
