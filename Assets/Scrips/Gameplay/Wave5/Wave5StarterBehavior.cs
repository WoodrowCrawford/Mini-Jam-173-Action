using UnityEngine;

public class Wave5StarterBehavior : MonoBehaviour
{
    public delegate void Wave5StarterEventHandler();

    public static event Wave5StarterEventHandler onPlayerTriggeredWave5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Send a signal to start the wave
            onPlayerTriggeredWave5?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
