using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectPuzzlePath : MonoBehaviour
{
    public static event UnityAction GetShockDamage;
    public static event UnityAction FirstPersonDeath;
    public static event UnityAction ThirdPersonDeath;

    public bool SelectPath = true;

    [SerializeField] GameObject PathOneCrutch;
    [SerializeField] GameObject PathTwoLever;

    // Start is called before the first frame update
    void Start()
    {
        if (SelectPath)
        {
            if (Random.Range(0, 101) >= 50)
            {
                PathOneCrutch.SetActive(false);
            }
            else
            {
                PathTwoLever.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (SelectPath)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (GetShockDamage != null)
                    GetShockDamage();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (FirstPersonDeath != null)
                    FirstPersonDeath();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (ThirdPersonDeath != null)
                    ThirdPersonDeath();
            }
        }
    }
}
