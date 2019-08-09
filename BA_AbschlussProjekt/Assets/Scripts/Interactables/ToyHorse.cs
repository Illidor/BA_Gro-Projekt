using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyHorse : BaseInteractable {

    // isMoving is used to check when interaction is possible and handling the delay after the animation. It gets reseted to false from within the animation
    private bool isMoving = false;
    private float interactionTicker = 0f;
    private float interactionThreshold = 1.25f;

    private Animator anim;
    public Sound pushSound;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (isMoving == false) {
            interactionTicker += Time.deltaTime;
        }
    }

    public override bool CarryOutInteraction(InteractionScript player) {
        if (interactionTicker > interactionThreshold) {
            interactionTicker = 0f;
            anim.SetTrigger("StartSeesaw");
            pushSound.PlaySound(0);
            isMoving = true;
        }
        return true;
    }

    public void StopMovement() {
        isMoving = false;
    }
}
