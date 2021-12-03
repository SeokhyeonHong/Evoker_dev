using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    MissionController m_MC;
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MC = GameObject.FindGameObjectWithTag("Mission").gameObject.GetComponent<MissionController>();
    }
    public void Exit()
    {
        m_MC.InMission = false;
        m_PlayerObject.transform.position = m_MC.EntryPosition;
        SceneManager.LoadScene("Main");
    }
}
