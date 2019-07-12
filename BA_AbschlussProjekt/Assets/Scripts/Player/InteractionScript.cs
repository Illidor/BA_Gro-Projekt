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

    [field: Space,
        LabelOverride("Grabing Point"), SerializeField, Tooltip("Hand the carried object is parented to")]
    public Transform GrabingPoint { get; private set; }

    [field: LabelOverride("GUI Interaction Feedback Handler"), SerializeField,
        Tooltip("The GUI Interaction Feedback Handler of the player. If not supplied the script will search on this gameobject and it's children")]
    public GUIInteractionFeedbackHandler GUIInteractionFeedbackHandler { get; private set; }

    [field: LabelOverride("Player Health"), SerializeField,
        Tooltip("Handler of the players injuries. If not supplied the script will search on this gameobject and it's children")]
    public PlayerHealth PlayerHealth { get; protected set; }

    public GrabInteractable UsedObject { get; set; }

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
                Debug.Log(raycastHit.collider.GetComponent<ICombinable>());
                if (raycastHit.collider.GetComponent<ICombinable>()?.HandleCombine(this, UsedObject) == true)
                    return;
            }

            UsedObject?.GetComponent<IUseable>()?.HandleUse(this);
        }
    }

    public IEnumerator IKToObject(BaseInteractable objecToInteractWith)
    {
        cR_isRunning = true;
        Transform point;
        if (objecToInteractWith == null)
        {
            point = HandIKRight.parent;
        }
        else
        {
            point = objecToInteractWith.GetIKPoint(GrabingPoint.transform);

            fPSController.freezePlayerCamera = true;
            fPSController.freezePlayerMovement = true;
        }

        float distance = iKSpeed/Time.deltaTime;

        while ((HandIKRight.position - point.position).magnitude > 5f || (HandIKRight.eulerAngles - point.eulerAngles).magnitude > 5f)
        {
            HandIKRight.transform.position = Vector3.MoveTowards(HandIKRight.position, point.position, distance);
            HandIKRight.rotation = Quaternion.Lerp(HandIKRight.rotation, point.rotation, distance);

            yield return new WaitForEndOfFrame();
        }

        HandIKRight.rotation = point.rotation;
        yield return new WaitForEndOfFrame();
        objecToInteractWith?.CarryOutInteraction(this);

        if(objecToInteractWith != null && UsedObject != null)
        {
            while ((objecToInteractWith.transform.position - GrabingPoint.position).magnitude > 5f || ((objecToInteractWith.transform.eulerAngles - GrabingPoint.eulerAngles).magnitude > 5f))
            {
                objecToInteractWith.transform.position = Vector3.MoveTowards(objecToInteractWith.transform.position, GrabingPoint.transform.position, distance);
                objecToInteractWith.transform.rotation = Quaternion.Lerp(objecToInteractWith.transform.rotation, GrabingPoint.transform.rotation, distance);

                HandIKRight.transform.position = Vector3.MoveTowards(HandIKRight.position, point.position, distance);
                HandIKRight.rotation = Quaternion.Lerp(HandIKRight.rotation, point.rotation, distance);
                yield return new WaitForEndOfFrame();
            }
        }
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
        if (cR_isRunning)
        {
            StopCoroutine(IKToObject(UsedObject));

            cR_isRunning = false;
        }

        UsedObject = null;
        IsCarrying = false;
        IsPushing = false;

        //StartCoroutine(IKToObject(HandIKRight.parent));
        StartCoroutine(IKToObject(null));
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






