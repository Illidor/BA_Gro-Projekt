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

        PlayerAnimationEvents.instance.PlayAnimation("DropDownLadder");
        PlayerAnimationEvents.instance.SnapPlayerToTargetPosition(startPosition);
        player.transform.position = endPosition.position;



        yield return new WaitForSeconds(24.5f);
        playerController.freezePlayerCamera = false;
        playerController.freezePlayerMovement = false;

    }
}
