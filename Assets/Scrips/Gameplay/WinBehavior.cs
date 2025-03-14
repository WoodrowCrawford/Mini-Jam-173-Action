using UnityEngine;
using UnityEngine.UI;

public class WinBehavior : MonoBehaviour
{
   public delegate void WinEventHandler();
   public static event WinEventHandler onWinScreenShown;

  public static WinBehavior instance;

   public GameObject WinScreen;
   public Button Quitbutton;


    void OnEnable()
    {
        Quitbutton.onClick.AddListener(() => Application.Quit());
    }


    void OnDisable()
    {
        Quitbutton.onClick.RemoveListener(() => Application.Quit());
    }

    public void ShowWinScreen()
   {
      Cursor.visible = true;
      onWinScreenShown?.Invoke();
      WinScreen.SetActive(true);
   }
}
