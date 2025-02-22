using UnityEngine;

public class Wave4StarterBehavior : MonoBehaviour
{
    public delegate void Wave4StarterEventHandler();

    public static event Wave4StarterEventHandler onPlayerTriggeredWave4;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave4?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
