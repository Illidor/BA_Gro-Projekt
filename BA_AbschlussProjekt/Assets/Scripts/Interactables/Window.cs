using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Window : BaseInteractable
{
    public static event UnityAction ShockPlayer;

    [SerializeField] Sound windowSounds;

    private float interactionTicker = 0f;
    private float interactionThreshold = 2f;

    private int interactionCount = 0;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        Debug.Log("asdasd");
        PlayerAnimationEvents.instance.PlayAnimation("TryOpenWindow");

        if (interactionTicker > interactionThreshold)
        {

            interactionTicker = 0f;
            interactionCount++;

            if(interactionCount >= 2) {
                ShockPlayer?.Invoke();
            }
            else 
            {
                windowSounds.PlaySound(Random.Range(0, windowSounds.clips.Count));
            }
        }

        return true;
    }

    private void Update()
    {
        interactionTicker += Time.deltaTime;
    }
}
