//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Events;

//public class HealthConditions : MonoBehaviour
//{
//    // bool param is true for adding condition and false for removing
//    public static event UnityAction<Condition, bool> ChangeCondition;

//    private AudioManager audioManager;
//    private AudioSource[] sounds;

//    public enum Condition { ArmDislocated, HandSprained, TwistedAnkle, BrokenLeg }
//    private List<Condition> currentConditions = new List<Condition>();

//    private void Start()
//    {
//        audioManager = FindObjectOfType<AudioManager>();
//    }

//    void Update()
//    {
//        //// Test functions to add conditions to the player
//        //if (Input.GetKeyDown(KeyCode.Z))
//        //{
//        //    AddConditionToCurrentConditions(Condition.ArmDislocated);
//        //    Debug.Log("Disloacte Arm");
//        //}

//        //if (Input.GetKeyDown(KeyCode.U))
//        //{
//        //    AddConditionToCurrentConditions(Condition.HandSprained);
//        //    Debug.Log("Sprain Hand");
//        //}

//        //if (Input.GetKeyDown(KeyCode.I))
//        //{
//        //    AddConditionToCurrentConditions(Condition.TwistedAnkle);
//        //    Debug.Log("Twist Ankle");
//        //}

//        //if (Input.GetKeyDown(KeyCode.O))
//        //{
//        //    AddConditionToCurrentConditions(Condition.BrokenLeg);
//        //    Debug.Log("Break Leg");
//        //}

//        //if (Input.GetKeyDown(KeyCode.P))
//        //{
//        //    RemoveConditionFromCurrentConditions(Condition.ArmDislocated);
//        //    RemoveConditionFromCurrentConditions(Condition.BrokenLeg);
//        //    RemoveConditionFromCurrentConditions(Condition.HandSprained);
//        //    RemoveConditionFromCurrentConditions(Condition.TwistedAnkle);
//        //}
//    }

//    // Add condition to current conditions list and calls an event which casues the Player Controller to react with value changes
//    private void AddConditionToCurrentConditions(Condition con)
//    {
//        if(currentConditions.Contains(con) == false)
//        {
//            currentConditions.Add(con);
//            if (ChangeCondition != null)
//            {
//                // bool param is true for adding condition and false for removing
//                ChangeCondition(con, true);
//            }
//        }
//    }

//    // Remove condition from current conditions when a twisted ankle gets "healed" and call an event to change back the Player Values
//    private void RemoveConditionFromCurrentConditions(Condition con)
//    {
//        if(currentConditions.Contains(con) == true)
//        {
//            currentConditions.Remove(con);
//            if (ChangeCondition != null)
//            {
//                // bool param is true for adding condition and false for removing
//                ChangeCondition(con, false);
//            }
//        }
//    }

//    public List<Condition> GetConditions()
//    {
//        return currentConditions;
//    }

//    //TODO:
//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.y);
//    //    if(collision.collider.material.bounciness < 0.6 && gameObject.GetComponent<Rigidbody>().velocity.y < -10)
//    //    {
//    //        AddConditionToCurrentConditions(Condition.TwistedAnkle);

//    //        audioManager.AddSound("snd_breakingbones", gameObject);
//    //    }
//    //    else if(collision.collider.material.bounciness < 0.6)
//    //    {
//    //        //Matrasse
//    //    }
//    //    else if(gameObject.GetComponent<Rigidbody>().velocity.y < -2)
//    //    {
//    //        AddConditionToCurrentConditions(Condition.TwistedAnkle);

//    //        audioManager.AddSound("snd_fallingfloor", gameObject);
//    //    }
//    //}

//    //public void PlaySound(string soundType)
//    //{
//    //    if (GetComponent<AudioSource>() != null)
//    //    {
//    //        sounds = GetComponents<AudioSource>();
//    //        foreach (AudioSource sound in sounds)
//    //        {
//    //            if (sound.clip.name == soundType)
//    //            {
//    //                sound.Play();
//    //            }
//    //            else
//    //            {
//    //                audioManager.AddSound(soundType, this.gameObject);
//    //                sounds = GetComponents<AudioSource>();
//    //                sounds.First(audios => audios.name == soundType).Play();
//    //            }
//    //        }
//    //    }
//    //    else
//    //    {
//    //        audioManager.AddSound(soundType, this.gameObject);
//    //        sounds = GetComponents<AudioSource>();
//    //        sounds[0].Play();
//    //    }
//    //}
//}
