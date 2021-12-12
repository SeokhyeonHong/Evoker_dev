using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonopolyCompany : MonoBehaviour
{
    public GameObject ImageObject;
    public Sprite[] Images = new Sprite[2];
    private GameObject m_PlayerObject;
    private MissionController m_MC;
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_MC = GameObject.FindGameObjectWithTag("Mission").GetComponent<MissionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MC.GetAllMissionSuccess())
        {
            ImageObject.SetActive(true);
            float dist = Vector3.Distance(m_PlayerObject.transform.position, transform.position);
            if(dist < 5f)
            {
                ImageObject.GetComponent<Image>().sprite = Images[1];
                if(Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene("Outro_1");
                }
            }
            else
            {
                ImageObject.GetComponent<Image>().sprite = Images[0];
            }
        }
        else
        {
            ImageObject.SetActive(false);
        }
    }
}
