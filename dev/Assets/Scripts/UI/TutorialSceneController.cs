using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialSceneController : MonoBehaviour
{
    public List<Sprite> ImgList = new List<Sprite>();
    public GameObject StartButton, PrevButton, NextButton;
    private int m_ImgNum = 0;
    void Start()
    {
    }

    void Update()
    {
        bool is_first_page = m_ImgNum == 0 ? true : false;
        bool is_last_page = m_ImgNum == ImgList.Count - 1 ? true : false;
        StartButton.SetActive(is_last_page);
        NextButton.SetActive(!is_last_page);
        PrevButton.SetActive(!is_first_page);
        this.GetComponent<Image>().sprite = ImgList[m_ImgNum];
    }

    public void IncreaseImgNum()
    {
        if(m_ImgNum < ImgList.Count - 1)
        {
            m_ImgNum++;
        }
    }

    public void DecreaseImgNum()
    {
        if(m_ImgNum > 0)
        {
            m_ImgNum--;
        }
    }
}
