using System.Collections;
using UnityEngine;

public class Wave3AreaBehavior : MonoBehaviour
{
    public delegate void Wave3AreaEventHandler();


    public static event Wave3AreaEventHandler onWave3Started;
    public static event Wave3AreaEventHandler onWave3Ended;

    public int enemiesRequiredToStart;

    public bool wave3Started;
    public bool wave3Complete;

    public bool playerIsInsideWave1Area;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject enemyToSpawn;





    private void OnEnable()
    {
        onWave3Ended += () => wave3Complete = true;
        Wave3StarterBehavior.onPlayerTriggeredWave3 += () => StartCoroutine(StartWave());

    }

    private void OnDisable()
    {
        onWave3Ended -= () => wave3Complete = true;
        Wave3StarterBehavior.onPlayerTriggeredWave3 -= () => StartCoroutine(StartWave());
    }




    public IEnumerator StartWave()
    {
        //first check if the wave has already been completed
        if (!wave3Complete)
        {
            //then check to see if the wave has already started
            if (!wave3Started)
            {
                //set wave started to true
                wave3Started = true;

                //send an event to signal that the wave has started
                onWave3Started?.Invoke();

                //spawn enemies
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemyToSpawn, spawnPoints[i].transform.position, Quaternion.identity);
                }

                //wait until all the enemies are defeated
                yield return new WaitUntil(() => GameManager.totalEnemiesDefeated == 6);

                Debug.Log("Player defeated all the enemies");

                //tell the barrier that all the enemies are defeated
                onWave3Ended?.Invoke();

                yield break;

            }
        }

        yield break;

    }


}

