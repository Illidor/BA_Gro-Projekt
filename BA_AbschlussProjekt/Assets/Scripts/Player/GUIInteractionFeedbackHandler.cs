using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteractionFeedbackHandler : MonoBehaviour
{
    [field: LabelOverride("Standart Crosshair"), SerializeField, Tooltip("Reference to the standard crosshair")]
    public GameObject StandardCrosshair { get; private set; }

    [field: LabelOverride("Interaction Crosshair"), SerializeField, Tooltip("Reference to the crosshair displayed when an interaction is possible")]
    public GameObject InteractionCrosshair { get; private set; }

    [field: LabelOverride("Action Description"), SerializeField, Tooltip("Reference to the text used to display the description of an action")]
    public Text ActionDescription { get; set; }

    [field: LabelOverride("Second Action Description"), SerializeField, Tooltip("Reference to the text used to display the second description of an action")]
    public Text SecondActionDescription { get; set; }

    [field: LabelOverride("Interaction Symbol Hand"), SerializeField, Tooltip("Symbol of the hand used to indicate an interaction")]
    public GameObject InteractionSymbolHand { get; private set; }

    private bool stopResetingGUI = false;

    private void Awake()
    {
        ResetGUI();
    }

    public void ResetGUI()
    {
        if (stopResetingGUI)
            return;

        StandardCrosshair?.SetActive(true);
        InteractionCrosshair?.SetActive(false);
        ActionDescription.text = "";
        SecondActionDescription.text = "";
        InteractionSymbolHand?.SetActive(false);
    }

    public void RemoveGUI()
    {
        StandardCrosshair?.SetActive(false);
        InteractionCrosshair?.SetActive(false);
        ActionDescription.text = "";
        SecondActionDescription.text = "";
        InteractionSymbolHand?.SetActive(false);
    }

    private void DisableInteractionGUI()
    {
        stopResetingGUI = true;

        Destroy(StandardCrosshair);
        Destroy(InteractionCrosshair);
        Destroy(ActionDescription);
        Destroy(SecondActionDescription);
        Destroy(InteractionSymbolHand);
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDied += DisableInteractionGUI;
        PlayerHealth.PlayerDiedFirstPerson += DisableInteractionGUI;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDied -= DisableInteractionGUI;
        PlayerHealth.PlayerDiedFirstPerson -= DisableInteractionGUI;
    }
}
