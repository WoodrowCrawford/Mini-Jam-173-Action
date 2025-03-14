using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
   

    public delegate void GameManagerEventHandler();
    public static event GameManagerEventHandler GameManagerInitialized;

   



    public Wave1AreaBehavior wave1AreaBehavior;
    public Wave2AreaBehavior wave2AreaBehavior;
    public Wave3AreaBehavior wave3AreaBehavior;



    public static int totalEnemiesDefeated;
 




   



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
       
       
    }

     public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded");
        wave1AreaBehavior = GameObject.FindGameObjectWithTag("Wave1Area").GetComponent<Wave1AreaBehavior>();
        wave2AreaBehavior = GameObject.FindGameObjectWithTag("Wave2Area").GetComponent <Wave2AreaBehavior>();
        wave3AreaBehavior = GameObject.FindGameObjectWithTag("Wave3Area").GetComponent<Wave3AreaBehavior>();
        Time.timeScale = 1f;
    }

     public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("scene is unloaded");
    }

    

    

    public void Start()
    {
      Cursor.visible = false;

    }

   
   
}
