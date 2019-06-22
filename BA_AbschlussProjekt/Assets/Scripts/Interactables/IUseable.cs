using System.Collections;

/// <summary>
/// Used to make an GrabInteractable in the players hand useable
/// </summary>
public interface IUseable
{
    /// <summary>
    /// Used to use a given object with in the hand. Usually fired from "HandleUse"
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool Use(InteractionScript player);

    /// <summary>
    /// Call to make the players UI show the possible use via text and crosshair change. Should fire "Use" when necessary. Possible implementation in the IUseable file
    /// </summary>
    /// <param name="guiInteractionFeedbackHandler">Collection of references of GUI elements used for interaction feedback inside the gui. Should be present in the current "InteractionScript"</param>
    void HandleUse(InteractionScript player);
    /* Possible implementation:   
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to use " + DisplayName; //Inherit from InteractionFoundation to have access to DisplayName

        if (CTRLHub.InteractDown)
            Use(player);
    } */
}
