using UnityEngine;

public class Wave3StarterBehavior : MonoBehaviour
{
    public delegate void Wave3StarterEventHandler();

    public static event Wave3StarterEventHandler onPlayerTriggeredWave3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave3?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}