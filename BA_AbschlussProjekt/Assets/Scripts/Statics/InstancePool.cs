using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singelton to parent all instances to that just need to be parented to global
/// </summary>
public class InstancePool : MonoBehaviour
{
    public static new Transform transform;
    public static InstancePool instance;

    private void Awake()
    {
        transform = base.transform;
        if(instance == null)
        {
            instance = this;
        }
    }

    public void GoBackToMainMenue(float waitTime)
    {
        StartCoroutine(backToMenueCR(waitTime));
    }
    
    private IEnumerator backToMenueCR(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        SceneManager.LoadScene(0);
    }
}
