using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Barrier4Behavior : MonoBehaviour
{
    public Wave4AreaBehavior wave4AreaBehavior;

    [Header("Wave 4 Walls")]
    [SerializeField] private GameObject barrierA;
    [SerializeField] private GameObject barrierB;
    [SerializeField] private GameObject barrierC;
    [SerializeField] private GameObject barrierD;


    [Header("Wave 4Text")]
    [SerializeField] private TMP_Text _enemiesRequiredText;
    [SerializeField] private TMP_Text _enemiesDefeatedText;

    //how many enemies need to be defeated in order to open
    public int enemiesRequiredToOpen;



    private void Start()
    {
        _enemiesRequiredText.text = "/" + enemiesRequiredToOpen.ToString();
    }

    private void Update()
    {
        _enemiesDefeatedText.text = GameManager.totalEnemiesDefeated.ToString();
    }


    private void OnEnable()
    {
        Wave4CheckerBehavior.onPlayerWantsToEnterWave4Area += CheckIfPlayerCanEnter;
        Wave4AreaBehavior.onWave4Started += EnableBarriers;
        Wave4AreaBehavior.onWave4Ended += DisableBarriers;
    }

   
    private void OnDisable()
    {
        Wave4CheckerBehavior.onPlayerWantsToEnterWave4Area -= CheckIfPlayerCanEnter;
        Wave4AreaBehavior.onWave4Started -= EnableBarriers;
        Wave4AreaBehavior.onWave4Ended -= DisableBarriers;
    }


    private void Awake()
    {
        wave4AreaBehavior = GameObject.FindGameObjectWithTag("Wave4Area").GetComponent<Wave4AreaBehavior>();
    }

   
    public void EnableBarriers()
    {
        //get all the barriers and enable them
        barrierA.GetComponent<BoxCollider>().enabled = true;
        barrierB.GetComponent<BoxCollider>().enabled = true;
        barrierC.GetComponent<BoxCollider>().enabled = true;
        barrierD.GetComponent<BoxCollider>().enabled = true;
    

        //show the door for barrier A
        barrierA.GetComponent <MeshRenderer>().enabled = true;

    }


    public void DisableBarriers()
    {
        //get all the barriers and disable them
        barrierA.GetComponent<BoxCollider>().enabled = false;
        barrierB.GetComponent<BoxCollider>().enabled = false;
        barrierC.GetComponent<BoxCollider>().enabled = false;
        barrierD.GetComponent<BoxCollider>().enabled = false;

        //hide the door for barrier A
        barrierA.GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("Disable barriers");
    }


    public void CheckIfPlayerCanEnter()
    {
        //checks to see if the player can enter the area
        if(GameManager.totalEnemiesDefeated >= enemiesRequiredToOpen)
        {
            //open the door
            DisableBarriers();
        }
    }

}