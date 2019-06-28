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

    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        Debug.Log("Interacted");
        StartCoroutine(showFLashback(player, condition, minCondition));
        return true;
    }
    
    private IEnumerator showFLashback(InteractionScript player, Conditions condition, float minCondition)
    {
        foreach (Sprite item in flashbackSprites)
        {
            image.enabled = true;
            player.UsedObject = this.gameObject.GetComponent<GrabInteractable>();
            player.GUIInteractionFeedbackHandler.RemoveGUI();
            image.sprite = item;
            yield return new WaitForSeconds(flashbackTimer);
            image.enabled = false;
            player.UsedObject = null;
            player.GUIInteractionFeedbackHandler.ResetGUI();
        }

        secondInteraction?.Interact(player, condition, minCondition);

        yield return null;
    }
}
