using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashbackInteraction : BaseInteractable
{
    public Image image;
    public List<Sprite> flashbackSprite;
    public Carryable secondInteraction;

    public float flashbackTimer;


    public override bool Combine(GameObject gameObject)
    {
        return false;
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        Debug.Log("Interacted");
        StartCoroutine(showFLashback(interactionScript));
        return true;
    }

    private IEnumerator showFLashback(InteractionScript interactionScript)
    {
        foreach (var item in flashbackSprite)
        {
            image.enabled = true;
            image.sprite = item;
            yield return new WaitForSeconds(flashbackTimer);
            image.enabled = false;
        }

        if(secondInteraction != null)
        {
            secondInteraction.Interact(interactionScript);
        }
        yield return new WaitForFixedUpdate();
    }
}
