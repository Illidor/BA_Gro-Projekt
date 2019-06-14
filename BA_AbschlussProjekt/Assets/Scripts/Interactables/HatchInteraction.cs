using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : BaseInteractable
{
    public List<GameObject> thingsToInteractWtih;
    public List<GameObject> correlatingGameObjects;



    public override bool Interact(InteractionScript interactionScript)
    {
       return false;
    }

    public override bool Combine(GameObject gameObject)
    {
        Debug.Log("Interacting with: " + gameObject.name);
        foreach (var gO in thingsToInteractWtih)
        {
            if(gameObject.name == gO.name)
            {
                try
                {
                    this.gameObject.GetComponent<Animation>().Play();
                }
                catch (System.Exception){}



                foreach (var cgO in correlatingGameObjects)
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

    public override bool Use()
    {
        return false;
    }
}
