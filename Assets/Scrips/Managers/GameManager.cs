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



    public static int totalEnemiesDefeated;
 




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
        
    }


    private void OnDisable()
    {
       
       
    }

   


}
