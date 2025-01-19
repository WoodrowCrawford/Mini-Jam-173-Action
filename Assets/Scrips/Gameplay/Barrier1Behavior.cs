using System.Collections;
using UnityEngine;

public class Barrier1Behavior : MonoBehaviour
{
    [Header("Wave 1 Walls")]
    [SerializeField] private GameObject barrierA;
    [SerializeField] private GameObject barrierB;
    [SerializeField] private GameObject barrierC;
    [SerializeField] private GameObject barrierD;
    [SerializeField] private GameObject barrierE;


   

    public IEnumerator SetUpBarrier()
    {
        //this runs once the wave is starting.

        //disable the barrier so the player can get inside.

        //check to see if the player is still inside 

        //if the player is still inside then enable barriers

        yield return null;
    }
}
