using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Carryable
{
    private Vector3 startPosition;
    public Transform readingSpot;
    private Vector3 endPosition;

    private void Start()
    {
        endPosition = readingSpot.position;
    }
    public override bool Use()
    {
        base.Use();
        startPosition = transform.position;
        Debug.Log("Used");
        return true;
    }

}
