using UnityEngine;

public class Wave1AreaBehavior : MonoBehaviour
{
    public delegate void Wave1AreaEventHandler();
    public static event Wave1AreaEventHandler onPlayerEnteredWaveArea1;

    public bool playerWantsToStartWave1;

    public int enemiesRequiredToStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWantsToStartWave1 = true;
            onPlayerEnteredWaveArea1?.Invoke();
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerWantsToStartWave1 = false;
    }
}
