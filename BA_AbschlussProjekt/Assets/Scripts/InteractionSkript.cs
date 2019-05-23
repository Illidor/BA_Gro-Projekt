using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[Obsolete]
public class InteractionSkript : MonoBehaviour
{
    private bool mouseButtonClicked = false;
    private GameObject grabbedObject;
    public GameObject grabingPoint;
    public float grabbDistance;
    public float throwStrengh;


 
    void Update()   // Update is called once per frame
    {
        if(grabbedObject != null)
        {
            grabbedObject.transform.localPosition = new Vector3(0, 0, 0);
            grabbedObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            mouseButtonClicked = true;
        }

        if (mouseButtonClicked && CrossPlatformInputManager.GetButtonUp("Fire1") && grabbedObject == null)
        {
            mouseButtonClicked = false;
            grabObject();
        }
        else if(mouseButtonClicked && grabbedObject != null)
        {
            mouseButtonClicked = false;
            dropObject();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2") && grabbedObject != null)
        {

        }
        else if (CrossPlatformInputManager.GetButtonUp("Fire2") && grabbedObject != null)
        {
            throwObject();
        }
    }

    private void throwObject()
    {
        grabbedObject.transform.SetParent(null);

        Vector3 throwDirection = gameObject.transform.localEulerAngles;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        grabbedObject.GetComponent<Rigidbody>().AddForce(ray.direction*throwStrengh , ForceMode.Impulse);

        Debug.Log(ray.direction);

        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.GetComponent<Collider>().isTrigger = false;
        grabbedObject = null;
    }

    private void dropObject()
    {
        grabbedObject.transform.SetParent(null);
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.GetComponent<Collider>().isTrigger = false;
        grabbedObject = null;
    }

    private void grabObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, grabbDistance))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hit.collider.gameObject.GetComponent<Collider>().isTrigger = true;
                hit.collider.gameObject.transform.SetParent(grabingPoint.transform);

                grabbedObject = hit.collider.gameObject;
            }
        }
    }


}
