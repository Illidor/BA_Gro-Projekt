using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageClosure : MonoBehaviour
{
    bool active = false;


    // Update is called once per frame
    void Update()
    {
        if (active && (CTRLHub.Interact || Input.GetKeyDown(KeyCode.Escape)))
        {
            this.gameObject.SetActive(false);
            active = false;
        }
    }

    public void open()
    {
        StartCoroutine(enableClose());
    }


    public IEnumerator enableClose()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }
}
