using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingDownTheAtticTrigger : MonoBehaviour
{
    private Rigidbody playerRb;
    private PlayerHealth playerHealthRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerRb = other.GetComponentInParent<Rigidbody>();
            playerHealthRef = other.GetComponentInParent<PlayerHealth>();
            if (playerRb.velocity.y <= -10f)
            {
                playerHealthRef.ChangeCondition(Conditions.LowerBodyCondition, 0.5f);
            }
        }
    }
}
