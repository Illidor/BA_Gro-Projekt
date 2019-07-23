using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Max distance to objects the player is able to grab empty handed")]
    private float emptyHandedGrabingReach = 1.5f;
    [SerializeField]
    private float iKSpeed = 2f;
    private RigidbodyFirstPersonController fPSController;
    private bool lastGrabWasBothHanded;

    [field: Space,
        LabelOverride("Grabing Point"), SerializeField, Tooltip("Object the carried object is parented to")]
    public Transform GrabingPoint { get; private set; }

    [field: LabelOverride("GUI Interaction Feedback Handler"), SerializeField,
        Tooltip("The GUI Interaction Feedback Handler of the player. If not supplied the script will search on this gameobject and it's children")]
    public GUIInteractionFeedbackHandler GUIInteractionFeedbackHandler { get; private set; }

    [field: LabelOverride("Player Health"), SerializeField,
        Tooltip("Handler of the players injuries. If not supplied the script will search on this gameobject and it's children")]
    public PlayerHealth PlayerHealth { get; protected set; }

    public GrabInteractable UsedObject { get; set; }
    private Animator animator;

    public bool IsCarrying { get; private set; }
    public bool IsPushing { get; private set; }

    public bool IsFrozen { get; set; }

    public Transform HandIKLeft;
    public Transform HandIKRight;
    public bool cR_isRunning = false;



    public float GrabingReach { get; private set; }

    protected void Awake()
    {
        GrabingReach = emptyHandedGrabingReach;

        IsFrozen = false;

        if (GUIInteractionFeedbackHandler == null)
            GUIInteractionFeedbackHandler = GetComponentInChildren<GUIInteractionFeedbackHandler>();

        if (PlayerHealth == null)
            PlayerHealth = GetComponentInChildren<PlayerHealth>();

        fPSController = gameObject.GetComponent<RigidbodyFirstPersonController>();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SceneManager.LoadScene(1);

        if (IsFrozen)
            return;

        if (IsCarrying || IsPushing)
        {
            if (CTRLHub.DropUp)
                UsedObject.PutDown(this);
        }

        HandleActions();
    }

    private void HandleActions()
    {
        GUIInteractionFeedbackHandler.ResetGUI();

        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        bool didRaycastHit = Physics.Raycast(screenCenterRay, out raycastHit, GrabingReach);

        if (IsCarrying == false)
        {
            if (!cR_isRunning)
            {
                raycastHit.collider?.GetComponent<BaseInteractable>()?.HandleInteraction(this);
            }
        }
        else
        {
            if (didRaycastHit)
            {
                if (raycastHit.collider.GetComponent<ICombinable>()?.HandleCombine(this, UsedObject) == true)
                    return;
            }

            UsedObject?.GetComponent<IUseable>()?.HandleUse(this);
        }
    }

    public void ResetIK()
    {
        if (cR_isRunning)
        {
            StopCoroutine(IKToObject(UsedObject, lastGrabWasBothHanded));

            cR_isRunning = false;
        }

        animator.SetBool("Grab", false);
      

        StartCoroutine(IKToObject(null, true));
    }

    public IEnumerator IKToObject(BaseInteractable objecToInteractWith, bool bothHanded)
    {
        cR_isRunning = true;

        //BackUP
        bool backUpGrabbing = false;


        if (objecToInteractWith != null)
        {
            if (!bothHanded)
            {
                animator.SetBool("Grab", true);
                animator.SetFloat("Blend", 0f);
            }
            else
            {
                animator.SetFloat("Blend", .5f);
                animator.SetBool("Grab", true);
            }



        }

        if (objecToInteractWith != null && objecToInteractWith.GetIKPoint(false) == null)
        {
            backUpGrabbing = true;
        }

        Transform pointRight;
        Transform pointLeft = null;
        if (objecToInteractWith == null || backUpGrabbing) //Resetting the arms when droping a item
        {
            pointRight = HandIKRight.parent;
            pointLeft = HandIKLeft.parent;
        }
        else
        {
            pointRight = objecToInteractWith.GetIKPoint(false);

            if (bothHanded)
            {
                pointLeft = objecToInteractWith.GetIKPoint(true);
            }

            fPSController.freezePlayerCamera = true;
            fPSController.freezePlayerMovement = true;
        }

        float distance = iKSpeed / Time.deltaTime;

        if (pointRight != null)
        {
            while ((HandIKRight.position - pointRight.position).magnitude > .2f || (HandIKRight.eulerAngles - pointRight.eulerAngles).magnitude > .2f)
            {
                HandIKRight.position = Vector3.MoveTowards(HandIKRight.position, pointRight.position, distance);
                HandIKRight.rotation = Quaternion.Lerp(HandIKRight.rotation, pointRight.rotation, distance);

                if (bothHanded)
                {
                    HandIKLeft.position = Vector3.MoveTowards(HandIKLeft.position, pointLeft.position, distance);
                    HandIKLeft.rotation = Quaternion.Lerp(HandIKLeft.rotation, pointLeft.rotation, distance);
                }

                yield return new WaitForEndOfFrame();
            }

            HandIKRight.rotation = pointRight.rotation;
            yield return new WaitForEndOfFrame();
        }

        objecToInteractWith?.CarryOutInteraction(this);


        if (backUpGrabbing)
        {
            objecToInteractWith.transform.SetParent(GrabingPoint);
            objecToInteractWith.transform.localPosition = fPSController.transform.forward;
            objecToInteractWith.transform.localEulerAngles = Vector3.zero;
        }
        else if (pointRight != null && objecToInteractWith != null && UsedObject != null)
        {
            Transform FixPoint = objecToInteractWith.transform;

            if (FixPoint != null && FixPoint.gameObject.tag == "FixPoint")
            {
                while ((FixPoint.transform.position - GrabingPoint.position).magnitude > .2f || ((FixPoint.transform.eulerAngles - GrabingPoint.eulerAngles).magnitude > .2f))
                {
                    FixPoint.transform.position = Vector3.MoveTowards(FixPoint.transform.position, GrabingPoint.transform.position, distance);
                    FixPoint.transform.rotation = Quaternion.Lerp(FixPoint.transform.rotation, GrabingPoint.transform.rotation, distance);

                    HandIKRight.transform.position = Vector3.MoveTowards(HandIKRight.position, pointRight.position, distance);
                    HandIKRight.rotation = Quaternion.Lerp(HandIKRight.rotation, pointRight.rotation, distance);

                    if (bothHanded)
                    {
                        HandIKLeft.transform.position = Vector3.MoveTowards(HandIKLeft.position, pointLeft.position, distance);
                        HandIKLeft.rotation = Quaternion.Lerp(HandIKLeft.rotation, pointLeft.rotation, distance);
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        lastGrabWasBothHanded = bothHanded;
        Debug.Log("cR_End");
        cR_isRunning = false;

        fPSController.freezePlayerCamera = false;
        fPSController.freezePlayerMovement = false;
    }

    public void SetCarriedObject(GrabInteractable objectToCarry)
    {
        UsedObject = objectToCarry;
        IsCarrying = true;
        IsPushing = false;
    }

    public void SetPushedObject(GrabInteractable objectToPush)
    {
        UsedObject = objectToPush;
        IsPushing = true;
        IsCarrying = false;
    }

    public void StopUsingObject()
    {
        ResetIK();

        animator.ResetTrigger("Grab");
        UsedObject = null;
        IsCarrying = false;
        IsPushing = false;
    }

    public void IncreaseReach(float reachToAdd)
    {
        GrabingReach += reachToAdd;
    }

    public void ResetReachToDefault()
    {
        GrabingReach = emptyHandedGrabingReach;
    }
}