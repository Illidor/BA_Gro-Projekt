using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyPicture : GrabInteractable
{
    [SerializeField]
    private GameObject uiImageOfItself;
    [SerializeField] [Tooltip("If not supplied, the script will use the parent of \"Ui Image Of Itself\" instead.")]
    private GameObject familyPictureCollectionParent;

    private void Start()
    {
        uiImageOfItself.SetActive(false);

        if (familyPictureCollectionParent == null)
            familyPictureCollectionParent = uiImageOfItself.transform.parent.gameObject;
        familyPictureCollectionParent.SetActive(false);

        textToDisplayOnHover = "Click to pick up " + DisplayName;
    }

    public override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        base.CarryOutInteraction_Carry(player);

        uiImageOfItself.SetActive(true);
        familyPictureCollectionParent.SetActive(true);

        return true;
    }

    public override void PutDown(InteractionScript player)
    {
        base.PutDown(player);

        familyPictureCollectionParent.SetActive(false);
        Destroy(gameObject);
    }
}