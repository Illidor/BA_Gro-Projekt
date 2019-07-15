using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject startGameButton;
    [SerializeField]
    private GameObject controllsButton;
    [SerializeField]
    private GameObject controllsPanel;
    private bool mouseLock = false;

    private void Update()
    {
        LockMouse(mouseLock);
    }

    public void onStartGameClicked()
    {
        mouseLock = true;
        LockMouse(mouseLock);
        SceneManager.LoadScene(1);
    }

    public void onContollsClikced()
    {
        startGameButton.SetActive(!startGameButton.activeSelf);
        controllsButton.SetActive(!controllsButton.activeSelf);
        controllsPanel.SetActive(!controllsPanel.activeSelf);
    }

    private void LockMouse(bool p_mouseLock)
    {
        if (p_mouseLock == false && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (p_mouseLock == true && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}