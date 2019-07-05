using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsOtherRoom : MonoBehaviour
{
    [SerializeField]
    private  float testForSoundToPlayIntervallInSeconds = 60f;
    [SerializeField]
    private float percentageToPlaySound = 50f;
    private AudioSource footsteps;

    private bool isCorRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        footsteps = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isCorRunning)
            StartCoroutine(FootstepDelay());
    }

    private IEnumerator FootstepDelay()
    {
        isCorRunning = true;
        yield return new WaitForSeconds(testForSoundToPlayIntervallInSeconds);

        if(Random.Range(0, 100) < percentageToPlaySound)
        {
            footsteps.Play();
        }
        isCorRunning = false;
    }
}
