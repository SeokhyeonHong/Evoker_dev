using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSceneController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    private MissionController m_MC;
    public string EnterSceneName;
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MC = transform.parent.gameObject.GetComponent<MissionController>();
    }

    void Update()
    {
        float dist = Vector3.Distance(m_PlayerObject.transform.position, transform.position);
        if(dist < 5f)
        {
            m_MC.ClosePlayer = true;
            if(Input.GetKey(KeyCode.Return))
            {
                m_MC.InMission = true;
                SceneManager.LoadScene(EnterSceneName);
            }
        }
        else
        {
            m_MC.ClosePlayer = false;
        }
    }
}
