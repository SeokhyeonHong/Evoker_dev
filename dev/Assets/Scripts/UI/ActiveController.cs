using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveController : MonoBehaviour
{
    private bool mb_Active;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.SetActive(mb_Active);
    }

    void LateUpdate()
    {
        mb_Active = false;
    }

    public void Active(bool val)
    {
        mb_Active = (mb_Active || val);
    }
}
