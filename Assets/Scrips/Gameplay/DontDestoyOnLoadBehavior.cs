using UnityEngine;

public class DontDestoyOnLoadBehavior : MonoBehaviour
{

    public static DontDestoyOnLoadBehavior instance;

    public GameObject ReferenceObject;

        private void Awake()
    {      
        ReferenceObject = gameObject;


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(ReferenceObject);
        }
        else
        {

            Destroy(ReferenceObject);
        }

        
    }
}