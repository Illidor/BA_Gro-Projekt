using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ShakeLamp : MonoBehaviour
{
    private void Start() {
        Tween.LocalRotation(transform, Quaternion.Euler(4f, 0f, 0f), 7f, Random.Range(0f, 5f), Tween.EaseInOut, Tween.LoopType.PingPong, null, null, true);
    }
}
