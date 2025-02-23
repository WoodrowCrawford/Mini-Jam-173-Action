using UnityEngine;


//script used to check if the player can enter wave 1 area
public class Wave6CheckerBehavior : MonoBehaviour
{
    public delegate void Wave6CheckerEventHandler();

    public static event Wave6CheckerEventHandler onPlayerWantsToEnterWave6Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 4 area
            onPlayerWantsToEnterWave6Area?.Invoke();
            Debug.Log("Player is trying to enter wave 6");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
    }

    
}