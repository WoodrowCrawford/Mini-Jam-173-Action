using UnityEngine;

public class EnemyWeaponBehavior : MonoBehaviour
{
    public delegate void EnemyWeaponEventHandler();

    public static event EnemyWeaponEventHandler OnEnemyAttackHit;

    public BoxCollider damageBox;
    public int damage;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && damageBox.enabled)
        {
            //call the player attack hit event
            Debug.Log("Player has been hit");
            OnEnemyAttackHit?.Invoke();



        }
    }




    private void OnTriggerExit(Collider other)
    {

    }

    public void EnableAttack()
    {
        damageBox.enabled = true;
    }

    public void DisableAttack()
    {
        damageBox.enabled = false;
    }
}
