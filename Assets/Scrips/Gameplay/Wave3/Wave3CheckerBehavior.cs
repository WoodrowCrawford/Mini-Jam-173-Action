using UnityEngine;


//script used to check if the player can enter wave 1 area
public class Wave3CheckerBehavior : MonoBehaviour
{
    public delegate void Wave3CheckerEventHandler();

    public static event Wave3CheckerEventHandler onPlayerWantsToEnterWave3Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 1 area
            onPlayerWantsToEnterWave3Area?.Invoke();
            Debug.Log("Player is trying to enter wave 3");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
    }

    
}
