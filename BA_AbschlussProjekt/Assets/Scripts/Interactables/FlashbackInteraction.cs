using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Obsolete]
public class FlashbackInteraction : BaseInteractable
{
    public static event UnityAction StartFlashBack;

    [SerializeField]
    private float flashbackTimer;
    [SerializeField]
    private List<Sprite> flashbackSprites;
    [SerializeField]
    private Image image;
    [SerializeField]
    public BaseInteractable secondInteraction;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        Debug.Log("Interacted");
        StartCoroutine(showFLashback(player));
        return true;
    }
    
    private IEnumerator showFLashback(InteractionScript player)
    {
        foreach (Sprite item in flashbackSprites)
        {
            /* <code from MergeBranch>
               image.enabled = true;
               player.UsedObject = this.gameObject.GetComponent<GrabInteractable>();
               player.GUIInteractionFeedbackHandler.RemoveGUI();
               image.sprite = item;
               yield return new WaitForSeconds(flashbackTimer);
               image.enabled = false;
               player.UsedObject = null;
               player.GUIInteractionFeedbackHandler.ResetGUI();
               </code from MergeBranch> 
            */


            // <code from SpringerBranch>:

            //old logic
            //image.enabled = true;
            //image.sprite = item;
            //yield return new WaitForSeconds(flashbackTimer);
            //image.enabled = false;

            //cutscene logic
            StartFlashBack?.Invoke();
        }

        secondInteraction?.CarryOutInteraction(player);

        yield return null;
    }
}
