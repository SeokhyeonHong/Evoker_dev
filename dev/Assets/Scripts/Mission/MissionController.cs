﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    private List<bool> m_MissionSuccess = new List<bool>();
    private List<GameObject> m_MissionObjects = new List<GameObject>();

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Mission");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            m_MissionObjects.Add(transform.GetChild(i).gameObject);
            m_MissionSuccess.Add(false);
        }
    }

    void Update()
    {
    }

    public void SetMissionSuccess(int idx)
    {
        m_MissionSuccess[idx] = true;
    }

    public bool GetMissionSuccess(int idx)
    {
        return m_MissionSuccess[idx];
    }
}
