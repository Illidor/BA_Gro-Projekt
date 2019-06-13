using System;
using UnityEngine;

/// <summary>
/// Central input handling
/// </summary>
public class CTRLHub : MonoBehaviour
{
    // Key to press for interacting with interactable Objects. This means picking up, grabbing, combining and using
    private static KeyCode interactKey = KeyCode.Mouse0;
    // Key to drop a carried object
    private static KeyCode dropKey = KeyCode.Mouse1;


    public static bool Interact { get { return Input.GetKey(interactKey); } }
    public static bool InteractDown { get { return Input.GetKeyDown(interactKey); } }
    public static bool InteractUp { get { return Input.GetKeyUp(interactKey); } }

    public static bool Drop { get { return Input.GetKey(dropKey); } }
    public static bool DropDown { get { return Input.GetKeyDown(dropKey); } }
    public static bool DropUp { get { return Input.GetKeyUp(dropKey); } }

    public static float HorizontalAxis { get { return Input.GetAxis("Horizontal"); } }

    public static float VerticalAxis { get { return Input.GetAxis("Vertical"); } }


    private void Awake()
    {
        interactKey = ParseKeyCode("Fire1", "Mouse0");
        dropKey    = ParseKeyCode("Fire2", "Mouse1");
    }

    private KeyCode ParseKeyCode(string internalName, string keyCodeName)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(internalName, keyCodeName));
    }
}
