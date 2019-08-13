using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropFromAttic : MonoBehaviour
{
    public static event UnityAction PlayerDroppedFromAttic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if(other.GetComponentInParent<Rigidbody>().velocity.y < -8f)
            {
                PlayerDroppedFromAttic?.Invoke();
            }
        }
    }
}
