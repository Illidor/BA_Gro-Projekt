using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenueFunktions
{
    NewGame,
    EnableRL,
    Controlls,
    Back,
    Exit
}


public class MainMenueButtonScript : MonoBehaviour
{

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color highlightColor;
    [SerializeField]
    private MainMenueFunktions funktion;
    private TextMesh textMesh;
    private GameObject camera;
    [SerializeField]
    private GameObject loadingCanvas;

    [SerializeField]
    private AudioClip hoverSound;
    [SerializeField]
    private AudioClip clickSound;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        camera = GameObject.Find("Camera");
        loadingCanvas.SetActive(false);

        if (funktion == MainMenueFunktions.EnableRL)
        {
            if (!GetComponent<SavePathSelection>().enableRogueLike)
            {
                GetComponentInChildren<TextMesh>().text = "Rogue-like Disabled";
            }
            else
            {
                GetComponentInChildren<TextMesh>().text = "Rogue-like Enabled";
            }
        }
    }

    public void buttonClicked()
    {
        if (camera.GetComponent<AudioSource>().isPlaying)
        {
            camera.GetComponent<AudioSource>().Stop();
        }
        camera.GetComponent<AudioSource>().clip = clickSound;
        camera.GetComponent<AudioSource>().Play();
        switch (funktion)
        {
            case MainMenueFunktions.NewGame:
                loadingCanvas.SetActive(true);
                SceneManager.LoadSceneAsync(1);
                break;
            case MainMenueFunktions.EnableRL:
                if (GetComponent<SavePathSelection>().enableRogueLike)
                {
                    GetComponentInChildren<TextMesh>().text = "Rogue-like Disabled";
                    GetComponent<SavePathSelection>().enableRogueLike = false;
                }
                else
                {
                    GetComponentInChildren<TextMesh>().text = "Rogue-like Enabled";
                    GetComponent<SavePathSelection>().enableRogueLike = true;
                }
                break;
            case MainMenueFunktions.Controlls:
                camera.GetComponent<MainMenuRay>().moveToControls(true);
                break;
            case MainMenueFunktions.Back:
                camera.GetComponent<MainMenuRay>().moveToControls(false);
                break;
            case MainMenueFunktions.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    
    public void ChangeColor(bool highlighted)
    {
        if (!highlighted)
        {
            textMesh.color = normalColor;

        }
        else
        {
            textMesh.color = highlightColor;
            if (camera.GetComponent<AudioSource>().isPlaying)
            {
                camera.GetComponent<AudioSource>().Stop();
            }
            camera.GetComponent<AudioSource>().clip = hoverSound;
            camera.GetComponent<AudioSource>().Play();
        }
    }
}
