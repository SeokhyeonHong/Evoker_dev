using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorController : MonoBehaviour
{
    private GameObject m_MissionObject;
    private List<ColorController> m_MapObjects = new List<ColorController>();
    void Start()
    {
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        for(int i = 0; i < transform.childCount - 1; ++i)
        {
            ColorController cc = transform.GetChild(i).gameObject.GetComponent<ColorController>();
            if(cc != null)
            {
                m_MapObjects.Add(cc);
            }
        }
    }

    void Update()
    {
        for(int i = 0; i < m_MapObjects.Count; ++i)
        {
            bool success = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(i);
            int color = success ? 1 : 0;
            m_MapObjects[i].SetColor(color);
        }
    }
}
