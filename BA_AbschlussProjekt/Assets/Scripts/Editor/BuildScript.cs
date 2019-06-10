using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using System.Globalization;

public class BuildScript
{
    static string[] scenes = { "Assets/Scenes/Prototyp-Erzieher.unity" };
    static string name = "ReleaseBuild";

    [MenuItem("Build/Build Windows")]
    static void BuildWindows()
    {
        string dateTime = System.DateTime.Now.ToString();
        dateTime = dateTime.Replace(":", "-");
        dateTime = dateTime.Replace("/", "_");
        string buildPath = "C:/Studium/Endprojekt_Material/Autodeploy/" + dateTime + "/";

        if (!Directory.Exists(buildPath))
        {
            Directory.CreateDirectory(buildPath);
        }



        BuildPipeline.BuildPlayer(scenes, buildPath + name + "_v" + Application.version + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}