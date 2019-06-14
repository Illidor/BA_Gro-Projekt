using System.Collections;

/// <summary>
/// Used to make an GrabInteractable in the players hand useable
/// </summary>
public interface IUseable
{
    bool Use(InteractionScript player);
}
