using System;
using System.IO;
using UnityEngine;

using System.Diagnostics;

public class RunPython : MonoBehaviour
{
    Process psi = new Process();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        try
        {
            psi.StartInfo.FileName = "python"; // "C:/Users/VML/AppData/Local/Programs/Python/Python37/python.exe";
            psi.StartInfo.Arguments = Directory.GetCurrentDirectory() + "\\PyClient.py";// "C:/Users/VML/Desktop/21-2/Lectures/Game_Design_Project/Evoker_dev/dev/Assets/Scripts/Python/PyClient.py";
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

    void OnDestroy()
    {
        try
        {
            psi.Kill();
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log("RunPython OnDestroy(): " + e.ToString());
        }
    }
}

