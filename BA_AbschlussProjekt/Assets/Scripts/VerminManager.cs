using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerminManager : MonoBehaviour
{
    [Header("Bottom Floor Paths")]
    [SerializeField] Pixelplacement.Spline pathOne;
    [SerializeField] Pixelplacement.Spline pathTwo;
    [SerializeField] Pixelplacement.Spline pathThree;
    [SerializeField] Pixelplacement.Spline pathFour;

    [Header("Upper Floor Paths")]
    [SerializeField] Pixelplacement.Spline pathFive;
    [SerializeField] Pixelplacement.Spline pathSix;

    void Update()
    {
        // Bottom Floor Paths
        // Path One
        pathOne.followers[0].percentage += Time.deltaTime / 20f;
        if (pathOne.followers[0].percentage >= 1f)
            pathOne.followers[0].percentage = 0f;

        pathOne.followers[1].percentage += Time.deltaTime / 17f;
        if (pathOne.followers[1].percentage >= 1f)
            pathOne.followers[1].percentage = 0f;

        // Path Two
        pathTwo.followers[0].percentage += Time.deltaTime / 30f;
        if (pathTwo.followers[0].percentage >= 1f)
            pathTwo.followers[0].percentage = 0f;

        // Path Three
        pathThree.followers[0].percentage += Time.deltaTime / 21f;
        if (pathThree.followers[0].percentage >= 1f)
            pathThree.followers[0].percentage = 0f;

        pathThree.followers[1].percentage += Time.deltaTime / 15f;
        if (pathThree.followers[1].percentage >= 1f)
            pathThree.followers[1].percentage = 0f;

        // Path Four
        pathFour.followers[0].percentage += Time.deltaTime / 25f;
        if (pathFour.followers[0].percentage >= 1f)
            pathFour.followers[0].percentage = 0f;

        // Upper Floor Paths
        // Path Five
        pathFive.followers[0].percentage += Time.deltaTime / 25f;
        if (pathFive.followers[0].percentage >= 1f)
            pathFive.followers[0].percentage = 0f;

        // Path Six
        pathSix.followers[0].percentage += Time.deltaTime / 14f;
        if (pathSix.followers[0].percentage >= 1f)
            pathSix.followers[0].percentage = 0f;
    }
}
