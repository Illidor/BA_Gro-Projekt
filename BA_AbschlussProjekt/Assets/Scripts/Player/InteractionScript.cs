using System.Collections;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Max distance to objects the player is able to grab empty handed")]
    private float emptyHandedGrabingReach = 1.5f;

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

    public float GrabingReach { get; private set; }

    protected void Awake()
    {
        GrabingReach = emptyHandedGrabingReach;

        IsFrozen = false;

        if (GUIInteractionFeedbackHandler == null)
            GUIInteractionFeedbackHandler = GetComponentInChildren<GUIInteractionFeedbackHandler>();

        if (PlayerHealth == null)
            PlayerHealth = GetComponentInChildren<PlayerHealth>();
    }

    protected void Update()
    {
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
            raycastHit.collider?.GetComponent<BaseInteractable>()?.HandleInteraction(this);
        }
        else
        {
            if (didRaycastHit)
            {
                Debug.Log("want to combine???!?!? " + raycastHit.collider.name + "   " + raycastHit.collider.GetComponent<InteractionFoundation>().name);
                if (raycastHit.collider.GetComponent<ICombinable>()?.HandleCombine(this, UsedObject) == true)
                    return;
            }

            UsedObject?.GetComponent<IUseable>()?.HandleUse(this);
            StartCoroutine(IKToObject(UsedObject.GetComponent<BaseInteractable>().getIKPoint()));
        }
    }

    public IEnumerator IKToObject(Transform point)
    {
        if(point != null)
        {
            int timer = 120; // int -> how long is the grabbing time in frames

            //ToDo: if Condition > 1, this for if Condition <= 1 use left Hand
            for (int i = 0; i < timer; i++)
            {
                HandIKRight.position = Vector3.Lerp(HandIKRight.position, point.position, 0.1f);
                HandIKRight.eulerAngles = Vector3.Lerp(HandIKRight.eulerAngles, point.eulerAngles, 0.1f);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForFixedUpdate();
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






