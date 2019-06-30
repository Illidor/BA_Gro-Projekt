using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : MonoBehaviour
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

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        key.SetActive(false);
        CloseKeyBox();
    }

    public void OpenKeyBox()
    {
        key.SetActive(true);
        key.GetComponent<Rigidbody>().isKinematic = false;

        greenLampMaterial.SetColor("_EmissionColor", greenLampOnEmissionColor);
        redLampMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));

        GetComponent<Animator>().SetBool("open", true);

        lampInsideBox.SetActive(true);

        IsOpen = true;
    }

    public void CloseKeyBox()
    {
        greenLampMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));
        redLampMaterial.SetColor("_EmissionColor", redLampOnEmissionColor);

        GetComponent<Animator>().SetBool("open", false);

        lampInsideBox.SetActive(false);

        IsOpen = false;
    }
}
