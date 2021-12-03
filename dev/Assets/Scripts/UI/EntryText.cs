using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryText : MonoBehaviour
{
    private GameObject m_EntryMessage;
    private MissionController m_MC;
    void Start()
    {
        m_MC = transform.parent.gameObject.GetComponent<MissionController>();
        m_EntryMessage = transform.Find("EntryMessage").gameObject;
    }

    void Update()
    {
        bool inMission = m_MC.InMission;
        bool closePlayer = m_MC.GetClose();
        m_EntryMessage.SetActive(!inMission && closePlayer);
    }
}
