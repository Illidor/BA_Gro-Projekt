using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPoses = new List<GameObject>();
    [SerializeField]
    private float returnToMenuAfterSequenceEnded = 30f;
    [SerializeField]
    private AudioMixerSnapshot deathSnapshot;
    [SerializeField]
    private AudioMixerSnapshot defautlSnapshot;
    public GameObject player;

    private bool isSceneFinished = false;

    [SerializeField] AudioSource deadSceneMusic;
    //[SerializeField] AudioSource bodyOnFloor;

    private void Start()
    {
        defautlSnapshot.TransitionTo(0f);
    }

    private void PlayerDied()
    {
        player.SetActive(false);
        StartCoroutine(DelayPositionSwap());
        SwitchToDeathSnap();
        deadSceneMusic.Play();

        InstancePool.instance.GoBackToMainMenue(2f);
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

            // if (i == playerPoses.Count)  // makes no sense ~Robin
            //     bodyOnFloor.Play();

            yield return new WaitForSeconds(3f);
        }

        isSceneFinished = true;

        yield return new WaitForSeconds(returnToMenuAfterSequenceEnded);

        LoadSceneAfterDeath();
    }
    private void SwitchToDeathSnap()
    {
        deathSnapshot.TransitionTo(0f);
    }

    private void ChangeToDefault()
    {
        defautlSnapshot.TransitionTo(0f);
    }

#if UNITY_EDITOR
    private static void LoadSceneAfterDeath()
    {
        InstancePool.instance.GoBackToMainMenue(0f);
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
