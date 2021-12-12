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
    public bool SpeechFinished
    {
        get { return m_ImgNum < SpeechImgs.Count ? false : true; }
    }
    void Start()
    {
    }

    void Update()
    {
    }
    public void ShowSpeech()
    {
        ImageObject.GetComponent<Image>().sprite = SpeechImgs[m_ImgNum];
        if(Input.GetKeyDown(KeyCode.Return))
        {
            m_ImgNum++;
        }
    }

    public void SetSpeechActive(bool val)
    {
        ImageObject.SetActive(val);
    }
}
