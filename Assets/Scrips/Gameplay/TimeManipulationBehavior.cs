using System.Collections;
using UnityEngine;

public class TimeManipulationBehavior : MonoBehaviour
{
    public delegate void TimeEventHandler();

    public static event TimeEventHandler OnTimeSlowed;

    [SerializeField] private bool _isTimeSlowed = false;
    [SerializeField] private float _timeInSlowMotion;
    [SerializeField] private float _cooldown;






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
        if(_isTimeSlowed)
        {
            yield return null;
        }

        //set time is slowed to true
        _isTimeSlowed = true;

        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.03f * Time.timeScale;

        //keep time slowed until the timer is up
        yield return new WaitForSecondsRealtime(_timeInSlowMotion);

        //revert time
        Time.timeScale = 1.0f;
       Time.fixedDeltaTime *= Time.timeScale;

        //wait until the cool down is over
        yield return new WaitForSecondsRealtime(_cooldown);

        Debug.Log("OVer");
        
    }
}
