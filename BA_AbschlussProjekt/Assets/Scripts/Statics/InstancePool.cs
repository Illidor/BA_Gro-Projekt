using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singelton to parent all instances to that just need to be parented to global
/// </summary>
public class InstancePool : MonoBehaviour
{
    public static new Transform transform;

    private void Awake()
    {
        transform = base.transform;
    }
}
