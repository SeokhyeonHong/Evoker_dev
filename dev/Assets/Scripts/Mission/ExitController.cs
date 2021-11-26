using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    MissionController m_MC;
    void Start()
    {
        m_MC = GameObject.FindGameObjectWithTag("Mission").gameObject.GetComponent<MissionController>();
    }
    public void Exit()
    {
        m_MC.InMission = false;
        SceneManager.LoadScene("Main");
    }
}
