using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutPlank : BaseInteractable
{
    [SerializeField] Sound plankSound;

    private float crackSoundTicker = 10f;
    private float crackSoundThreshold = 2f;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        plankSound.PlaySound(0);
        Destroy(gameObject, 0.75f);
        return true;
    }

    private void Update()
    {
        crackSoundTicker += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player walks over the plank play a crack sound but limit it to only play every 2 secs max
        if (other.CompareTag("Player"))
        {
            if (plankSound.clips.Count > 1 && crackSoundTicker > crackSoundThreshold)
            {
                plankSound.PlaySound(1);
                crackSoundTicker = 0f;
            }
        }
    }

}
