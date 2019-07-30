using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnCollision : MonoBehaviour
{
    private float time;

    private void Awake()
    {
        time = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (time > 7.5f)
            return;

        Destroy(gameObject);
    }
}
