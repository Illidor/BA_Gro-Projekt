using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

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

        BuildPipeline.BuildPlayer(scenes, "C:/Users/AB/Dropbox/" + System.DateTime.Today.ToString() + "/" + name+".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
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
