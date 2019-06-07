using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : BaseInteractable
{
    [SerializeField]
    private bool hasFlashback;
    [SerializeField]
    private GameObject flashBackImage;
    [SerializeField]
    private Sprite flashBackSprite;


    public override bool Interact(InteractionScript interactionScript)
    {
        if(hasFlashback &&  this.enabled)
        {
            flashBackImage.GetComponent<Image>().sprite = flashBackSprite;
            flashBackImage.SetActive(true);
            flashBackImage.GetComponent<ImageClosure>().open();
            this.enabled = false;
            return true;
        }
        else if(gameObject.name == "pre_Commode" && this.enabled)
        {
            GetComponent<Animation>().Play();
            this.enabled = false;
            return true;
        }


        return false;
    }
}
