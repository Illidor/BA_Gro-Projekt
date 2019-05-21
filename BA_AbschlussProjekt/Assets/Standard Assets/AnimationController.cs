using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AnimationController : MonoBehaviour
{
    private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();   
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Forward", controller.Velocity.magnitude);
    }
}
