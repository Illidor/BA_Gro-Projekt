using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CutSceneCrutch : MonoBehaviour
{
    public GameObject child;
    public GameObject adult;

    public Transform childEndPoint;
    public Transform adultEndPoint;

    public Transform middlePoint;

    private void StartCutscene()
    {
        Tween.Position(child.transform, middlePoint.position, 2f, 0f, Tween.EaseLinear);
        Tween.Position(adult.transform, middlePoint.position, 2f, 0f, Tween.EaseLinear);
    }

    private void OnEnable()
    {
        FlashbackInteraction.StartFlashBack += StartCutscene;
    }

    private void OnDisable()
    {
        FlashbackInteraction.StartFlashBack -= StartCutscene;
    }
}
