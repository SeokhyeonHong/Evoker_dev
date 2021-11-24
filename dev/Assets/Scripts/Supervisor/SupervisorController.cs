using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SupervisorController : MonoBehaviour
{
    public GameObject moveRangeObject;
    private Animator m_Animator;
    private float mf_MinX, mf_MinZ, mf_MaxX, mf_MaxZ;
    private float mf_NewX, mf_NewZ, mf_Angle, mf_Speed;
    private Quaternion m_NewQuat;
    private GameObject m_PlayerObject;
    private float mf_NextMoveTime = 0f, mf_MissionTimeElapsed = 0f;
    public int mi_MissionEmotion = 0;
    public float mf_Score = 0f;
    private bool mb_MissionFinished = false;

    void Start()
    {
        SetRange(moveRangeObject);

        m_Animator = GetComponent<Animator>();
        m_PlayerObject = GameObject.Find("PlayerObject");
        mf_Speed = 1.0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        if(distance > 5.0f)
        {
            Move();
            mb_MissionFinished = false;
        }
        else if(!mb_MissionFinished)
        {
            ThrowMission();
        }
    }

    public void SetRange(GameObject obj)
    {
        Vector3 center = obj.transform.position;
        Vector3 scale = obj.transform.localScale;
        mf_MinX = center.x - 5 * scale.x;
        mf_MinZ = center.z - 5 * scale.z;
        mf_MaxX = center.x + 5 * scale.x;
        mf_MaxZ = center.z + 5 * scale.z;
        
        transform.position = new Vector3((mf_MinX + mf_MaxX) / 2, 0, (mf_MinZ+ mf_MaxZ) / 2);
    }

    void Move()
    {
        float weight = 5f;
        if(Time.time > mf_NextMoveTime)
        {
            float dx = (2 * Random.Range(0, 2) - 1) * weight;
            float dz = (2 * Random.Range(0, 2) - 1) * weight;
            
            float normalize = Mathf.Sqrt(dx * dx + dz * dz);
            if(normalize > 0.0001f)
            {
                mf_Angle = Mathf.Rad2Deg * Mathf.Acos(dx / Mathf.Sqrt(dx * dx + dz * dz));
                if(dz < 0)
                {
                    mf_Angle = 360 - mf_Angle;
                }
                mf_Angle = 90 - mf_Angle;

                
                mf_NewX = Mathf.Max(Mathf.Min(transform.position.x + dx, mf_MaxX), mf_MinX);
                mf_NewZ = Mathf.Max(Mathf.Min(transform.position.z + dz, mf_MaxZ), mf_MinZ);
                m_NewQuat = Quaternion.AngleAxis(mf_Angle, Vector3.up);

                mf_NextMoveTime += 1.5f;
            }
        }
        
        Vector3 target = new Vector3(mf_NewX, 0, mf_NewZ);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * mf_Speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_NewQuat, Time.deltaTime * Mathf.Abs(mf_Angle) * 0.05f);
        m_Animator.SetBool("bMoving", true);
    }

    void ThrowMission()
    {
        m_Animator.SetBool("bMoving", false);
        if(mf_MissionTimeElapsed < 5.0f)
        {
            mf_MissionTimeElapsed += Time.deltaTime;
            if(mf_Score > 0.5f)
            {
                m_PlayerObject.GetComponent<CharacterController>().DecreaseGauge(mi_MissionEmotion, 1);
                mb_MissionFinished = true;
                mf_MissionTimeElapsed = 0f;
            }
        }
        else
        {
            m_PlayerObject.GetComponent<CharacterController>().DecreaseGauge(mi_MissionEmotion, 2);
            mb_MissionFinished = true;
            mf_MissionTimeElapsed = 0f;
        }
    }
}
