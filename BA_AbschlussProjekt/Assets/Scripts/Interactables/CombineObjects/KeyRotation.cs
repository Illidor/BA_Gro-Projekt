using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class KeyRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tween.LocalRotation(transform, new Vector3(transform.localRotation.x + 180f,transform.localRotation.y,transform.localRotation.z), 1f, 0f);
    }
}
