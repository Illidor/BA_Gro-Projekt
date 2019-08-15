using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenueFunktions
{
    NewGame,
    EnableRL,
    Controlls,
    Back
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

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();     
        
        if(funktion == MainMenueFunktions.EnableRL)
        {
            if (!GetComponent<SavePathSelection>().enableRogueLike)
            {
                GetComponentInChildren<TextMesh>().text = "Rough-Like Disabled";
            }
            else
            {
                GetComponentInChildren<TextMesh>().text = "Rough-Like Enabled";
            }
        }
    }

    public void buttonClicked()
    {
        switch (funktion)
        {
            case MainMenueFunktions.NewGame:
                SceneManager.LoadScene(1);
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
                GameObject.Find("Camera").GetComponent<MainMenuRay>().moveToControls(true);
                break;
            case MainMenueFunktions.Back:
                GameObject.Find("Camera").GetComponent<MainMenuRay>().moveToControls(false);
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

        }
    }
}
