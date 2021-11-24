using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScript_2 : MonoBehaviour
{
    private PyServer m_Server;
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Server");
        m_Server = objs[0].GetComponent<PyServer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_Server.GetConnected());
    }
    public void Skip()
    {
        SceneManager.LoadScene("Main");
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Intro_1");
    }
}
