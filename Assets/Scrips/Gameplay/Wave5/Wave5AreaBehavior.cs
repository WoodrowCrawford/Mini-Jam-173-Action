using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class Wave5AreaBehavior : MonoBehaviour
{
    public delegate void Wave5AreaEventHandler();


    public static event Wave5AreaEventHandler onWave5Started;
    public static event Wave5AreaEventHandler onWave5Ended;

    public int enemiesRequiredToStart;

    public bool wave5Started;
    public bool wave5Complete;

    public bool playerIsInsideWave5Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;

    [Header("AI Navmesh")]
    [SerializeField] private NavMeshSurface navMeshSurface;



    private void OnEnable()
    {
        onWave5Ended += () => wave5Complete = true;
        Wave5StarterBehavior.onPlayerTriggeredWave5 += () => StartCoroutine(StartWave());

    }

    private void OnDisable()
    {
        onWave5Ended -= () => wave5Complete = true;
        Wave5StarterBehavior.onPlayerTriggeredWave5 -= () => StartCoroutine(StartWave());
    }




    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if (!wave5Complete)
        {
            //then check to see if the wave has already started
            if (!wave5Started)
            {
                //set wave started to true
                wave5Started = true;

                //send an event to signal that the wave has started
                onWave5Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 19);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave5Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}