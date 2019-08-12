using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCar : MonoBehaviour
{
    [SerializeField] Sound dropSound;

    public void CallSoundAfterDelay(float delay) {
        StartCoroutine(DelayRoutine(delay));
    }

    private IEnumerator DelayRoutine(float delay) {
        yield return new WaitForSeconds(delay);
        dropSound.PlaySound(1);
    }
}
