using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricBracelet : MonoBehaviour
{
    public static event UnityAction DropItemAfterBeingShocked;
    public static event UnityAction PlayerDied;

    private int maxHealth = 5;
    private int currentHealth;
    private int shockCount = 0;

    // SFX
    [SerializeField] Sound screamSound;
    [SerializeField] Sound heartBeatSound;
    [SerializeField] Sound elctricShockSound;
    [SerializeField] Sound breathingSound;

    [Space]
    [SerializeField] MeshRenderer[] braceletLamps;
    [SerializeField] Material disabledLampMaterial;

    [Space]
    [SerializeField] Animator anim;
    private PlayerHealth playerHealth;

    void Start()
    {
        currentHealth = maxHealth;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            GetShockDamage();
        }
    }

    private void GetShockDamage() {
        if(currentHealth > 0) {
            // Disable anim triggers
            PlayerAnimationEvents.instance.DisableTrigger("TryOpenWindow");

            // Disable Bracelet Lamp
            braceletLamps[currentHealth - 1].material = disabledLampMaterial;

            currentHealth -= 1;
            // Enables Feedback to current shock count like screams, fades and heartbeat sfx
            ShockFeedback();
        }
    }

    private void ShockFeedback() {
        shockCount++;
        elctricShockSound.PlaySound(0);
        // Shock Event called, will be used in Interaction for Player to Drop current Items
        DropItemAfterBeingShocked?.Invoke();

        switch (shockCount) {
            case 1:
                // Intro Shock
                anim.SetTrigger("Shocked");
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 2:
                // First Shock just Screaming
                anim.SetTrigger("Shocked");
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 3:
                // Fade and Feedback that the shocks harm the Player long term
                anim.SetTrigger("Shocked");
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 4:
                // Intense Feedback, This is the last Shock before you DIE! Be careful now!
                anim.SetTrigger("Shocked");
                screamSound.PlaySound(0);
                heartBeatSound.PlaySound(0);
                break;
            case 5:
                // Player Dead
                heartBeatSound.PlaySound(0);
                PlayerDied?.Invoke();
                playerHealth.activateRagdoll(true);
                break;
        }
    }

    private void OnEnable() {
        Window.ShockPlayer += GetShockDamage;
        KeyBox.ShockPlayer += GetShockDamage;
        WardrobeDoor.ShockPlayer += GetShockDamage;
        WardrobeBack.ShockPlayer += GetShockDamage;
    }

    private void OnDisable() {
        Window.ShockPlayer -= GetShockDamage;
        KeyBox.ShockPlayer -= GetShockDamage;
        WardrobeDoor.ShockPlayer -= GetShockDamage;
        WardrobeBack.ShockPlayer -= GetShockDamage;
    }
}
