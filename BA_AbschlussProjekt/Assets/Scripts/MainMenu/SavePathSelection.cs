using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SavePathSelection : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private bool enableRogueLike = false;

    private GameObject levelGameObject;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle.isOn)
        {
            enableRogueLike = true;
        }
        else
        {
            enableRogueLike = false;
        }
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
