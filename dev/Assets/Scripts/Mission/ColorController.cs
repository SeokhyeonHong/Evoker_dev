using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private int mi_Success;
    private List<Material> m_ChildMaterialList;
    void Awake()
    {
        m_ChildMaterialList = new List<Material>();
        mi_Success = 0;

        RecursiveAssignMaterials(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Color();
    }

    void RecursiveAssignMaterials(Transform t)
    {
        if(t.childCount == 0)
        {
            return;
        }

        for(int i = 0; i < t.childCount; ++i)
        {
            Transform t_child = t.GetChild(i);
            Renderer rend = t_child.gameObject.GetComponent<Renderer>();
            if(rend != null)
            {
                m_ChildMaterialList.Add(rend.material);
                // Debug.Log(rend.name);
            }
            RecursiveAssignMaterials(t_child);
        }
    }

    void Color()
    {
        for(int i = 0; i < m_ChildMaterialList.Count; ++i)
        {
            m_ChildMaterialList[i].SetInt("Colored", mi_Success);
        }
    }

    public void SetColor(int success)
    {
        mi_Success = success;
    }
}
