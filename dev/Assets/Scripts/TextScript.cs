using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private int mEmotionNum;
    private GameObject serverObject;
    private PyServer server;
    Text mText;
    // Start is called before the first frame update
    void Start()
    {
        mEmotionNum = 6;
        serverObject = GameObject.Find("ServerObject");
        server = serverObject.GetComponent<PyServer>();
        mText = GetComponent<Text>();
    }

    void Update()
    {
        if(mEmotionNum != 6)
        {
            float score = server.GetScore(mEmotionNum);
            string name = server.GetName(mEmotionNum);
            if(score > 0.5f)
            {
                mText.text = name + " Success!";
            }
            else
            {
                mText.text = "Be more " + name + "!";
            }
        }
        else
        {
            mText.text = "Select an emotion!";
        }
    }

    void OnDestroy()
    {
        server.OnDestroy();
    }

    public void SetEmotionNum(int emotionNum)
    {
        mEmotionNum = emotionNum;        
    }
}
