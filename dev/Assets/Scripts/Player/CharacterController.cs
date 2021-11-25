using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    Camera m_Camera;
    private Vector3 m_CameraTrans = new Vector3(0, 6, -10);
    Animator m_Animator;
    private float mf_Smooth = 5f;
    private float mf_Move = 1f;
    private bool mb_MoveLeft, mb_MoveRight, mb_MoveForward, mb_MoveBackward;
    private bool mb_Run, mb_Movable = true;

    private int[] mi_Gauges = new int[5];

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Camera = Camera.main;
        m_Camera.transform.rotation = Quaternion.AngleAxis(10f, Vector3.right);
        for(int i = 0; i < mi_Gauges.Length; ++i)
        {
            mi_Gauges[i] = 5;
        }
    }

    void Update()
    {
        if(mb_Movable)
        {
            Move();
        }
        AnimationUpdate();
        CameraUpdate();
    }

    void Move()
    {
        Vector3 vTarget = transform.position;
        vTarget.y = 0f;
        
        Quaternion qTarget = transform.rotation;

        mb_MoveForward = Input.GetKey(KeyCode.W);
        mb_MoveBackward = Input.GetKey(KeyCode.S);
        mb_MoveLeft = Input.GetKey(KeyCode.A);
        mb_MoveRight = Input.GetKey(KeyCode.D);
        mb_Run = Input.GetKey(KeyCode.Space);

        if(mb_Run)
        {
            mf_Move = 1.5f;
        }
        else
        {
            mf_Move = 1f;
        }

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
        if(!mb_Movable)
        {
            m_Animator.SetBool("bRunning", false);
            m_Animator.SetBool("bMoving", false);
            return;
        }

        if(mb_MoveForward || mb_MoveBackward || mb_MoveLeft || mb_MoveRight)
        {
            m_Animator.SetBool("bMoving", true);
            if(mb_Run)
            {
                m_Animator.SetBool("bRunning", true);
            }
            else
            {
                m_Animator.SetBool("bRunning", false);
            }
        }
        else
        {
            m_Animator.SetBool("bMoving", false);
            m_Animator.SetBool("bRunning", false);
        }

    }

    void CameraUpdate()
    {
        m_Camera.transform.position = transform.position + m_CameraTrans;
        m_Camera.transform.rotation = Quaternion.AngleAxis(10f, Vector3.right);
    }

    public int GetEmotionGauge(int idx)
    {
        return mi_Gauges[idx];
    }

    public void DecreaseGauge(int idx, int decrease)
    {
        mi_Gauges[idx] -= decrease;
        if(mi_Gauges[idx] <= 0)
        {
            SceneManager.LoadScene("NO!!!");
        }
    }

    public void SetMovable(bool movable)
    {
        mb_Movable = movable;
    }
}
