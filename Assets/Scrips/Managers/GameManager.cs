using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public delegate void GameManagerEventHandler();

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

       

    }


    private void OnEnable()
    {
        
    }


    private void OnDisable()
    {
        
    }

    public void CheckWhichWaveToStart()
    {
        //check which wave the player is trying to start

        //check if the player can play that wave (have they defeated enough eniemies to unlock it)
    }


}
