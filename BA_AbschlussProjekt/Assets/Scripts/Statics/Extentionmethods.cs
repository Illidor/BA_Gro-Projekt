using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Extentionmethods
{
    /// <summary>
    /// Gets the last element of a collection. (Only works if T is a class, so no list/array of ints etc)
    /// </summary>
    public static T GetLast<T>(this IReadOnlyList<T> collection) where T : class
    {
        if (collection.Count <= 0)
            return null;
        else
            return collection[ collection.Count - 1 ];
    }

}
