using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DropZoneTrigger : MonoBehaviour
{

    private bool hasPlayerEntered = false;

    [SerializeField] Sound headbump;
    [SerializeField] Sound breakingBones;
    [SerializeField] Sound bodyonfloor;

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
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if(other.CompareTag("Player") && !hasPlayerEntered && isPictureCarried)
        {
            Debug.Log("playerCarriesPicture");
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



        yield return new WaitForSeconds(24.5f);
        playerController.freezePlayerCamera = false;
        playerController.freezePlayerMovement = false;

    }
}
