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



    public void onStartGameClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void onContollsClikced()
    {
        startGameButton.SetActive(!startGameButton.activeSelf);
        controllsButton.SetActive(!controllsButton.activeSelf);
        controllsPanel.SetActive(!controllsPanel.activeSelf);
    }
}
