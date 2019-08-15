using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    bool isFilling = true;


    // Update is called once per frame
    void Update()
    {

        if (isFilling)
        {
            if (gameObject.GetComponent<Image>().fillAmount >= 0.95f)
            {
                isFilling = false;
            }
            else
            {
                gameObject.GetComponent<Image>().fillAmount += .65f * Time.deltaTime;
            }
        }
        else
        {
            if (gameObject.GetComponent<Image>().fillAmount <= 0.05f)
            {
                isFilling = true;
            }
            else
            {
                gameObject.GetComponent<Image>().fillAmount -= .85f * Time.deltaTime;
            }
        }

        gameObject.transform.Rotate(0, 0, -200 * Time.deltaTime);
    }
}
