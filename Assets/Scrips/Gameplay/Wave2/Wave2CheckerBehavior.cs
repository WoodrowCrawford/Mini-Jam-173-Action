using UnityEngine;


//script used to check if the player can enter wave 1 area
public class Wave2CheckerBehavior : MonoBehaviour
{
    public delegate void Wave2CheckerEventHandler();

    public static event Wave2CheckerEventHandler onPlayerWantsToEnterWave2Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 2 area
            onPlayerWantsToEnterWave2Area?.Invoke();
            Debug.Log("Player is trying to enter wave 2");
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }


}
