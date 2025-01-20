using UnityEngine;

public class Wave1AreaBehavior : MonoBehaviour
{
    public delegate void Wave1AreaEventHandler();
    public static event Wave1AreaEventHandler onPlayerEnteredWaveArea1;

    public bool playerIsInsideWave1Area;
   
    

    public int enemiesRequiredToStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
    }
}
