using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light flickerLight;
    private float ticker = 0f;
    private float threshold = 5f;

    private void Start() {
        threshold = Random.Range(6f, 20f);
    }

    void Update()
    {
        ticker += Time.deltaTime;
        
        if(ticker > threshold) {
            ticker = 0f;
            StartCoroutine(FlickerRoutine());
            threshold = Random.Range(8f, 22f);
        }
    }

    private IEnumerator FlickerRoutine() {
        for (int i = 0; i < Random.Range(3, 5); i++) {
            flickerLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
            flickerLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
        }
        flickerLight.enabled = true;
    }
}
