using UnityEngine;
using System;
using System.Collections;

public class Crutch : GrabInteractable
{
    [SerializeField]
    private float reachIncreaseOnCarry = 1;

    [SerializeField] GameObject crutchChild;

    public override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        GetComponent<Sound>().PlaySound(0);

        return base.CarryOutInteraction_Carry(player); 
    }

    public override void PutDown(InteractionScript player)
    {
        player.ResetReachToDefault();
        base.PutDown(player);
        GetComponent<Sound>()?.PlaySound(0);
    }

    private void DisableAndReenable()
    {
        StartCoroutine(EnableAfterDelay());
    }

    private IEnumerator EnableAfterDelay()
    {
        crutchChild.SetActive(false);

        yield return new WaitForSeconds(5f);

        crutchChild.SetActive(true);
    }

    private void OnEnable()
    {
        HatchInteraction.PlayCrutchAnim += DisableAndReenable;
    }

    private void OnDisable()
    {
        HatchInteraction.PlayCrutchAnim -= DisableAndReenable;
    }
}
