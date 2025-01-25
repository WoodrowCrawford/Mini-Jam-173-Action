using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public delegate void WeaponEventHandler();

    public static event WeaponEventHandler OnPlayerAttackHit;

    public BoxCollider damageBox;
    public static float damage;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && damageBox.enabled)
        {
      
            //grab that enemies take damage function
            other.TryGetComponent<EnemyStateManager>(out EnemyStateManager enemy);
            enemy.SwitchState(enemy.takeDamageState);
      
        }
    }




    private void OnTriggerExit(Collider other)
    {
        
    }
}
