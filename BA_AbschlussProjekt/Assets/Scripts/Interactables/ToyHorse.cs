using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyHorse : BaseInteractable {

    private bool isMoving = false;
    private float interactionTicker = 0f;
    private float interactionThreshold = 2f;

    public override bool CarryOutInteraction(InteractionScript player) {
        if (interactionTicker > interactionThreshold) {
            interactionTicker = 0f;
            isMoving = true;
        }

        return true;
    }

    private void Update() {
        if(isMoving == false) {
            interactionTicker += Time.deltaTime;
        }
    }
}
