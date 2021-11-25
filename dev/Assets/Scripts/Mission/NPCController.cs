using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string DefaultAnimationName;
    public int EmotionNum;
    private Animator m_Animator;
    private GameObject m_PlayerObject;
    private PyServer m_Server;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(DefaultAnimationName, true);
    }
    void Start()
    {
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        // m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();
    }

    void Update()
    {
        ThrowMission();
    }

    void ThrowMission()
    {
        
        float dist = Vector3.Distance(m_PlayerObject.transform.position, transform.position);
        if(dist < 3f)
        {
            // mission
            Debug.Log("Close enough");
        }
    }
}
