using UnityEngine;
using UnityEngine.UI;


public class PauseBehavior : MonoBehaviour
{
   
    

   [SerializeField] private GameObject _pauseScreen;
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _quitButton;


    public bool isPaused;


    private void OnEnable()
    {
        PlayerInputBehavior.OnPause += () => Debug.Log("Player wants to pause");

        _playButton.onClick.AddListener(() => Debug.Log("Play button click"));
        _quitButton.onClick.AddListener(() => Debug.Log("Quit button clicked"));
    }

    private void OnDisable()
    {
        PlayerInputBehavior.OnPause += () => Debug.Log("Player wants to pause");

        _playButton.onClick.RemoveListener(() => Debug.Log("Play button click"));
        _quitButton.onClick.RemoveListener(() => Debug.Log("Quit button clicked"));
    }


    public void TogglePause()
    {
        //first check if the game is paused or not
       if(isPaused)
        {
            // then check to see if the game was paused while time was slowed
            
        }

        


    }
}
