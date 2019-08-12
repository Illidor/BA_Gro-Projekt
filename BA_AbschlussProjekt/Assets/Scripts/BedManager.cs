using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    public GameObject toyCar;
    private int rattleCount = 0;
    [SerializeField] Transform player;

    private void BedRattle(GameObject bed) {
        rattleCount++;
        if(rattleCount == 3) {
            GameObject tmpCar = Instantiate(toyCar, player.position + Vector3.up + player.forward * 0.5f, Quaternion.identity);
            tmpCar.GetComponent<ToyCar>().CallSoundAfterDelay(0.5f);
        }
    }

    private void OnEnable() {
        PlaySoundOnInteract.BedRattle += BedRattle;
    }

    private void OnDisable() {
        PlaySoundOnInteract.BedRattle -= BedRattle;
    }
}
