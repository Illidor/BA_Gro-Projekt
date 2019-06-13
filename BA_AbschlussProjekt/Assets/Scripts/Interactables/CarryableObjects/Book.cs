using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Book : GrabInteractable, IUseable
{
    [SerializeField] GameObject bookToRead;

    private bool isBookOpened = false;

    public bool Use(InteractionScript player)
    {
        if(!isBookOpened)
        {
            bookToRead.SetActive(true);
            isBookOpened = true;

            return true;
        }
        else
        {
            bookToRead.SetActive(false);
            isBookOpened = false;
        }
        return true;
    }
}
