using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLampScript : MonoBehaviour
{
    public Light UVVisible;
    public Light UVLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int randomNumber = Random.Range(1, 100);
        
        if(randomNumber > 90)
        {
            UVVisible.enabled = !UVLight.enabled;
            UVLight.enabled = !UVLight.enabled;
        }
    }
}
