using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor1 : MoveSupervisor
{
    void Start()
    {
        SetRange(0f, 0f, 30f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
