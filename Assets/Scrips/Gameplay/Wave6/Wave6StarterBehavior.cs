using UnityEngine;

public class Wave6StarterBehavior : MonoBehaviour
{
    public delegate void Wave6StarterEventHandler();

    public static event Wave6StarterEventHandler onPlayerTriggeredWave6;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave6?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}