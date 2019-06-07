using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Carryable
{
    public override bool Use()
    {
        base.Use();
        Debug.Log("Used");
        return true;
    }

}
