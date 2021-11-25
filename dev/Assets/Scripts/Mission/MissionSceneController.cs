using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSceneController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    public GameObject AskMessage;
    public string EnterSceneName;
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        AskMessage.SetActive(false);
    }

    void Update()
    {
        float dist = Vector3.Distance(m_PlayerObject.transform.position, transform.position);
        if(dist < 5f)
        {
            AskMessage.SetActive(true);
            if(Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(EnterSceneName);
            }
        }
        else
        {
            AskMessage.SetActive(false);
        }
    }
}
