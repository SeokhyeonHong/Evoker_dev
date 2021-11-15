using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRecolor : MonoBehaviour
{
    private const int mi_EmotionNum = 3;
    private int isMono = 1;
    private Material material;
    private GameObject player, server;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        player = GameObject.Find("PlayerObject");
        server = GameObject.Find("ServerObject");
    }

    // Update is called once per frame
    void Update()
    {
        material.SetInt("isMono", isMono);
        RecognizeEmotion();
    }

    void RecognizeEmotion()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist > 5.0f)
        {
            Debug.Log("Have to be closer");
            return;
        }

        PyServer ps = server.GetComponent<PyServer>();
        if(ps.bConnected)
        {
            float score = ps.GetScore(mi_EmotionNum);
            isMono = score > 0.5f ? 0 : 1;
        }
    }
}
