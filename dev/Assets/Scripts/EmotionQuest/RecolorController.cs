using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecolorController : MonoBehaviour
{
    public int emotionNum = 0;
    private int mi_Colored = 0;
    private List<Material> m_ChildMaterials;
    private GameObject m_PlayerObject, m_ServerObject, m_NeighborObject;
    private Text m_Text;

    /*
    public void Initialize(int emotionNum)
    {
        mi_EmotionNum = emotionNum;
    */
    void Start()
    {
        m_PlayerObject = GameObject.Find("PlayerObject");
        m_ServerObject = GameObject.Find("ServerObject");
        m_NeighborObject = transform.Find("Neighbor").gameObject;

        m_ChildMaterials = new List<Material>();

        m_Text = GameObject.Find("Canvas/MissionMessage").GetComponent<Text>();

        for(int i = 0; i < transform.childCount; ++i)
        {
            Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
            if(rend != null)
            {
                m_ChildMaterials.Add(rend.material);
            }
        }
        Renderer rend_neighbor = m_NeighborObject.transform.Find("Mesh").gameObject.GetComponent<Renderer>();
        if(rend_neighbor != null)
        {
            m_ChildMaterials.Add(rend_neighbor.material);
        }
    }

    void Update()
    {
        Recolor();
        RecognizeEmotion();
    }

    public void Recolor()
    {
        for(int i = 0; i < m_ChildMaterials.Count; ++i)
        {
            m_ChildMaterials[i].SetInt("Colored", mi_Colored);
        }
    }

    public void RecognizeEmotion()
    {
        if(mi_Colored == 1)
        {
            return;
        }

        float dist = Vector3.Distance(m_NeighborObject.transform.position, m_PlayerObject.transform.position);
        if(dist > 5.0f)
        {
            Debug.Log("Have to be closer to " + name);
            return;
        }

        PyServer ps = m_ServerObject.GetComponent<PyServer>();
        if(ps.bConnected)
        {
            m_Text.text = "Make " + ps.GetName(emotionNum) + " Expression!";
            float score = ps.GetScore(emotionNum);
            mi_Colored = score < 0.5f ? 0 : 1;
        }
    }
}
