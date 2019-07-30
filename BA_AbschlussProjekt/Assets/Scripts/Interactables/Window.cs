using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : BaseInteractable
{
    [SerializeField] Sound windowSounds;

    private float interactionTicker = 0f;
    private float interactionThreshold = 4f;

    public override bool CarryOutInteraction(InteractionScript player) {
        if(interactionTicker > interactionThreshold) {
            interactionTicker = 0f;
            windowSounds.PlaySound(Random.Range(0, windowSounds.clips.Count));
        }

        return true;
    }

    private void Update() {
        interactionTicker += Time.deltaTime;
    }
}
