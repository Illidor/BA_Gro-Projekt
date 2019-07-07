using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePoses : MonoBehaviour
{
    private Material[] materials;

    private float lerpTimer = 0f;

    private bool fadeIn = true;
    private bool fadeOutCalled = false;

    public bool canFadeOut = true;

    // Start is called before the first frame update
    void Start()
    {
        materials = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTimer += 25f * Time.deltaTime;


        if (lerpTimer >= 1f && fadeOutCalled == false)
        {
            fadeOutCalled = true;
            StartCoroutine(DelayFadeOut());
        }

        foreach (Material material in materials)
        {
            if(fadeIn)
            {
                //material.color = Color.Lerp( transparentColor, visibleColor, lerpTimer);
                material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a + 0.1f);
            }
            else
            {
                if(canFadeOut)
                {
                    material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - 0.1f);

                    if (material.color.a <= 0.1)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private IEnumerator DelayFadeOut()
    {
        yield return new WaitForSeconds(1f);
        fadeIn = false;
        lerpTimer = 0f;
    }
}
