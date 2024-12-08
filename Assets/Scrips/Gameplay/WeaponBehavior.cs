using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitthe enemy for " + damage);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
