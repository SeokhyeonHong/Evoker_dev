using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

[CustomEditor(typeof(PythonManager))]
public class PythonManager_Editor : Editor
{
    PythonManager pythonManager;
    private void OnEnable()
    {
        pythonManager = (PythonManager)target;
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Launch", GUILayout.Height(35)))
        {
            string path = Application.dataPath + "/Python/PyClient.py";
            PythonRunner.RunFile(path);
        }
    }
}
