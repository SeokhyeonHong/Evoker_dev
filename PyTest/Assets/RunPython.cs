using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;

public class RunPython : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            try
            {
                Process psi = new Process();
                psi.StartInfo.FileName = "C:\\Users\\VML\\AppData\\Local\\Programs\\Python\\Python37\\python.exe";
                psi.StartInfo.Arguments = "C:\\Users\\VML\\Desktop\\21-2\\Lectures\\Game_Design_Project\\Evoker_dev\\PyTest\\Assets\\Python\\PyClient.py";
                psi.StartInfo.CreateNoWindow = true;

                psi.StartInfo.UseShellExecute = false;
                psi.Start();

                UnityEngine.Debug.Log("py file running");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("error: " + e.Message);
            }
        }
    }
}

