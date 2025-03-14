using UnityEngine;

public class DodgeTriggerBehavior : MonoBehaviour
{

    public static DodgeTriggerBehavior instance;

    public delegate void DodgeEventHandler();

    public static event DodgeEventHandler OnSuccessfulDodge;



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
        PlayerInputBehavior.OnDodgeStarted +=  () => GetComponent<SphereCollider>().enabled = true;
        PlayerInputBehavior.OnDodgeEnded += () => GetComponent<SphereCollider>().enabled = false;
    }

    private void OnDisable()
    {
        PlayerInputBehavior.OnDodgeStarted -= () => GetComponent<SphereCollider>().enabled = true;
        PlayerInputBehavior.OnDodgeEnded -= () => GetComponent<SphereCollider>().enabled = false;
    }


  
  

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            OnSuccessfulDodge?.Invoke();
        }
 
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
