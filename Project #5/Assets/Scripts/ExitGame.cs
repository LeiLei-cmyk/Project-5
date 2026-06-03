using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void doExitGame() {
        Application.Quit();
        print("Game is exiting");
    }
}
