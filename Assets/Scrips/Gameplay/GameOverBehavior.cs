
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverBehavior : MonoBehaviour
{


    [SerializeField] private GameObject _gameOverScreen;

    
    [SerializeField] private Button _quitButton;


    void OnEnable()
    {
        PlayerBehavior.onDeath += ShowGameOverScreen;
       TimerBehavior.onTimerEnded += ShowGameOverScreen;
        

      
        _quitButton.onClick.AddListener(() => Application.Quit());
    }

    void OnDisable()
    {
        PlayerBehavior.onDeath -= ShowGameOverScreen;
        TimerBehavior.onTimerEnded -= ShowGameOverScreen;

    
        _quitButton.onClick.RemoveListener(() => Application.Quit());
    }


    public void ShowGameOverScreen()
    {
        Cursor.visible = true;
        _gameOverScreen.SetActive(true);
    }
}
