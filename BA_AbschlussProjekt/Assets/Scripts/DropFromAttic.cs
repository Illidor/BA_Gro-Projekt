using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropFromAttic : MonoBehaviour
{
    public static event UnityAction PlayerDroppedFromAttic;

    [SerializeField] Sound impactSound;
    [SerializeField] Sound breakingBonesSound;
    [SerializeField] Sound scream;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if(other.GetComponentInParent<Rigidbody>().velocity.y < -8f)
            {
                PlayerDroppedFromAttic?.Invoke();
                impactSound.PlaySound(0);
                scream.PlaySound(0);
            }
        }
    }
}
