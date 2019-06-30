using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneHandle : MonoBehaviour
{
    public GameObject playerFPS;

    public GameObject crutchCutSceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartCrutchCutScene()
    {
        playerFPS.SetActive(false);
        crutchCutSceneCamera.SetActive(true);
        StartCoroutine(CrutchCutScene());
    }

    private IEnumerator CrutchCutScene()
    {
        yield return new WaitForSeconds(2.5f);
        playerFPS.SetActive(true);
        crutchCutSceneCamera.SetActive(false);
    }

    private void OnEnable()
    {
        FlashbackInteraction.StartFlashBack += StartCrutchCutScene;
    }

    private void OnDisable()
    {
        FlashbackInteraction.StartFlashBack -= StartCrutchCutScene;
    }
}
