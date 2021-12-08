using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysColor : MonoBehaviour
{
    private List<Material> m_ChildMaterialList = new List<Material>();
    void Start()
    {
        RecursiveAssignMaterials(transform);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < m_ChildMaterialList.Count; ++i)
        {
            m_ChildMaterialList[i].SetInt("Colored", 1);
        }
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
}
