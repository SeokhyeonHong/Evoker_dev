using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    Camera m_Camera;
    private Vector3 m_CameraTrans = new Vector3(0, 6, -10);
    Animator m_Animator;
    private float mfSmooth = 5f;
    private float mfMove = 1f;
    private bool mbMoveLeft, mbMoveRight, mbMoveForward, mbMoveBackward;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Camera = Camera.main;
        m_Camera.transform.rotation = Quaternion.AngleAxis(10f, Vector3.right);
    }

    void Update()
    {
        Move();
        AnimationUpdate();
        CameraUpdate();
    }

    void Move()
    {
        Vector3 vTarget = transform.position;
        Quaternion qTarget = transform.rotation;

        mbMoveForward = Input.GetKey(KeyCode.W);
        mbMoveBackward = Input.GetKey(KeyCode.S);
        mbMoveLeft = Input.GetKey(KeyCode.A);
        mbMoveRight = Input.GetKey(KeyCode.D);

        if(mbMoveForward && mbMoveLeft) {
            vTarget.x -= mfMove / Mathf.Sqrt(2);
            vTarget.z += mfMove / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 315, 0);
        }
        else if(mbMoveForward && mbMoveRight) {
            vTarget.x += mfMove / Mathf.Sqrt(2);
            vTarget.z += mfMove / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 45, 0);
        }
        else if(mbMoveBackward && mbMoveLeft) {
            vTarget.x -= mfMove / Mathf.Sqrt(2);
            vTarget.z -= mfMove / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 225, 0);
        }
        else if(mbMoveBackward && mbMoveRight) {
            vTarget.x += mfMove / Mathf.Sqrt(2);
            vTarget.z -= mfMove / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 135, 0);
        }
        else if(mbMoveForward) {
            vTarget.z += mfMove;
            qTarget = Quaternion.Euler(0, 0, 0);
        }
        else if(mbMoveBackward) {
            vTarget.z -= mfMove;
            qTarget = Quaternion.Euler(0, 180, 0);
        }
        else if(mbMoveLeft) {
            vTarget.x -= mfMove;
            qTarget = Quaternion.Euler(0, 270, 0);
        }
        else if(mbMoveRight) {
            vTarget.x += mfMove;
            qTarget = Quaternion.Euler(0, 90, 0);
        }
        transform.position = Vector3.Lerp(transform.position, vTarget, Time.deltaTime * mfSmooth);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTarget , Time.deltaTime * mfSmooth);
    }

    void AnimationUpdate()
    {
        if(mbMoveForward || mbMoveBackward || mbMoveLeft || mbMoveRight)
        {
            m_Animator.SetBool("bMoving", true);
        }
        else
        {
            m_Animator.SetBool("bMoving", false);
        }
    }

    void CameraUpdate()
    {
        m_Camera.transform.position = transform.position + m_CameraTrans;
    }
}
