using UnityEngine;

public class Wave1StarterBehavior : MonoBehaviour
{
    public delegate void Wave1StarterEventHandler();

    public static event Wave1StarterEventHandler onPlayerTriggeredWave1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave1?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
