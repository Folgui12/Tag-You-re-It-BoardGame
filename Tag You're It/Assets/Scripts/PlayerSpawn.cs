using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;   

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint1; 
    [SerializeField] private Transform spawnPoint2; 
    [SerializeField] private Transform spawnPoint3; 
    [SerializeField] private Transform spawnPoint4; 
    [SerializeField] private Image P1square;
    [SerializeField] private Image P2square;
    [SerializeField] private Image P3square;
    [SerializeField] private Image P4square;

    [SerializeField] private Sprite pj1;
    [SerializeField] private Sprite pj2;
    [SerializeField] private Sprite pj3;
    [SerializeField] private Sprite pj4;
    //Sprite player1Sprite = Resources.Load<Sprite>("PERSONAJE1");
    // Sprite player2Sprite = Resources.Load<Sprite>("PERSONAJE2");
    // Sprite player3Sprite = Resources.Load<Sprite>("PERSONAJE3");
    // Sprite playerDefaultSprite = Resources.Load<Sprite>("PERSONAJE4");

    private PhotonView pv;
    private GameObject pj;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        int playerIndex = PhotonNetwork.PlayerList.Length;

        SpawnPlayer(playerIndex);

        PlayerManager pjManager = pj.GetComponent<PlayerManager>();
        PlayerMovement pjMov = pj.GetComponent<PlayerMovement>();

        pjMov.nextNode = GameObject.Find("Initial Node").GetComponent<Node>();

        pjManager.ID = playerIndex;

        pv.RPC("PlayerJoinned", RpcTarget.AllBuffered, pj.GetComponent<PhotonView>().ViewID, playerIndex);

        GameManager.Instance.pv.RPC("AsignPlayer", RpcTarget.AllBuffered, playerIndex, pj.GetComponent<PhotonView>().ViewID);
    }

    private void SpawnPlayer(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                pj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint1.position.x, spawnPoint1.position.y, -0.1f), Quaternion.identity);
                break;

            case 2:
                pj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint2.position.x, spawnPoint2.position.y, -0.1f), Quaternion.identity);
                break;

            case 3:
                pj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint3.position.x, spawnPoint3.position.y, -0.1f), Quaternion.identity);
                break;

            case 4:
                pj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint4.position.x, spawnPoint4.position.y, -0.1f), Quaternion.identity);
                break;

            default:
                break;
        }
    }

    [PunRPC]
    private void PlayerJoinned(int playerID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerID);

        if (targetPhotonView != null)
        {
           

            // Accede al SpriteRenderer del objeto
            SpriteRenderer spriteRenderer = targetPhotonView.gameObject.GetComponent<SpriteRenderer>();

            switch (playerIndex)
            {
                case 1:
                    spriteRenderer.sprite = pj1;
                    // Si necesitas cambiar otra imagen relacionada
                    break;

                case 2:
                    spriteRenderer.sprite = pj2;
                   
                    break;

                case 3:
                    spriteRenderer.sprite = pj3;
                   
                    break;

                default:
                    spriteRenderer.sprite = pj4;
                    
                    break;
            }
        }
    }
}
