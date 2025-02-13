using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Wave1AreaBehavior : MonoBehaviour
{
    public delegate void Wave1AreaEventHandler();
    

    public static event Wave1AreaEventHandler onWave1Started;
    public static event Wave1AreaEventHandler onWave1Ended;

    public int enemiesRequiredToStart;

    public bool wave1Started;
    public bool wave1Complete;

    public bool playerIsInsideWave1Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;

    [Header("AI Navmesh")]
    [SerializeField] private NavMeshSurface navMeshSurface;





    private void OnEnable()
    {
        onWave1Ended += () => wave1Complete = true;
        Wave1StarterBehavior.onPlayerTriggeredWave1 += () => StartCoroutine(StartWave());
       
    }

    private void OnDisable()
    {
        onWave1Ended -= () => wave1Complete = true;
        Wave1StarterBehavior.onPlayerTriggeredWave1 -= () => StartCoroutine(StartWave());
    }


 

    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if(!wave1Complete)
        {
            //then check to see if the wave has already started
            if (!wave1Started)
            {
                //set wave started to true
                wave1Started = true;

                //send an event to signal that the wave has started
                onWave1Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 2);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave1Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}
