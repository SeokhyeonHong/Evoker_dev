using System;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public int EmotionNum;
    public Sprite[] GaugeImages = new Sprite[5];
    private GameObject m_PlayerObject;

    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
    }

    void Update()
    {
        int gauge = m_PlayerObject.GetComponent<CharacterController>().GetEmotionGauge(EmotionNum);
        if(gauge >= 1)
        {
            gameObject.GetComponent<Image>().sprite = GaugeImages[gauge - 1];
        }
    }
}
