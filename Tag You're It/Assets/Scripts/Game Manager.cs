using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject StartGame;
    [SerializeField] private GameObject OuijaBoard;
    public static GameManager Instance;
    public Button ShortCut;
    public Button SafeWay;
    public int movesToAsing;
    public UnityEvent asignMoves;
    public int TurnCounter;
    public PhotonView pv; 

    private bool FirstDiceRoll;
    private PlayerManager PJ1;
    private PlayerManager PJ2;
    private PlayerManager PJ3;
    private PlayerManager PJ4;
    public int turnIndex = 1;

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
        FirstDiceRoll = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 2 && PhotonNetwork.IsMasterClient)
        {
            StartGame.SetActive(true);
        }
    }

    public void ActiveCrossRoadButtons()
    {
        ShortCut.gameObject.SetActive(true);
        SafeWay.gameObject.SetActive(true);
    }
    public void DeActiveCrossRoadButtons()
    {
        ShortCut.gameObject.SetActive(false);
        SafeWay.gameObject.SetActive(false);
    }

    public void Go()
    {
        pv.RPC("currentTurn", RpcTarget.AllBuffered, turnIndex);
    }

    [PunRPC]
    private void currentTurn(int index)
    {
        Debug.Log("Asing Turns");
        switch (index)
        {
            case 1:
                PJ1.MyTurn = true;
                PJ2.MyTurn = false;
                //PJ3.MyTurn = false;
                //PJ4.MyTurn = false;
                PJ1.ActiveOuijaButton(); 
                break;

            case 2:
                PJ1.MyTurn = false;
                PJ2.MyTurn = true;
                //PJ3.MyTurn = false;
                //PJ4.MyTurn = false;
                PJ2.ActiveOuijaButton(); 
                break;

            case 3:
                PJ1.MyTurn = false;
                PJ2.MyTurn = false;
                //PJ3.MyTurn = true;
                //PJ4.MyTurn = false;
                //PJ3.ActiveOuijaButton(); 
                break;

            case 4:
                PJ1.MyTurn = false;
                PJ2.MyTurn = false;
                // PJ3.MyTurn = false;
                // PJ4.MyTurn = true;
                //PJ4.ActiveOuijaButton(); 
                break;
            
            default:
                break; 
        }
    }

    [PunRPC]
    public void NextTurn()
    {
        turnIndex++; 
    }

    [PunRPC]
    public void AsignPlayer(int pjIndex, int ID)
    {
        PhotonView pjView = PhotonView.Find(ID);

        switch(pjIndex)
        {
            case 1:
                PJ1 = pjView.gameObject.GetComponent<PlayerManager>();
                Debug.Log("Player 1 Assigned"); 
                break;

            case 2:
                PJ2 = pjView.gameObject.GetComponent<PlayerManager>();
                Debug.Log("Player 2 Assigned"); 
                break;

            case 3:
                //PJ3 = pjView.gameObject.GetComponent<PlayerManager>();
                break;

            case 4:
                // PJ4 = pjView.gameObject.GetComponent<PlayerManager>();
                break;
            
            default:
                break; 
        }
    }

    public void RollDice()
    {
        pv.RPC("ShowOuija", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void ShowOuija()
    {
        OuijaBoard.SetActive(true);
    }
}
