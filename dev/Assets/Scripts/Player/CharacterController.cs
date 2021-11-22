﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Camera m_Camera;
    private Vector3 m_CameraTrans = new Vector3(0, 6, -10);
    Animator m_Animator;
    private float mf_Smooth = 5f;
    private float mf_Move = 1f;
    private bool mb_MoveLeft, mb_MoveRight, mb_MoveForward, mb_MoveBackward;
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

        mb_MoveForward = Input.GetKey(KeyCode.W);
        mb_MoveBackward = Input.GetKey(KeyCode.S);
        mb_MoveLeft = Input.GetKey(KeyCode.A);
        mb_MoveRight = Input.GetKey(KeyCode.D);

        if(mb_MoveForward && mb_MoveLeft) {
            vTarget.x -= mf_Move / Mathf.Sqrt(2);
            vTarget.z += mf_Move / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 315, 0);
        }
        else if(mb_MoveForward && mb_MoveRight) {
            vTarget.x += mf_Move / Mathf.Sqrt(2);
            vTarget.z += mf_Move / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 45, 0);
        }
        else if(mb_MoveBackward && mb_MoveLeft) {
            vTarget.x -= mf_Move / Mathf.Sqrt(2);
            vTarget.z -= mf_Move / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 225, 0);
        }
        else if(mb_MoveBackward && mb_MoveRight) {
            vTarget.x += mf_Move / Mathf.Sqrt(2);
            vTarget.z -= mf_Move / Mathf.Sqrt(2);
            qTarget = Quaternion.Euler(0, 135, 0);
        }
        else if(mb_MoveForward) {
            vTarget.z += mf_Move;
            qTarget = Quaternion.Euler(0, 0, 0);
        }
        else if(mb_MoveBackward) {
            vTarget.z -= mf_Move;
            qTarget = Quaternion.Euler(0, 180, 0);
        }
        else if(mb_MoveLeft) {
            vTarget.x -= mf_Move;
            qTarget = Quaternion.Euler(0, 270, 0);
        }
        else if(mb_MoveRight) {
            vTarget.x += mf_Move;
            qTarget = Quaternion.Euler(0, 90, 0);
        }
        transform.position = Vector3.Lerp(transform.position, vTarget, Time.deltaTime * mf_Smooth);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTarget , Time.deltaTime * mf_Smooth);
    }

    void AnimationUpdate()
    {
        if(mb_MoveForward || mb_MoveBackward || mb_MoveLeft || mb_MoveRight)
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