using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SupervisorController : MonoBehaviour
{
    private Animator m_Animator;
    private float mf_MinX, mf_MinZ, mf_MaxX, mf_MaxZ;
    private float mf_NewX, mf_NewZ, mf_Angle;
    private Quaternion m_NewQuat;
    private GameObject m_PlayerObject;
    private float mf_NextMoveTime = 0f;
    public void SetRange(float minX, float minZ, float maxX, float maxZ)
    {
        mf_MinX = minX;
        mf_MinZ = minZ;
        mf_MaxX = maxX;
        mf_MaxZ = maxZ;
        
        transform.position = new Vector3((minX + maxX) / 2, 0, (minZ + maxZ) / 2);

        m_Animator = GetComponent<Animator>();
        m_PlayerObject = GameObject.Find("PlayerObject");
    }

    public void Move()
    {
        float weight = 5f;

        if(Time.time > mf_NextMoveTime)
        {
            float dx = (2 * Random.Range(0, 2) - 1) * weight;
            float dz = (2 * Random.Range(0, 2) - 1) * weight;

            // float dx = Random.Range(-1f, 1f) * weight;
            // float dz = Random.Range(-1f, 1f) * weight;
            
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

                mf_NextMoveTime += 3f;
            }
        }
        
        Vector3 target = new Vector3(mf_NewX, 0, mf_NewZ);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_NewQuat, Time.deltaTime * Mathf.Abs(mf_Angle) * 0.05f);
        m_Animator.SetBool("bMoving", true);
    }

    public void DetectGameEnd()
    {
        float dist = Vector3.Distance(transform.position, m_PlayerObject.transform.position);
        Text text = GameObject.Find("Canvas/EndMessage").GetComponent<Text>();
        if(dist < 5.0f)
        {
            // Text text = GameObject.Find("Canvas/UserMessage").GetComponent<Text>();
            text.text = "Game End!";
        }
        else
        {
            text.text = "Not End Yet";
        }

    }

}
