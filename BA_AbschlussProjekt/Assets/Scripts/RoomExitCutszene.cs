using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RoomExitCutszene : MonoBehaviour
{
    [SerializeField]
    private GameObject lookAtObj;
    private GameObject playerObject;
    private bool fixLook;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<RigidbodyFirstPersonController>().enabled = false;
            playerObject = other.gameObject;
            fixLook = true;
            GetComponent<Animation>().Play();
            StartCoroutine(deleteCam());
        }
    }

    private IEnumerator deleteCam()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.enabled = false;
    }

    private void Update()
    {
        if (fixLook)
        {
            playerObject.GetComponentInParent<Transform>().LookAt(lookAtObj.transform.position);
        }
    }
}
