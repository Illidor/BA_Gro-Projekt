using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCrutchAnimation : MonoBehaviour
{

    [SerializeField] Animator anim;

    private void PlayOpenAnimation()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        anim.SetTrigger("OpenAttic");
        StartCoroutine(DelayDisable());
    }

    private IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(5f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HatchInteraction.PlayCrutchAnim += PlayOpenAnimation;
    }

    private void OnDisable()
    {
        HatchInteraction.PlayCrutchAnim -= PlayOpenAnimation;
    }
}
