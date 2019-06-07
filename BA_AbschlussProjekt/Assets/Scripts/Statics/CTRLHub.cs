using System;
using UnityEngine;

/// <summary>
/// Central input handling
/// </summary>
public class CTRLHub : MonoBehaviour
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


    private void Awake()
    {
        interactKey = ParseKeyCode("Fire1", "Mouse0");
        throwKey    = ParseKeyCode("Fire2", "Mouse1");
    }

    private KeyCode ParseKeyCode(string internalName, string keyCodeName)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(internalName, keyCodeName));
    }
}
