using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class JoinServerUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMPro.TMP_InputField createInput;
    [SerializeField] private TMPro.TMP_InputField joinInput;
    [SerializeField] private TMPro.TMP_Text errorMessage;

    void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    void OnDestroy()
    {
        createButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
    }

    private void CreateRoom()
    {
        RoomOptions roomConfiguration = new RoomOptions();
        roomConfiguration.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(createInput.text, roomConfiguration);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("BoardScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        DisplayError("Error al crear sala: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        DisplayError("Error al unirse a la sala: " + message);
    }

    private void DisplayError(string error)
    {
        if (errorMessage != null)
        {
            errorMessage.text = error;
            errorMessage.gameObject.SetActive(true);

            // Opcional: Ocultar el mensaje después de unos segundos
            Invoke(nameof(HideError), 5f);
        }
    }

    private void HideError()
    {
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

}
