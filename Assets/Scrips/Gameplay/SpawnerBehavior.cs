using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    public SpawnerBehavior instance;

   [SerializeField] private GameObject Spawner;
   [SerializeField] private GameObject GameObjectToSpawn;



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

   public void SpanwnEnemyWithModifiers(float health)
   {
      //get reference to enenmy behavior
      
     
   }
}
