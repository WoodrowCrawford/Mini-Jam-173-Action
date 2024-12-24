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
            //call the player attack hit event
            OnPlayerAttackHit?.Invoke();
            
            

        }
    }




    private void OnTriggerExit(Collider other)
    {
        
    }
}
