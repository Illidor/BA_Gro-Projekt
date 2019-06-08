using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : Carryable
{
    public List<GameObject> pictureParts = new List<GameObject>();
    private MeshRenderer pictureUnbroken;
    private BoxCollider interactionCollider;

    // Start is called before the first frame update
    void Start()
    {
        pictureUnbroken = GetComponent<MeshRenderer>();
        interactionCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for Physics Material
        if(base.isInUse == false)
        {
            foreach (var part in pictureParts)
            {
                pictureUnbroken.enabled = false;
                interactionCollider.enabled = false;
                part.SetActive(true);
                part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
            }
        }
    }
}
