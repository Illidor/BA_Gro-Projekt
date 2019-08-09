using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPuzzlePath : MonoBehaviour
{
    public bool SelectPath = true;

    [SerializeField] GameObject PathOneCrutch;
    [SerializeField] GameObject PathTwoLever;

    // Start is called before the first frame update
    void Start()
    {
        if(SelectPath)
        {
            if(Random.Range(0,101) >= 50)
            {
                PathOneCrutch.SetActive(false);
            }
            else
            {
                PathTwoLever.SetActive(false);
            }
        }
    }
}
