using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScript_1 : MonoBehaviour
{
    public bool bTest;
    private int mEmotionNum;
    private GameObject m_ServerObject, m_Button;
    private PyServer m_Server;
    private List<Text> m_ChildTextList = new List<Text>();
    void Start()
    {
        mEmotionNum = 6;
        m_ServerObject = GameObject.Find("ServerObject");
        m_Server = m_ServerObject.GetComponent<PyServer>();
        m_Button = GameObject.Find("Button");

        for(int i = 0; i < transform.childCount; ++i)
        {
            Text text = transform.GetChild(i).GetComponent<Text>();
            if(text != null)
            {
                m_ChildTextList.Add(text);
            }
        }
    }

    void Update()
    {
        string message = "";
        if(m_Server.GetConnected())
        {
            message = "Press Start";
            m_Button.SetActive(true);
        }
        else
        {
            message = System.IO.Directory.GetCurrentDirectory();//"Loading ...";
            m_Button.SetActive(false);
        }

        for(int i = 0; i < m_ChildTextList.Count; ++i)
        {
            m_ChildTextList[i].text = message;
        }
    }

    public void SetEmotionNum(int emotionNum)
    {
        mEmotionNum = emotionNum;        
    }
    
    public void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
