using UnityEngine;
using UnityEngine.UI;


public class PauseBehavior : MonoBehaviour
{
   
    public delegate void PauseEventHandler();

    public static event PauseEventHandler onGamePaused;
    public static event PauseEventHandler onGameUnpaused;

    [Header("Game Objects")]
   [SerializeField] private GameObject _pauseScreen;
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _quitButton;

   [SerializeField] private bool _gameWasPausedInSlowMotion;
   [SerializeField] private bool _gameWasPausedInNormalTime;


    public static bool isPaused;

    private void OnEnable()
    {
        PlayerInputBehavior.OnPause += () => TogglePause();

        _playButton.onClick.AddListener(() => TogglePause());
        _quitButton.onClick.AddListener(() => Application.Quit());
    }

    private void OnDisable()
    {
        PlayerInputBehavior.OnPause += () => TogglePause();

        _playButton.onClick.RemoveListener(() => TogglePause());
        _quitButton.onClick.RemoveListener(() => Application.Quit());
    }


    public void TogglePause()
    {
        //first check if the game is paused or not

        //if the game is not paused
       if(!isPaused)
        {
            //if the game was paused while the time was slowed
            if(TimeManipulationBehavior.isTimeSlowed)
            {
               //make a note that the game was paused in slow motion
               _gameWasPausedInSlowMotion = true;

                //set is paused to true
                isPaused = true;

               //pause the game
                Time.timeScale = 0f;

               //show the pause screen
               _pauseScreen.SetActive(true);

               onGamePaused?.Invoke();

               
            }

            else if(!TimeManipulationBehavior.isTimeSlowed)
            {
                //make a note that the game was paused in slow motion
                _gameWasPausedInNormalTime = true;

                //set is paused to true
                isPaused = true;

                //pause the game
                Time.timeScale = 0;
            

                //show the pause screen
                _pauseScreen.SetActive(true);

                onGamePaused?.Invoke();

            }
            
        }

        //else if the game is already paused
        else if(isPaused)
        {
            //first check how the game was paused

            //if the game was paused in normal time
            if(_gameWasPausedInNormalTime)
            {
                //unpause the game normally
                Time.timeScale = 1f;

                //set is paused to false
                isPaused = false;

                //hide the pause screen
                _pauseScreen.SetActive(false);

                onGameUnpaused?.Invoke();

            }

            else if(_gameWasPausedInSlowMotion)
            {
                //unpause the game back in slow motion time
                Time.timeScale = 0.3f;

                //set is paused to false
                isPaused = false;

                //hide the pause screen
                _pauseScreen.SetActive(false);

                onGameUnpaused?.Invoke();
            }


        }

        


    }
}
