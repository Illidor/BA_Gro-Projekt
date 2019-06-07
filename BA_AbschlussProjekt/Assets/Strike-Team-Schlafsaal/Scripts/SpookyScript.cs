using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject spookyDude;
    [SerializeField]
    private GameObject jesusLight;
    [SerializeField]
    private GameObject spookyMachine;
    [SerializeField]
    private float spookyDelay;
    private float spookyCounter;

    private Light light;

    bool firstTimePianoActive = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if(light.enabled == true)
        {
            spookyDude.SetActive(false);
            spookyMachine.SetActive(true);
            jesusLight.SetActive(true);
        }
        else
        {
            spookyDude.SetActive(true);
            spookyMachine.SetActive(false);
            jesusLight.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if(other.tag == "Player")
        {
            if(spookyCounter > spookyDelay)
            {
                float random = Random.Range(0f, 100f);


                if (random > 55)
                {
                    light.enabled = false;

                    if (!firstTimePianoActive)
                    {
                        firstTimePianoActive = true;
                        GetComponent<AudioSource>().PlayDelayed(0.5f);
                    }
                }
                else
                {
                    light.enabled = true;


                }
                spookyCounter = 0;
            }
            else
            {
                spookyCounter += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            light.enabled = true;
        }
    }
}
