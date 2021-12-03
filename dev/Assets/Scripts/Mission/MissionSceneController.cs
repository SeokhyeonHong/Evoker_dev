using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSceneController : MonoBehaviour
{
    private GameObject m_PlayerObject;
    private MissionController m_MC;
    public string EnterSceneName;
    public int MissionNum;
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
            m_MC.SetClose(MissionNum, true);
            if(Input.GetKey(KeyCode.Return))
            {
                m_MC.InMission = true;
                m_MC.EntryPosition = m_PlayerObject.transform.position;
                m_PlayerObject.transform.position = Vector3.zero;
                SceneManager.LoadScene(EnterSceneName);
            }
        }
        else
        {
            m_MC.SetClose(MissionNum, false);
        }
    }
}
