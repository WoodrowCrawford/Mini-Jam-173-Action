using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class Wave2AreaBehavior : MonoBehaviour
{
    public delegate void Wave2AreaEventHandler();


    public static event Wave2AreaEventHandler onWave2Started;
    public static event Wave2AreaEventHandler onWave2Ended;

    public int enemiesRequiredToStart;

    public bool wave2Started;
    public bool wave2Complete;

    public bool playerIsInsideWave1Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;

     [Header("AI Navmesh")]
     [SerializeField] private NavMeshSurface navMeshSurface;



    private void OnEnable()
    {
        onWave2Ended += () => wave2Complete = true;
        Wave2StarterBehavior.onPlayerTriggeredWave2 += () => StartCoroutine(StartWave());

    }

    private void OnDisable()
    {
        onWave2Ended -= () => wave2Complete = true;
        Wave2StarterBehavior.onPlayerTriggeredWave2 -= () => StartCoroutine(StartWave());
    }




    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if (!wave2Complete)
        {
            //then check to see if the wave has already started
            if (!wave2Started)
            {
                //set wave started to true
                wave2Started = true;

                //send an event to signal that the wave has started
                onWave2Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 5);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave2Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}
