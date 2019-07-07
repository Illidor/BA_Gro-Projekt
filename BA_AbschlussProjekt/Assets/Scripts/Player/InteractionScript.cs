using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Max distance to objects the player is able to grab empty handed")]
    private float emptyHandedGrabingReach = 1.5f;
    private bool cR_isRunning = false;

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

        if(UsedObject == null && cR_isRunning)
        {
            StopCoroutine(IKToObject(UsedObject.GetComponent<BaseInteractable>().GetIKPoint(GrabingPoint.transform)));
            cR_isRunning = false;

            StartCoroutine(IKToObject(HandIKRight.parent));
        }
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
                if (raycastHit.collider.GetComponent<ICombinable>()?.HandleCombine(this, UsedObject) == true)
                    return;
            }

            UsedObject?.GetComponent<IUseable>()?.HandleUse(this);

            if (!cR_isRunning)
            {
                StartCoroutine(IKToObject(UsedObject.GetComponent<BaseInteractable>().GetIKPoint(GrabingPoint.transform)));
            }
        }
    }

    public IEnumerator IKToObject(Transform point)
    {
        cR_isRunning = true;

        float time = 2f;
        float elapsedTime = 0f;
        float distance = Time.deltaTime/time;

        while (elapsedTime < time)
        {
            
            HandIKRight.transform.position = Vector3.Lerp(HandIKRight.position, point.position, distance);
            HandIKRight.transform.LookAt(point); 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        cR_isRunning = false;
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
            StopCoroutine(IKToObject(UsedObject.GetComponent<BaseInteractable>().GetIKPoint(GrabingPoint.transform)));
            cR_isRunning = false;
        }

        UsedObject = null;
        IsCarrying = false;
        IsPushing = false;

        StartCoroutine(IKToObject(HandIKRight.parent));
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






