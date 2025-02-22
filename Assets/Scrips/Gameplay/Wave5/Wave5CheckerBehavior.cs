using UnityEngine;


//script used to check if the player can enter wave 5 area
public class Wave5CheckerBehavior : MonoBehaviour
{
    public delegate void Wave5CheckerEventHandler();

    public static event Wave5CheckerEventHandler onPlayerWantsToEnterWave5Area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //signal that the player wants to enter wave 4 area
            onPlayerWantsToEnterWave5Area?.Invoke();
            Debug.Log("Player is trying to enter wave 5");
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }


}
