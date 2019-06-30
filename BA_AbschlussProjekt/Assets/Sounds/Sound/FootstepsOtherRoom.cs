using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsOtherRoom : MonoBehaviour
{
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
        yield return new WaitForSeconds(60f);

        if(Random.Range(0,2) == 1)
        {
            footsteps.Play();
        }
        isCorRunning = false;
    }
}
