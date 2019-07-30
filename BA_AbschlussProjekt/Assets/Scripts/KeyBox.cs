using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : BaseInteractable
{
    [SerializeField]
    private Color greenLampOnEmissionColor;
    [SerializeField]
    private Color redLampOnEmissionColor;
    [Space]
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private Material greenLampMaterial;
    [SerializeField]
    private Material redLampMaterial;
    [SerializeField]
    private GameObject lampInsideBox;
    [SerializeField]
    private Sound keyDropSound;
    [SerializeField]
    private Sound interactSound;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        key.SetActive(false);

        if (keyDropSound == null)
            keyDropSound = GetComponent<Sound>();

        CloseKeyBox();
        base.Awake();
    }

    public void CloseKeyBox()
    {
        greenLampMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));
        redLampMaterial.SetColor("_EmissionColor", redLampOnEmissionColor);

        GetComponent<Animator>().SetBool("open", false);

        lampInsideBox.SetActive(false);

        IsOpen = false;
    }

    public void OpenKeyBox()
    {
        key.SetActive(true);
        key.GetComponent<Rigidbody>().isKinematic = false;

        greenLampMaterial.SetColor("_EmissionColor", greenLampOnEmissionColor);
        redLampMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));

        GetComponent<Animator>().SetBool("open", true);

        lampInsideBox.SetActive(true);

        keyDropSound?.PlaySound(0);

        IsOpen = true;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        Debug.Log("use Keybox");
        //interactSound?.PlaySound(1);//Todo:activate with right numbers after sound is there
        //VoiceLines.instance.PlayVoiceLine(13, 1f);
        return true;
    }
}
