using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private GameObject m_MissionObject;
    private bool mb_Active = false;
    public int MissionNum = 0;
    public Sprite GrayImage, ColorImage;
    void Start()
    {
        m_MissionObject = GameObject.FindGameObjectWithTag("Mission");
        gameObject.SetActive(false);
    }

    void Update()
    {
        bool color = m_MissionObject.GetComponent<MissionController>().GetMissionSuccess(MissionNum);
        if(color)
        {
            gameObject.GetComponent<Image>().sprite = ColorImage;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = GrayImage;
        }
    }

    public void SwitchActive()
    {
        mb_Active = !mb_Active;
        gameObject.SetActive(mb_Active);
    }
}
