using System;
using UnityEngine;

using System.Diagnostics;

public class RunPython : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Process psi = new Process();
            psi.StartInfo.FileName = "C:/Users/VML/AppData/Local/Programs/Python/Python37/python.exe";
            psi.StartInfo.Arguments = "c:/Users/VML/Desktop/21-2/Lectures/Game_Design_Project/Evoker_dev/dev/Assets/Scripts/Python/PyClient.py";
            psi.StartInfo.CreateNoWindow = true;
            psi.StartInfo.UseShellExecute = false;
            psi.Start();

            UnityEngine.Debug.Log("RunPython Success!");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("RunPython.cs Exception: " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

