using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class BuildScript
{
    static string[] scenes = { "Assets/Scenes/SampleScene.unity" };
    static string name = "MyGame";

    [MenuItem("Build/Build WebGL")]
    static void BuildWebGL()
    {
        BuildPipeline.BuildPlayer(scenes, "./" + name + "_Web/" + name, BuildTarget.WebGL, BuildOptions.None);
    }

    [MenuItem("Build/Build Windows")]
    static void BuildWindows()
    {
        scenes = new string[SceneManager.sceneCount];

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i).path.ToString();
        }

        //string projectPath = "C:/Users/AB/Dropbox/" + System.DateTime.Today.ToString() + "/";
        string projectPath = "C:/Users/MattManco/Desktop/Gro0projekt/Builds/" + System.DateTime.Today.Day.ToString() + "_" + System.DateTime.Today.Month.ToString() + "/";

        if(!Directory.Exists(projectPath))
        {
            Directory.CreateDirectory(projectPath);
        }

        BuildPipeline.BuildPlayer(scenes,  projectPath + name+".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("Build/Build Linux")]
    static void BuildLinux()
    {
        BuildPipeline.BuildPlayer(scenes, "./" + name + "_Linux/" + name, BuildTarget.StandaloneLinux64, BuildOptions.None);
    }

    [MenuItem("Build/Build All")]
    static void BuildAll()
    {
        BuildLinux();
        BuildWindows();
        BuildWebGL();
    }
}
