using UnityEngine;

public class DodgeTriggerBehavior : MonoBehaviour
{

    public delegate void DodgeEventHandler();

    public static event DodgeEventHandler OnSuccessfulDodge;

    public EnemyWeaponBehavior enemyWeapon;

   

    private void Start()
    {
        _dashTrigger = GameObject.FindGameObjectWithTag("DashTrigger");
        enemyWeapon = GameObject.FindGameObjectWithTag("EnemyWeapon").GetComponent<EnemyWeaponBehavior>();
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

        if (_dashTrigger.gameObject.tag == "DashTrigger"  && _dashTrigger.GetComponent<SphereCollider>().enabled)
        {
            if (other.CompareTag("Testing"))
            {
                Debug.Log("WITCH TIME");

                OnSuccessfulDodge.Invoke();
            }

            if (other.CompareTag("EnemyWeapon") && enemyWeapon.damageBox.enabled)
            {
                OnSuccessfulDodge.Invoke();
            }

        }

       

        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
