using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SavePathSelection : MonoBehaviour
{
    public bool enableRogueLike = false;

    private GameObject levelGameObject;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            levelGameObject = GameObject.Find("Level");
            levelGameObject.GetComponent<SelectPuzzlePath>().SelectPath = enableRogueLike;
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
