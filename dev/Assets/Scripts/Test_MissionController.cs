using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Test_MissionController : MonoBehaviour
{
    public int emotionNum = 0;
    private int mi_Success = 0;
    private int mi_FailCount = 0;
    private float mf_ElapsedTime = 0.0f;
    private List<bool> m_SuccessList;
    private GameObject m_PlayerObject, m_ServerObject, m_NeighborObject;
    private List<GameObject> m_SupervisorObjList1, m_SupervisorObjList2;
    private Text m_Text;

    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
        m_ServerObject = GameObject.Find("ServerObject");
        m_NeighborObject = transform.Find("Neighbor").gameObject;
        m_SupervisorObjList1 = new List<GameObject>();
        m_SupervisorObjList2 = new List<GameObject>();

        Transform t_sup1 = transform.Find("Supervisor1");
        t_sup1.gameObject.SetActive(true);
        for(int i = 0; i < t_sup1.childCount; ++i)
        {
            m_SupervisorObjList1.Add(t_sup1.GetChild(i).gameObject);
        }
        
        Transform t_sup2 = transform.Find("Supervisor2");
        t_sup2.gameObject.SetActive(false);
        for(int i = 0; i < t_sup2.childCount; ++i)
        {
            m_SupervisorObjList2.Add(t_sup2.GetChild(i).gameObject);
        }

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
        if(ps.bConnected)
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

            if(mf_ElapsedTime > 5.0f)
            {
                if(mi_FailCount < 3)
                {
                    m_Text.text = "Fail! Penalty will be given";
                }
                else
                {
                    mi_FailCount++;
                    return;
                }
            }
            if(mf_ElapsedTime > 8.0f)
            {
                mi_FailCount++;
                mf_ElapsedTime = 0.0f;
                switch(mi_FailCount)
                {
                    case 1:
                    for(int i = 0; i < m_SupervisorObjList1.Count; ++i)
                    {
                        m_SupervisorObjList1[i].GetComponent<SupervisorController>().SpeedUp();
                    }
                    break;

                    case 2:
                    for(int i = 0; i < m_SupervisorObjList1.Count; ++i)
                    {
                        m_SupervisorObjList1[i].GetComponent<SupervisorController>().SearchScaleUp();
                    }
                    break;

                    case 3:
                    transform.Find("Supervisor2").gameObject.SetActive(true);
                    for(int i = 0; i < m_SupervisorObjList2.Count; ++i)
                    {
                        m_SupervisorObjList2[i].GetComponent<SupervisorController>().SpeedUp();
                        m_SupervisorObjList2[i].GetComponent<SupervisorController>().SearchScaleUp();
                    }
                    break;

                    default:
                    break;
                }
                m_SuccessList.Clear();
            }

            mf_ElapsedTime += Time.deltaTime;
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
