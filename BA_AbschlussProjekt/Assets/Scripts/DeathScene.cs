using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPoses = new List<GameObject>();
    [SerializeField]
    private float returnToMenuAfterSequenceEnded = 30f;

    private bool isSceneFinished = false;

    private void PlayerDied()
    {
        StartCoroutine(DelayPositionSwap());
    }

    private void Update()
    {
        if (isSceneFinished == false)
            return;

        if (CTRLHub.DropDown)
            LoadSceneAfterDeath();
    }

    private IEnumerator DelayPositionSwap()
    {
        for (int i = 0; i < playerPoses.Count; i++)
        {
                playerPoses[i].SetActive(true);

            yield return new WaitForSeconds(2f);
        }

        isSceneFinished = true;

        yield return new WaitForSeconds(returnToMenuAfterSequenceEnded);

        LoadSceneAfterDeath();
    }

#if UNITY_EDITOR
    private static void LoadSceneAfterDeath()
    {
        SceneManager.LoadScene(1);
    }
#else
    private static void LoadSceneAfterDeath()
    {
        SceneManager.LoadScene(0);
    }
#endif

    private void OnEnable()
    {
        PlayerHealth.PlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDied -= PlayerDied;
    }

}
