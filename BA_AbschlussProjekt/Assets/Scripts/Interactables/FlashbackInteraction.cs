using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashbackInteraction : BaseInteractable
{
    [SerializeField]
    private float flashbackTimer;
    [SerializeField]
    private List<Sprite> flashbackSprites;
    [SerializeField]
    private Image image;
    [SerializeField]
    public BaseInteractable secondInteraction;

    public override bool Interact(InteractionScript interactionScript)
    {
        Debug.Log("Interacted");
        StartCoroutine(showFLashback(interactionScript));
        return true;
    }
    
    private IEnumerator showFLashback(InteractionScript interactionScript)
    {
        foreach (Sprite item in flashbackSprites)
        {
            image.enabled = true;
            image.sprite = item;
            yield return new WaitForSeconds(flashbackTimer);
            image.enabled = false;
        }

        secondInteraction?.Interact(interactionScript);

        yield return null;
    }
}
