using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : MonoBehaviour, ICombinable        // I see similarities with PictureInteraction
{
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string objectToInteractWith;

    protected AudioManager audioManager;
    [SerializeField]
    protected string interactSound;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            
            GetComponent<Animator>().SetTrigger("open");
            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);
            return true;
        }
        return false;
    }
    protected void PlaySound(string soundType)
    {
        if (GetComponent<AudioSource>() == null)
        {
            audioManager.AddSound(soundType, this.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
