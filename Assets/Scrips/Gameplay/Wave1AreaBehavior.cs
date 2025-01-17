using UnityEngine;

public class Wave1AreaBehavior : MonoBehaviour
{
    //Used to create events for this script
    public delegate void Wave1AreaEventHandler();


    public static event Wave1AreaEventHandler onPlayerEnteredWave1Area;
    public static event Wave1AreaEventHandler onPlayerExitedWave1Area;

    public int enemiesRequiredToStart;
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnteredWave1Area?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player is no longer in the area");
            onPlayerExitedWave1Area?.Invoke();
        }
    }
}
