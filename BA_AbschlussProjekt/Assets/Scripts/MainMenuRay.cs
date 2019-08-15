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


    [SerializeField]
    float moveSpeed;
    [SerializeField]
    GameObject mainPos;
    [SerializeField]
    GameObject controlsPos;


    private void Awake()
    {
        camera = GetComponent<Camera>();
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
                    lastButton.ChangeColor(false);
                }
                else if (lastButton != newButton)
                {
                    lastButton.ChangeColor(false);
                    lastButton = newButton;
                }

                lastButton.ChangeColor(true);

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
            }
        }


        if (isOnMainPos && (gameObject.transform.position - mainPos.transform.position).sqrMagnitude > 0.2f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, mainPos.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(!isOnMainPos && (gameObject.transform.position - controlsPos.transform.position).sqrMagnitude > 0.2f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, controlsPos.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void moveToControls(bool direction)
    {
        isOnMainPos = !direction;
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
