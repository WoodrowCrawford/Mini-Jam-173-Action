using UnityEngine;

public class EnemyWeaponBehavior : MonoBehaviour
{
    public delegate void EnemyWeaponEventHandler();
    public PlayerBehavior playerBehavior;

    public static event EnemyWeaponEventHandler OnEnemyAttackHit;

    public BoxCollider damageBox;
    public int damage;
    public static bool AttackBoxEnabled;




    private void Awake()
    {
        playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && damageBox.enabled && !PlayerInputBehavior.playerIsInvulnerable)
        {
            //call the player attack hit event
            Debug.Log("Player has been hit");
            StartCoroutine(playerBehavior.TakeDamage(damage));

            OnEnemyAttackHit?.Invoke();
        }
    }




    private void OnTriggerExit(Collider other)
    {

    }

   


    public void EnableAttack()
    {
        damageBox.enabled = true;
        AttackBoxEnabled = true;
    }

    public void DisableAttack()
    {
        damageBox.enabled = false;
        AttackBoxEnabled = false;
    }
}
