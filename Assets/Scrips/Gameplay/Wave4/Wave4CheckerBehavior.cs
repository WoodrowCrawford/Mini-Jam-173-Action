using UnityEngine;


//script used to check if the player can enter wave 1 area
public class Wave4CheckerBehavior : MonoBehaviour
{
    public delegate void Wave4CheckerEventHandler();

    public static event Wave4CheckerEventHandler onPlayerWantsToEnterWave4Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 4 area
            onPlayerWantsToEnterWave4Area?.Invoke();
            Debug.Log("Player is trying to enter wave 4");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
    }

    
}
