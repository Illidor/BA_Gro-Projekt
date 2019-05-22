using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central input handling
/// </summary>
public static class CTRLHub
{
    // Key to press for interacting with interactable Objects. Pressing it again on carriables will drop them without throwing.
    private static KeyCode interactKey = KeyCode.Mouse0;
    // Key to throw a carried object
    private static KeyCode throwKey = KeyCode.Mouse1;


    public static bool Interact { get { return Input.GetKey(interactKey); } }
    public static bool InteractDown { get { return Input.GetKeyDown(interactKey); } }
    public static bool InteractUp { get { return Input.GetKeyUp(interactKey); } }

    public static bool Throw { get { return Input.GetKey(throwKey); } }
    public static bool ThrowDown { get { return Input.GetKeyDown(throwKey); } }
    public static bool ThrowUp { get { return Input.GetKeyUp(throwKey); } }

    public static float HorizontalAxis { get { return Input.GetAxis("Horizontal"); } }

    public static float VerticalAxis { get { return Input.GetAxis("Vertical"); } }
}
