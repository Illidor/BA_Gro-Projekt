using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Book : Carryable
{
    [SerializeField] GameObject bookToRead;

    private bool isBookOpened = false;

    public override bool Use()
    {
        base.Use();
        if(!isBookOpened)
        {
            bookToRead.SetActive(true);
            isBookOpened = true;
        }
        else
        {
            bookToRead.SetActive(false);
            isBookOpened = false;
            base.isInUse = false;
        }
        return true;
    }

}
