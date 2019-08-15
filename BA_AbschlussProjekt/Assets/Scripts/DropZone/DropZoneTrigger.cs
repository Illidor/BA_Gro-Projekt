using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DropZoneTrigger : MonoBehaviour
{

    private bool hasPlayerEntered = false;

    [SerializeField] Sound plankInFace;
    [SerializeField] Sound breakingBones;
    [SerializeField] Sound bodyonfloor;
    [SerializeField] Sound heavyBreathing;
    [SerializeField] Sound scream;


    [SerializeField] GameObject player;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    private RigidbodyFirstPersonController playerController;

    private InteractionScript interactionScript;

    private bool isPictureCarried = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<RigidbodyFirstPersonController>();

        interactionScript = player.GetComponent<InteractionScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!isPictureCarried && interactionScript.UsedObjectName == "Picture")
        {
            isPictureCarried = true;
        }
        else if(isPictureCarried && interactionScript.UsedObjectName == null)
        {
            isPictureCarried = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayerEntered && isPictureCarried)
        {
            hasPlayerEntered = true;
            StartCoroutine(PlayerDropping());
        }
    }

    private IEnumerator PlayerDropping()
    {
        yield return null;
        playerController.freezePlayerCamera = true;
        playerController.freezePlayerMovement = true;

        PlayerAnimationEvents.instance.PlayAnimation("DropDownLadder");
        PlayerAnimationEvents.instance.SnapPlayerToTargetPosition(startPosition);
        player.transform.position = endPosition.position;

        yield return new WaitForSeconds(16f);

        breakingBones.PlaySound(0);
        bodyonfloor.PlaySound(0);
        plankInFace.PlaySound(0);
        scream.PlaySound(0);

        yield return new WaitForSeconds(1.5f);
        heavyBreathing.PlaySound(0);

        yield return new WaitForSeconds(7f);
        playerController.freezePlayerCamera = false;
        playerController.freezePlayerMovement = false;

    }
}
