using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SupervisorController : MonoBehaviour
{
    public GameObject moveRangeObject;
    private Animator m_Animator;
    private Quaternion m_NewQuat;
    private GameObject m_PlayerObject, m_ExclaimationObject;
    private PyServer m_Server;
    private float mf_MinX, mf_MinZ, mf_MaxX, mf_MaxZ;
    private float mf_NewX, mf_NewZ, mf_Angle, mf_Speed;
    private List<float> m_ScoreList = new List<float>();
    private float mf_NextMoveTime = 0f, mf_MissionTimeElapsed = 0f;
    private bool mb_MissionFinished = false;
    private int[] mi_EmotionToIdx = {2, 3, 4, 0, 1};
    public int MissionEmotion = 0;

    void Start()
    {
        SetRange(moveRangeObject);

        m_Animator = GetComponent<Animator>();
        m_Server = GameObject.FindGameObjectWithTag("Server").GetComponent<PyServer>();
        m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_ExclaimationObject = transform.Find("Exclaimation").gameObject;
        mf_Speed = 1.0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        if(distance > 5.0f)
        {
            Move();
            mb_MissionFinished = false;
            mf_MissionTimeElapsed = 0f;
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
            float distance = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
            if(distance < 15f)
            {
                m_ExclaimationObject.SetActive(true);

                Vector3 toPlayer = m_PlayerObject.transform.position - transform.position;

                float angle = Vector3.Angle(transform.forward, toPlayer);
                Vector3 cross = Vector3.Cross(transform.forward, toPlayer);
                float dot = Vector3.Dot(transform.up, cross);
                if(dot < 0)
                {
                    mf_Angle -= angle;
                }
                else
                {
                    mf_Angle += angle;
                }

                mf_NewX = m_PlayerObject.transform.position.x;
                mf_NewZ = m_PlayerObject.transform.position.z;
                m_NewQuat = Quaternion.AngleAxis(mf_Angle, Vector3.up);
            }
            else
            {
                m_ExclaimationObject.SetActive(false);
                
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

                }
            }
            mf_NextMoveTime += 0.5f;
        }
        
        Vector3 target = new Vector3(mf_NewX, 0, mf_NewZ);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * mf_Speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_NewQuat, Time.deltaTime * Mathf.Abs(mf_Angle) * 0.05f);
        m_Animator.SetBool("bMoving", true);
    }

    void ThrowMission()
    {
        m_Animator.SetBool("bMoving", false);
        if(mf_MissionTimeElapsed < 5f)
        {
            m_PlayerObject.GetComponent<CharacterController>().SetMovable(false);
            mf_MissionTimeElapsed += Time.deltaTime;
            
            float score = m_Server.GetScore(MissionEmotion);
            if(mf_MissionTimeElapsed < 1f)
            {
                m_ScoreList.Add(score);
            }
            else
            {
                m_ScoreList.Add(score);
                m_ScoreList.RemoveAt(0);

                float avg_score = GetAverageScore();
                if(avg_score > 0.5f)
                {
                    m_PlayerObject.GetComponent<CharacterController>().SetMovable(true);
                    m_PlayerObject.GetComponent<CharacterController>().DecreaseGauge(mi_EmotionToIdx[MissionEmotion], 1);
                    mb_MissionFinished = true;
                    mf_MissionTimeElapsed = 0f;
                }
            }
        }
        else
        {
            m_PlayerObject.GetComponent<CharacterController>().SetMovable(true);
            m_PlayerObject.GetComponent<CharacterController>().DecreaseGauge(mi_EmotionToIdx[MissionEmotion], 2);
            mb_MissionFinished = true;
            mf_MissionTimeElapsed = 0f;
        }
    }

    float GetAverageScore()
    {
        float ret = 0f;
        for(int i = 0; i < m_ScoreList.Count; ++i)
        {
            ret += m_ScoreList[i];
        }
        ret /= m_ScoreList.Count;
        return ret;
    }
}
