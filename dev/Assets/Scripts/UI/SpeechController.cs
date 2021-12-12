using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    public GameObject ImageObject;
    public List<Sprite> SpeechImgs = new List<Sprite>();
    private int m_ImgNum;
    public int SpeechNum
    {
        set { m_ImgNum = value; }
    }
    private bool mb_InMission;
    public bool InMission
    {
        get { return mb_InMission; }
    }
    void Start()
    {
    }

    void Update()
    {
        mb_InMission = m_ImgNum == SpeechImgs.Count - 2 ? true : false; 
    }
    public void ShowSpeech()
    {
        ImageObject.GetComponent<Image>().sprite = SpeechImgs[m_ImgNum];
        if(Input.GetKeyDown(KeyCode.Return) && !mb_InMission)
        {
            m_ImgNum++;
            if(m_ImgNum >= SpeechImgs.Count)
            {
                m_ImgNum = SpeechImgs.Count - 1;
            }
        }
    }

    public void SetSpeechActive(bool val)
    {
        ImageObject.SetActive(val);
    }

    public void IncreaseSpeechNum()
    {
        m_ImgNum++;
    }
}
