using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScript : MonoBehaviour
{   
    public Sprite Background_1, Background_2, Background_Ready;
    public GameObject Button;
    private GameObject m_ServerObject;
    private PyServer m_Server;
    void Start()
    {
        m_ServerObject = GameObject.Find("ServerObject");
        m_Server = m_ServerObject.GetComponent<PyServer>();
    }

    void Update()
    {
        if(m_Server.GetConnected())
        {
            GetComponent<Image>().sprite = Background_Ready;
            Button.SetActive(true);
        }
        else
        {
            int time_in_sec = ((int)Time.time);
            if((time_in_sec / 5) % 2 == 0)
            {
                GetComponent<Image>().sprite = Background_1;
            }
            else
            {
                GetComponent<Image>().sprite = Background_2;
            }
            Button.SetActive(false);
        }
    }
}
