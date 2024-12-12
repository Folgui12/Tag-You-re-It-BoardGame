using Photon.Pun;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviourPunCallbacks
{
    public static SceneLoader Instance;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        GoToMenu();
    }

    public void YouWin()
    {
        SceneManager.LoadScene("Win");
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Lose");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
