using System;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Sprite[] GaugeImages = new Sprite[10];
    private GameObject m_PlayerObject;

    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
    }

    void Update()
    {
        int gauge = m_PlayerObject.GetComponent<CharacterController>().Gauge;
        if(gauge >= 1)
        {
            gameObject.GetComponent<Image>().sprite = GaugeImages[gauge - 1];
        }
    }
}
