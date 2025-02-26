using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class Wave4AreaBehavior : MonoBehaviour
{
    public delegate void Wave4AreaEventHandler();


    public static event Wave4AreaEventHandler onWave4Started;
    public static event Wave4AreaEventHandler onWave4Ended;

    public int enemiesRequiredToStart;

    public bool wave4Started;
    public bool wave4Complete;

    public bool playerIsInsideWave4Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;

     [Header("AI Navmesh")]
     [SerializeField] private NavMeshSurface navMeshSurface;



    private void OnEnable()
    {
        onWave4Ended += () => wave4Complete = true;
        Wave4StarterBehavior.onPlayerTriggeredWave4 += () => StartCoroutine(StartWave());

    }

    private void OnDisable()
    {
        onWave4Ended -= () => wave4Complete = true;
        Wave4StarterBehavior.onPlayerTriggeredWave4 -= () => StartCoroutine(StartWave());
    }




    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if (!wave4Complete)
        {
            //then check to see if the wave has already started
            if (!wave4Started)
            {
                //set wave started to true
                wave4Started = true;

                //send an event to signal that the wave has started
                onWave4Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 11);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave4Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}
