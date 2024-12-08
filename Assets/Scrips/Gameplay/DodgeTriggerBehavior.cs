using UnityEngine;

public class DodgeTriggerBehavior : MonoBehaviour
{

    public delegate void DodgeEventHandler();

    public static event DodgeEventHandler OnSuccessfulDodge;

    private void Awake()
    {
        _dashTrigger = GameObject.FindGameObjectWithTag("DashTrigger");
    }

    private void OnEnable()
    {
        PlayerInputBehavior.OnDodgeStarted +=  () =>_dashTrigger.GetComponent<SphereCollider>().enabled = true;
        PlayerInputBehavior.OnDodgeEnded += () => _dashTrigger.GetComponent<SphereCollider>().enabled = false;
    }

    private void OnDisable()
    {
        PlayerInputBehavior.OnDodgeStarted -= () => _dashTrigger.GetComponent<SphereCollider>().enabled = true;
        PlayerInputBehavior.OnDodgeEnded -= () => _dashTrigger.GetComponent<SphereCollider>().enabled = false;
    }


  
    [SerializeField] private GameObject _dashTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Testing"))
        {
            Debug.Log("WITCH TIME");

            OnSuccessfulDodge.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
