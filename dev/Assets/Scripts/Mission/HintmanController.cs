using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintmanController : MonoBehaviour
{
    
    private GameObject m_MissionObject;
    private List<GameObject> m_HintmanObjectList = new List<GameObject>();
    void Start()
    {
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        for(int i = 0; i < transform.childCount; ++i)
        {
            m_HintmanObjectList.Add(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        for(int i = 0; i < m_HintmanObjectList.Count; ++i)
        {
            bool success = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(i);
            m_HintmanObjectList[i].SetActive(success);
        }
    }
}
