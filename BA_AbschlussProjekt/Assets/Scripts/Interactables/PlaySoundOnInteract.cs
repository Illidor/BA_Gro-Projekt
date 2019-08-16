using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;

[RequireComponent(typeof(Sound))]
public class PlaySoundOnInteract : BaseInteractable
{
    public static event UnityAction<GameObject> BedRattle;

    private Sound soundToPlayOnInteract;


    private new void Awake()
    {
        soundToPlayOnInteract = GetComponent<Sound>();

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        soundToPlayOnInteract?.PlaySound(0);

        if(DisplayName == "Bed") {
            Tween.LocalRotation(transform, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 1f), 0.25f, 0f, Tween.EaseOut, Tween.LoopType.None, null, null, true);
            Tween.LocalRotation(transform, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -1f), 0.25f, 0.25f, Tween.EaseOut, Tween.LoopType.None, null, null, true);
            Tween.LocalRotation(transform, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f), 0.25f, 0.5f, Tween.EaseOut, Tween.LoopType.None, null, null, true);
            //if (BedRattle != null)
            //    BedRattle(gameObject);
        }
        return true;
    }
}
