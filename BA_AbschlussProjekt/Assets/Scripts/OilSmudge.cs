using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSmudge : MonoBehaviour
{

    private Sound oilSmudgeStep;

    private float ticker = 0f;

    void Start()
    {
        oilSmudgeStep = GetComponent<Sound>();
    }

    private void Update() {
        ticker += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && ticker > 2f) {
            oilSmudgeStep.PlaySound(0);
            ticker = 0f;
        }
    }
}
