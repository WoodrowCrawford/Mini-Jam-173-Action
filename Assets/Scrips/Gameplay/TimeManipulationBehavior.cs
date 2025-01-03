using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class TimeManipulationBehavior : MonoBehaviour
{
    public delegate void TimeEventHandler();

    public static event TimeEventHandler OnTimeSlowed;

    public static bool isTimeSlowed = false;
    [SerializeField] private float _timeInSlowMotion;
    [SerializeField] private float _cooldown;

    [Header("Virtual Camera properties")]
    [SerializeField] private CinemachineCamera _vCam;
    [SerializeField] private VolumeProfile _slowMotionEffect;



    private void Awake()
    {
        _vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineCamera>();
        _vCam.GetComponent<CinemachineVolumeSettings>().Profile = null;

        
    }

    private void OnEnable()
    {
        // OnTimeSlowed += () => StartCoroutine(SlowDownTime());
        DodgeTriggerBehavior.OnSuccessfulDodge += () => StartCoroutine(SlowDownTime());
    }

    private void OnDisable()
    {
        //OnTimeSlowed -= () => StartCoroutine(SlowDownTime());
        DodgeTriggerBehavior.OnSuccessfulDodge -= () => StartCoroutine(SlowDownTime());
    }


    private void Update()
    {
        
    }


    //Slows down time for a certain amount of time
    public IEnumerator SlowDownTime()
    {
        //check if time is already slowed down
        if(isTimeSlowed)
        {
            yield return null;
        }

        //set time is slowed to true
        isTimeSlowed = true;
        _vCam.GetComponent<CinemachineVolumeSettings>().Profile = _slowMotionEffect;

        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.03f * Time.timeScale;

        //keep time slowed until the timer is up
        yield return new WaitForSecondsRealtime(_timeInSlowMotion);

        //revert time
        Time.timeScale = 1.0f;
       Time.fixedDeltaTime *= Time.timeScale;
        isTimeSlowed = false;

        _vCam.GetComponent<CinemachineVolumeSettings>().Profile = null;

        //wait until the cool down is over
        yield return new WaitForSecondsRealtime(_cooldown);

        Debug.Log("OVer");
        
    }
}
