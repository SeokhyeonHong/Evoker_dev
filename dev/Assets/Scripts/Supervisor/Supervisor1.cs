using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor1 : SupervisorController
{
    void Start()
    {
        SetRange(0f, 0f, 30f, 10f);
    }

    void Update()
    {
        Move();
        DetectGameEnd();
    }
}
