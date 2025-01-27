using UnityEngine;
using TMPro;

public class EnemiesDefeatedUIBehavior : MonoBehaviour
{
    public TMP_Text EnemiesDefeatedNumber;




    private void Update()
    {
        EnemiesDefeatedNumber.text = GameManager.totalEnemiesDefeated.ToString();
    }

}
