using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        BuildPipeline.BuildPlayer(scenes, "./" + name + "_Windows/" + name+".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
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
