using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;   

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private Image P1square;
    [SerializeField] private Image P2square;
    [SerializeField] private Image P3square;
    [SerializeField] private Image P4square;

    private PhotonView pv;
    private GameObject pj;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {

        pj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint.position.x, spawnPoint.position.y, -0.1f), Quaternion.identity);
        
        int playerIndex = PhotonNetwork.PlayerList.Length;

        pv.RPC("PlayerJoinned", RpcTarget.AllBuffered, pj.GetComponent<PhotonView>().ViewID, playerIndex);    
    }

    [PunRPC]
    private void PlayerJoinned(int playerID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerID);

        if(targetPhotonView != null)
        {
            switch (playerIndex)
            {
                case 1:
                    targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    P1square.color = Color.red;
                    break;

                case 2:
                    targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    P2square.color = Color.blue;
                    break;

                case 3:
                    targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    P3square.color = Color.yellow;
                    break;

                default:
                    targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    P4square.color = Color.green;
                    break;
            }
        }
    }
}
