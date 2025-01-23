using UnityEngine;

public class Wave1AreaBehavior : MonoBehaviour
{
    public delegate void Wave1AreaEventHandler();
    public static event Wave1AreaEventHandler onPlayerEnteredWaveArea1;

    public int enemiesRequiredToStart;

    public bool wave1Started;
    public bool playerIsInsideWave1Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;





    private void OnEnable()
    {
        onPlayerEnteredWaveArea1 += StartWave;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //fire an event that lets the game know that the player entered the wave 1 area
            onPlayerEnteredWaveArea1?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
    }


    public void StartWave()
    {
        //first check to see if the wave has already started
        if(!wave1Started)
        {
            //set wave started to true
            wave1Started = true;

            //spawn enemies
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
            }
            
            //wait until all the enemies are defeated

            //tell the barrier that all the enemies are defeated
        }
    }


}
