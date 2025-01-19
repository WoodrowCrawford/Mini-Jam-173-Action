using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
   

    public delegate void GameManagerEventHandler();

    public static event GameManagerEventHandler onWave1Started;
    public static event GameManagerEventHandler onWave2Started;
    public static event GameManagerEventHandler onWave3Started;



    public Wave1AreaBehavior wave1AreaBehavior;
    public Wave2AreaBehavior wave2AreaBehavior;
    public Wave3AreaBehavior wave3AreaBehavior;



    public int totalEnemiesDefeated;
 




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        wave1AreaBehavior = GameObject.FindGameObjectWithTag("Wave1Area").GetComponent<Wave1AreaBehavior>();
        wave2AreaBehavior = GameObject.FindGameObjectWithTag("Wave2Area").GetComponent <Wave2AreaBehavior>();
        wave3AreaBehavior = GameObject.FindGameObjectWithTag("Wave3Area").GetComponent<Wave3AreaBehavior>(); 

    }


    private void OnEnable()
    {
        Wave1AreaBehavior.onPlayerEnteredWaveArea1 += CheckWhichWaveToStart;
        Wave2AreaBehavior.onPlayerEnteredWaveArea2 += CheckWhichWaveToStart;
        Wave3AreaBehavior.onPlayerEnteredWaveArea3 += CheckWhichWaveToStart;
    }


    private void OnDisable()
    {
        Wave1AreaBehavior.onPlayerEnteredWaveArea1 -= CheckWhichWaveToStart;
        Wave2AreaBehavior.onPlayerEnteredWaveArea2 -= CheckWhichWaveToStart;
        Wave3AreaBehavior.onPlayerEnteredWaveArea3 -= CheckWhichWaveToStart;
    }

    public void CheckWhichWaveToStart()
    {
        //check which wave the player is trying to start

        //if the player wants to start wave 1
        if(wave1AreaBehavior.playerWantsToStartWave1)
        {
            //check if the player can start wave 1
            if(totalEnemiesDefeated >= wave1AreaBehavior.enemiesRequiredToStart)
            {
                Debug.Log("Player can start wave 1!");

                //signal to everyone that wave 1 is starting
                onWave1Started?.Invoke();
            }

            else
            {
                Debug.Log("Player can not start wave 1...");
            }
            
        }

        //if the player wants to start wave 2
        else if (wave2AreaBehavior.playerWantsToStartWave2)
        {
            //check if the player can start wave 2
            if(totalEnemiesDefeated >= wave2AreaBehavior.enemiesRequiredToStart)
            {
                Debug.Log("Player can start wave 2!");

                //signal to everyone that wave 2 is starting
                onWave2Started?.Invoke();
            }

            else
            {
                Debug.Log("Player can not start wave 2!");
            }
        }

        //if the player wants to start wave 3
        else if (wave3AreaBehavior.playerWantsToStartWave3)
        {
            //check if the player can start wave 3
            if(totalEnemiesDefeated >= wave3AreaBehavior.enemiesRequiredToStart)
            {
                Debug.Log("Player can start wave 3!");

                //signal to everyone that wave 3 is starting
                onWave3Started?.Invoke();
            }

            else
            {
                Debug.Log("Player can not start wave 3");
            }
        }

        //check if the player can play that wave (have they defeated enough eniemies to unlock it)
    }


}
