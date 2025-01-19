using UnityEngine;

public class Wave2AreaBehavior : MonoBehaviour
{
    public delegate void WaveAreaEventHandler();
    public static event WaveAreaEventHandler onPlayerEnteredWaveArea2;

    public bool playerWantsToStartWave2;

    public int enemiesRequiredToStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWantsToStartWave2 = true;
            onPlayerEnteredWaveArea2?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerWantsToStartWave2 = false;
    }
}
