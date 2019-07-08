using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public List<Light> pointLights = new List<Light>();

    private float lerptimer = 0f;

    private bool day;

    private bool isPlayerDead = false;
    [SerializeField]
    Color nightColor;
    [SerializeField] Color dayColor;

    // Update is called once per frame
    void Update()
    {
        if(isPlayerDead)
        {
            if (day)
                lerptimer += Time.deltaTime / 15f;
            else
                lerptimer -= Time.deltaTime / 7.5f;

            if (lerptimer >= 0.6f)
                day = false;

            if (lerptimer <= 0.3f)
                day = true;

            foreach (Light light in pointLights)
            {
                light.color = Color.Lerp(dayColor, nightColor, lerptimer);
            }
        }
    }

    private void PlayerDied()
    {
        isPlayerDead = true;
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
