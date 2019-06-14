﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : MonoBehaviour, ICombinable
{
    public List<BaseInteractable> thingsToInteractWtih;
    public List<GameObject> correlatingGameObjects;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        foreach (BaseInteractable interactable in thingsToInteractWtih)
        {
            if(interactingComponent.name == interactable.name)
            {
                try
                {
                    this.gameObject.GetComponent<Animation>().Play();
                }
                catch (System.Exception){}



                foreach (GameObject cgO in correlatingGameObjects)
                {
                    try
                    {
                        cgO.GetComponent<Animation>().Play();
                    }
                    catch (System.Exception) { }
                }

            }
            else
            {
                Debug.Log("Wrong GO");
            }
        }

        return false;
    }

}
