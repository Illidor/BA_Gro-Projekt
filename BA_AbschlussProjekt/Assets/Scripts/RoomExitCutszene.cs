using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;

public class RoomExitCutszene : MonoBehaviour
{
    [SerializeField]
    private GameObject lookAtObj;
    private GameObject playerObject;

    [SerializeField] Camera blackScreen;
    [SerializeField] Camera mainCam;

    private PostProcessVolume mainCamProfile;

    private AudioSource hitSound;

    [SerializeField] Transform playerTrans;

    private BoxCollider trigger;

    private void Start()
    {
        hitSound = GetComponent<AudioSource>();
        trigger = GetComponent<BoxCollider>();
        mainCamProfile = mainCam.GetComponent<PostProcessVolume>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            trigger.enabled = false;
            other.gameObject.GetComponentInParent<RigidbodyFirstPersonController>().enabled = false;
            playerObject = other.gameObject;
            playerTrans.LookAt(lookAtObj.transform);
            StartCoroutine(deleteCam());

            if(Random.Range(0, 2) >= 1)
                VoiceLines.instance.PlayDillenVoiceLine(4, 0.8f);
            else
                VoiceLines.instance.PlayDillenVoiceLine(5, 0.8f);
        }
    }

    private IEnumerator deleteCam()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.2f);
        hitSound.Play();
        mainCam.enabled = false;
        blackScreen.enabled = true;

        InstancePool.instance.GoBackToMainMenue(2.7f);
    }
}
