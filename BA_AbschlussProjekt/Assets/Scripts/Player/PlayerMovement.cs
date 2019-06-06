using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private float minLookingAngle;
    [SerializeField]
    private float maxLookingAngle;





    private Vector3 mousePosition;

   


    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        playerObject.transform.Rotate(0,Input.mousePosition.x - mousePosition.x,0);

        Debug.Log("Pos: " + (Input.mousePosition.y - mousePosition.y) + " | " + Camera.main.transform.eulerAngles.x);

        if (Camera.main.transform.eulerAngles.x > minLookingAngle && Input.mousePosition.y - mousePosition.y > 0)
        {
            Camera.main.transform.Rotate(Input.mousePosition.y - mousePosition.y, 0, 0);
        }
        else if (Camera.main.transform.eulerAngles.x > 360 - maxLookingAngle && Input.mousePosition.y - mousePosition.y < 0)
        {
            Camera.main.transform.Rotate(Input.mousePosition.y - mousePosition.y, 0, 0);
        }

        mousePosition = Input.mousePosition;
    }
}
