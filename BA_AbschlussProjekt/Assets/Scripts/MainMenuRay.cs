using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuRay : MonoBehaviour
{
    private MainMenueButtonScript lastButton;
    private Camera camera;
    private bool isOnMainPos = true;
    private bool isOnCreditsPos = false;
    


    [SerializeField]
    float moveSpeed;
    [SerializeField]
    GameObject mainPos;
    [SerializeField]
    GameObject controlsPos;
    [SerializeField]
    GameObject creditsPos;


    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        RaycastHit hit;


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.collider.GetComponent<MainMenueButtonScript>() != null)
            {
                MainMenueButtonScript newButton = hit.collider.GetComponent<MainMenueButtonScript>();

                if (lastButton == null)
                {
                    lastButton = newButton;
                    lastButton.ChangeColor(true);
                }
                else if (lastButton != newButton)
                {
                    lastButton.ChangeColor(false);
                    lastButton = newButton;
                    lastButton.ChangeColor(true);
                }

                //lastButton.ChangeColor(true);

                if (Input.GetMouseButtonDown(0))
                {
                    lastButton.buttonClicked();
                }
            }
            else
            {
                if(lastButton != null)
                {
                    lastButton.ChangeColor(false);
                }
                lastButton = null;
            }        
        }



        if (isOnMainPos && (gameObject.transform.position - mainPos.transform.position).sqrMagnitude > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, mainPos.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (isOnCreditsPos && (gameObject.transform.position - creditsPos.transform.position).sqrMagnitude > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, creditsPos.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(!isOnMainPos && !isOnCreditsPos && (gameObject.transform.position - controlsPos.transform.position).sqrMagnitude > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, controlsPos.transform.position, moveSpeed * Time.deltaTime);
        }

        creditsPos.transform.GetChild(creditsPos.transform.childCount-1).Translate(Vector3.up * Time.deltaTime);

        //creditsPos.GetComponentInChildren<Transform>().Translate(Vector3.up * Time.deltaTime);

    }

    public void moveToControls(bool direction)
    {
        isOnMainPos = !direction;
        //isOnCreditsPos = !isOnMainPos;
    }

    public void moveToCredits(bool direction)
    {
        isOnCreditsPos = !direction;
        isOnMainPos = !isOnCreditsPos;
    }


    //[SerializeField]
    //private GameObject startGameButton;
    //[SerializeField]
    //private GameObject controllsButton;
    //[SerializeField]
    //private GameObject controllsPanel;
    //[SerializeField]
    //private GameObject loadingText;
    //[SerializeField]
    //private Sound menuSound;

    //public bool mouseLock;

    //private void Awake()
    //{
    //    menuSound?.PlaySound(0);
    //    mouseLock = false;
    //}

    //private void Update()
    //{
    //    LockMouse(mouseLock);
    //}

    //public void onStartGameClicked()
    //{
    //    mouseLock = true;
    //    LockMouse(mouseLock);
    //    startGameButton.GetComponent<Button>().interactable = false;
    //    controllsButton.GetComponent<Button>().interactable = false;
    //    loadingText.SetActive(true);
    //    SceneManager.LoadScene(1);
    //}

    //public void onContollsClikced()
    //{
    //    startGameButton.SetActive(!startGameButton.activeSelf);
    //    controllsButton.SetActive(!controllsButton.activeSelf);
    //    controllsPanel.SetActive(!controllsPanel.activeSelf);
    //}

    //private void LockMouse(bool p_mouseLock)
    //{
    //    Cursor.visible = !p_mouseLock;

    //    if (p_mouseLock == false && Cursor.lockState == CursorLockMode.Locked)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }

    //    if (p_mouseLock == true && Cursor.lockState == CursorLockMode.None)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //}
}
