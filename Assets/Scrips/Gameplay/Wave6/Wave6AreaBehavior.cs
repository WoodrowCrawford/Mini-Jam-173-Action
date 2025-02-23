using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class Wave6AreaBehavior : MonoBehaviour
{
    public delegate void Wave6AreaEventHandler();


    public static event Wave6AreaEventHandler onWave6Started;
    public static event Wave6AreaEventHandler onWave6Ended;

    public int enemiesRequiredToStart;

    public bool wave6Started;
    public bool wave6Complete;

    public bool playerIsInsideWave6Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;

    [Header("AI NavMesh")]

    [SerializeField] private NavMeshSurface navMeshSurface;
    


    private void OnEnable()
    {
        onWave6Ended += () => wave6Complete = true;
        Wave6StarterBehavior.onPlayerTriggeredWave6 += () => StartCoroutine(StartWave());

    }

    private void OnDisable()
    {
        onWave6Ended -= () => wave6Complete = true;
        Wave6StarterBehavior.onPlayerTriggeredWave6 -= () => StartCoroutine(StartWave());
    }




    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if (!wave6Complete)
        {
            //then check to see if the wave has already started
            if (!wave6Started)
            {
                //set wave started to true
                wave6Started = true;

                //send an event to signal that the wave has started
                onWave6Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 6);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave6Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}