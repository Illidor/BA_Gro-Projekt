using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DropZoneTrigger : MonoBehaviour
{

    private bool hasPlayerEntered = false;

    [SerializeField] Camera mainCam;
    [SerializeField] Camera blackScreen;

    [SerializeField] Sound headbump;
    [SerializeField] Sound breakingBones;
    [SerializeField] Sound bodyonfloor;

    [SerializeField] GameObject player;
    [SerializeField] Transform endPosition;

    private RigidbodyFirstPersonController playerController;

    // Start is called before the first frame update
    void Start()
    {
            GetComponent<BoxCollider>().enabled = false;
        playerController = player.GetComponent<RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayerEntered)
        {
            hasPlayerEntered = true;
            StartCoroutine(PlayerDropping());
        }
    }

    private IEnumerator PlayerDropping()
    {
        playerController.freezePlayerCamera = true;
        playerController.freezePlayerMovement = true;

        yield return new WaitForSeconds(0.3f);

        mainCam.enabled = false;
        blackScreen.enabled = true;
        headbump.PlaySound(0);

        yield return new WaitForSeconds(1.4f);
        bodyonfloor.PlaySound(0);
        breakingBones.PlaySound(0);
        player.transform.position = endPosition.position;

        yield return new WaitForSeconds(1.5f);
        mainCam.enabled = true;
        blackScreen.enabled = false;

        playerController.freezePlayerCamera = false;
        playerController.freezePlayerMovement = false;
    }
}
