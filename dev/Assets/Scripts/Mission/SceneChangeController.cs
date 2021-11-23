using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    private string m_SceneName;
    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        if(dist < 1f)
        {
            Debug.Log(m_SceneName);
            SceneManager.LoadScene(m_SceneName);
        }
    }
    public void SetSceneName(string sceneName)
    {
        m_SceneName = sceneName;
    }
}
