using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : BaseInteractable
{
    public bool used = false;

    private Animation parentAnimaton;
    [SerializeField]
    private Toolbox brother;


    [SerializeField]
    private AnimationClip close;
    [SerializeField]
    private AnimationClip open;

    [SerializeField] Sound openSound;
    [SerializeField] Sound closeSound;

    private void Awake()
    {
        parentAnimaton = transform.parent.GetComponent<Animation>();
    }


    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (!used && !brother.used)
        {
            used = true;
            parentAnimaton.GetComponent<Animation>().clip = open;
            parentAnimaton.GetComponent<Animation>().Play();
            openSound.PlaySound(0);
            return true;
        }
        else
        {
            used = false;
            brother.used = false;
            parentAnimaton.GetComponent<Animation>().clip = close;
            parentAnimaton.GetComponent<Animation>().Play();
            closeSound.PlaySound(0);
        }

        return false;
    }
}
