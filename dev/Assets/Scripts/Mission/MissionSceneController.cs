using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSceneController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    public string EnterSceneName;
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float dist = Vector3.Distance(m_PlayerObject.transform.position, transform.position);
        if(dist < 5f && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(EnterSceneName);
        }
    }
}
