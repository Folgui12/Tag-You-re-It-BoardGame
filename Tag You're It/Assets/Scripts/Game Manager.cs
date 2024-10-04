using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int movesToAsing;
    public UnityEvent asignMoves;
    public List<int> PlayersIDsTurnList;
    public int TurnCounter;
    public PhotonView pv; 

    private bool FirstDiceRoll;
    private int index;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        pv = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        FirstDiceRoll = true; 
        PlayersIDsTurnList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayersTurns()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            List<int> randomizedPlayersTurns = PlayersIDsTurnList.OrderBy(x => Random.Range(0, PlayersIDsTurnList.Count)).ToList();
        }
        else
        {
            Debug.Log("WAIT UNTIL THE ROOM IS FULL");
        }
        
    }

    [PunRPC]
    public void AddPlayer(int playerID)
    {
        PlayersIDsTurnList.Add(playerID);
    }
}
