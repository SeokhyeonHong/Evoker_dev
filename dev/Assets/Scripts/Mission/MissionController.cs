using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    private List<bool> m_MissionSuccess = new List<bool>();
    private List<GameObject> m_MissionObjects = new List<GameObject>();
    private Vector3 m_EntryPosition;
    public Vector3 EntryPosition
    {
        get { return m_EntryPosition; }
        set { m_EntryPosition = value; }
    }
    private bool[] mb_IsClose;
    private bool mb_InMission;
    public bool InMission
    {
        get { return mb_InMission; }
        set { mb_InMission = value; }
    }

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
        // except the canvas
        for(int i = 1; i < transform.childCount; ++i)
        {
            m_MissionObjects.Add(transform.GetChild(i).gameObject);
            m_MissionSuccess.Add(false);
        }
        mb_IsClose = new bool[transform.childCount - 1];
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

    public bool GetAllMissionSuccess()
    {
        for(int i = 0; i < m_MissionSuccess.Count; ++i)
        {
            if(!m_MissionSuccess[i])
            {
                return false;
            }
        }
        return true;
    }

    public void SetClose(int idx, bool val)
    {
        mb_IsClose[idx] = val;
    }

    public bool GetClose()
    {
        for(int i = 0; i < mb_IsClose.Length; ++i)
        {
            if(mb_IsClose[i])
            {
                return true;
            }
        }
        return false;
    }
}
