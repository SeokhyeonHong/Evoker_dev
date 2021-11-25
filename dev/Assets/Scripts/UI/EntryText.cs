using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryText : MonoBehaviour
{
    public List<GameObject> Entries = new List<GameObject>();
    private GameObject m_PlayerObject;
    void Start()
    {
        m_PlayerObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        bool show = false;
        for(int i = 0; i < Entries.Count; ++i)
        {
            float dist = Vector3.Distance(Entries[i].transform.position, m_PlayerObject.transform.position);
            if(dist < 5f)
            {
                show = true;
            }
        }
        string msg = show ? "Press 'Enter' to enter the mission" : "";
        gameObject.GetComponent<Text>().text = msg;
    }
}
