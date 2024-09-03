using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint; 

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnPoint.position.x, spawnPoint.position.y, -0.1f), Quaternion.identity);
        PlayerCounter.Instance.NewPlayer();
    }
}
