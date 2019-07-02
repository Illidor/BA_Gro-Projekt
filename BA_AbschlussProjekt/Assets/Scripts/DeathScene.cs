using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour
{
    public List<GameObject> playerPoses = new List<GameObject>();

    private void PlayerDied()
    {
        StartCoroutine(DelayPositionSwap());
    }

    private IEnumerator DelayPositionSwap()
    {
        for (int i = 0; i < playerPoses.Count; i++)
        {
            if(i != 0)
            {
                playerPoses[i].SetActive(true);
                playerPoses[i - 1].SetActive(false);
            }

            yield return new WaitForSeconds(2.5f);
        }
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDied -= PlayerDied;
    }

}
