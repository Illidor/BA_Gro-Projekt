using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;
using System;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public enum Conditions
{
    UpperBodyCondition,
    LowerBodyCondition
}

public class PlayerHealth : MonoBehaviour
{
    public static event UnityAction PlayerDied;

    private float upperBodyCondition;
    private float lowerBodyCondition;

    [SerializeField]
    private Sound smallConditionSound;
    [SerializeField]
    private Sound bigConditionSound;

    public Camera monitorRoomCamera;
    public Camera mainCamera;

    private Animator anim;
    private PostProcessVolume ppVolume;
    private Vignette vignette;
    private float lerpTimer = 1f;
    private bool shrinkVignette = true;
    private bool unshrinkVignette = false;

    private void Awake()
    {
        upperBodyCondition = 2f;
        lowerBodyCondition = 2f;


    }

    private void Start()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);

        ppVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);

        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerDeathThirdPerson();
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerDeathFirstPerson();
        }

        if(unshrinkVignette)
        {
            lerpTimer += Time.deltaTime / 10f;
            vignette.intensity.value = lerpTimer;

            if (lerpTimer >= 1f)
            {
                unshrinkVignette = false;
                lerpTimer = 1f;
            }
        }

        if(shrinkVignette)
        {
            lerpTimer -= Time.deltaTime / 10f;
            vignette.intensity.value = lerpTimer;

            if(lerpTimer <= 0f)
            {
                shrinkVignette = false;
                lerpTimer = 0f;
            }

        }

    }

    public void ChangeCondition(Conditions which, float value)
    {
        switch (which)
        {
            case (Conditions.UpperBodyCondition):

                if (upperBodyCondition - value > 0)
                {
                    upperBodyCondition -= value;
                    // Check which sound to play
                    if(Mathf.Approximately(upperBodyCondition, 1.5f)){
                        smallConditionSound.PlaySound(0);
                    }
                    else if(Mathf.Approximately(upperBodyCondition, 1f)) {
                        bigConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(upperBodyCondition, 0.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(upperBodyCondition, 0f)) {
                        bigConditionSound.PlaySound(0);
                    }
                }
                else
                {
                    upperBodyCondition = 0f;
                }

                break;
            case (Conditions.LowerBodyCondition):

                if (lowerBodyCondition - value > 0)
                {
                    lowerBodyCondition -= value;

                    // Check which sound to play
                    if (Mathf.Approximately(lowerBodyCondition, 1.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 1f)) {
                        bigConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 0.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 0f)) {
                        bigConditionSound.PlaySound(0);
                    }
                }
                else
                {
                    lowerBodyCondition = 0f;
                }

                break;
        }
    }

    public float GetCondition(Conditions which)
    {
        switch (which)
        {
            case (Conditions.UpperBodyCondition):

                return upperBodyCondition;

            case (Conditions.LowerBodyCondition):

                return lowerBodyCondition;
        }
        return 0f;
    }

    public float GetSummedCondition()
    {
        return upperBodyCondition + lowerBodyCondition;
    }

    private void PlayerDeathThirdPerson()
    {
        mainCamera.enabled = false;
        monitorRoomCamera.enabled = true;

        PlayerDied?.Invoke();
    }

    private void PlayerDeathFirstPerson()
    {
        anim.SetTrigger("Die");
        unshrinkVignette = true;

        vignette.intensity.value = 1f;
    }
    //ToDo: Play Sound

    private void OnEnable()
    {
        Picture.PlayerFailed += PlayerDeathThirdPerson;
    }

    private void OnDisable()
    {
        Picture.PlayerFailed -= PlayerDeathThirdPerson;
    }

}