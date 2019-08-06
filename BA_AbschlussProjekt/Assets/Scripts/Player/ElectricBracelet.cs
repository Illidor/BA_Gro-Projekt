using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricBracelet : MonoBehaviour
{
    public static event UnityAction DropItemAfterBeingShocked;

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

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void GetShockDamage() {
        if(currentHealth > 0) {
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
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 2:
                // First Shock just Screaming
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 3:
                // Fade and Feedback that the shocks harm the Player long term
                screamSound.PlaySound(0);
                breathingSound.PlaySound(0);
                break;
            case 4:
                // Intense Feedback, This is the last Shock before you DIE! Be careful now!
                screamSound.PlaySound(0);
                heartBeatSound.PlaySound(0);
                break;
            case 5:
                // Player Dead
                break;
        }
    }

    private void OnEnable() {
        Window.ShockPlayer += GetShockDamage;
        KeyBox.ShockPlayer += GetShockDamage;
    }

    private void OnDisable() {
        Window.ShockPlayer -= GetShockDamage;
        KeyBox.ShockPlayer -= GetShockDamage;
    }
}
