using UnityEngine;

public class Wave2StarterBehavior : MonoBehaviour
{
    public delegate void Wave2StarterEventHandler();

    public static event Wave2StarterEventHandler onPlayerTriggeredWave2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave2?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
