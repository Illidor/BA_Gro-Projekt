using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinPuzzle : MonoBehaviour
{
    public delegate void MannequinSatisfiedEvent(MannequinColors mannequinColor);
    public static MannequinSatisfiedEvent mannequinSatisfied;

    [SerializeField]
    private int mannequinsToSatisfy;
    [SerializeField]
    private GameObject crankDisplayCase;
    [SerializeField]
    private GameObject functioningCrank;
    [SerializeField]
    private GameObject decoCrank;

    private int mannequinsSatisfied;

    private void Awake()
    {
        functioningCrank.SetActive(false);
        decoCrank.SetActive(true);
    }

    private void CountSatisfiedMannequins(MannequinColors mannequinColor)
    {
        mannequinsSatisfied++;

        if (mannequinsSatisfied >= mannequinsToSatisfy)
        {
            Debug.Log("all mannequins satisfied");
            crankDisplayCase.SetActive(false);
            decoCrank.SetActive(false);
            functioningCrank.SetActive(true);
        }
    }
    

    private void OnEnable()
    {
        mannequinSatisfied += CountSatisfiedMannequins;
    }

    private void OnDisable()
    {
        mannequinSatisfied -= CountSatisfiedMannequins;
    }
}
