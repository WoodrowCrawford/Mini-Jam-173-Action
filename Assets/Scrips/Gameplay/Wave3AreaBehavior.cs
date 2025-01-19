using UnityEngine;

public class Wave3AreaBehavior : MonoBehaviour
{
    public delegate void Wave3AreaEventHandler();
    public static event Wave3AreaEventHandler onPlayerEnteredWaveArea3;

    public bool playerWantsToStartWave3;

    public int enemiesRequiredToStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWantsToStartWave3 = true;
            onPlayerEnteredWaveArea3?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerWantsToStartWave3 = false;
    }
}
