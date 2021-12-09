using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneController : MonoBehaviour
{
    public List<Sprite> ImgList = new List<Sprite>();
    public GameObject StartButton;
    private int m_ImgNum = 0;
    void Start()
    {
    }

    void Update()
    {
        bool active = m_ImgNum < ImgList.Count - 1 ? false : true;
        StartButton.SetActive(active);
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
