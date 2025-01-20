using UnityEngine;


//script used to check if the player can enter wave 1 area
public class Wave1CheckerBehavior : MonoBehaviour
{
    public delegate void Wave1CheckerEventHandler();

    public static event Wave1CheckerEventHandler onPlayerWantsToEnterWave1Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 1 area
            onPlayerWantsToEnterWave1Area?.Invoke();
            Debug.Log("Player is trying to enter wave 1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
    }
}
