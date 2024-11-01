using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void GoToConnectionLobby()
    {
        SceneManager.LoadScene("JoinServer");
    }   

    public void GoToInstructionsScreen()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
