using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject StartGame;
    [SerializeField] private Button EndTurnButton;
    //[SerializeField] private GameObject OuijaBoard;
    //[SerializeField] private GameObject OuijaPoints;
    [SerializeField] private List<AudioClip> Nums = new();
    [SerializeField] private AudioSource DiceNumberSpeaker;

    public static GameManager Instance;
    public Button ShortCut;
    public Button SafeWay;
    public int movesToAsing;
    public UnityEvent asignMoves;
    public int TurnCounter;
    public PhotonView pv; 

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
        EndTurnButton.onClick.AddListener(NextTurn);
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

    public void ActivePassTurnButton() => EndTurnButton.gameObject.SetActive(true);

    public void DeactivePassTurnButton() => EndTurnButton.gameObject.SetActive(false);

    public void Go()
    {
        pv.RPC("currentTurn", RpcTarget.AllBuffered, turnIndex);
    }

    [PunRPC]
    private void currentTurn(int index)
    {
        switch (index)
        {
            case 1:
                PJ1.MyTurn = true;
                PJ2.MyTurn = false;
                PJ3.MyTurn = false;
                PJ4.MyTurn = false;
                Debug.Log("Player 1 Turn");
                PJ1.TurnToRoll();
                break;

            case 2:
                PJ1.MyTurn = false;
                PJ2.MyTurn = true;
                PJ3.MyTurn = false;
                PJ4.MyTurn = false;
                Debug.Log("Player 2 Turn");
                PJ2.TurnToRoll();
                break;

            case 3:
                PJ1.MyTurn = false;
                PJ2.MyTurn = false;
                PJ3.MyTurn = true;
                PJ4.MyTurn = false;
                Debug.Log("Player 3 Turn");
                break;

            case 4:
                PJ1.MyTurn = false;
                PJ2.MyTurn = false;
                PJ3.MyTurn = false;
                PJ4.MyTurn = true;
                Debug.Log("Player 4 Turn");
                break;
            
            default:
                break; 
        }
    }

    private void NextTurn()
    {
        pv.RPC("NextPlayerTurn", RpcTarget.All);
    }  

    [PunRPC]
    public void NextPlayerTurn()
    {
        turnIndex++; 

        if(turnIndex > PhotonNetwork.CurrentRoom.PlayerCount)
        {
            turnIndex = 1;
        }

        Go();
        DeactivePassTurnButton();
    }

    [PunRPC]
    public void AsignPlayer(int pjIndex, int ID)
    {
        PhotonView pjView = PhotonView.Find(ID);

        switch(pjIndex)
        {
            case 1:
                PJ1 = pjView.gameObject.GetComponent<PlayerManager>();
                PJ1.ID = pjIndex;
                PJ1.Keys = GameObject.Find("P1").GetComponentsInChildren<Image>();
                PJ1.DiceText = GameObject.Find("DiceNumberP1").GetComponent<Text>();
                break;

            case 2:
                PJ2 = pjView.gameObject.GetComponent<PlayerManager>();
                PJ2.ID = pjIndex;
                PJ2.Keys = GameObject.Find("P2").GetComponentsInChildren<Image>();
                PJ2.DiceText = GameObject.Find("DiceNumberP2").GetComponent<Text>();
                break;

            case 3:
                PJ3 = pjView.gameObject.GetComponent<PlayerManager>();
                PJ3.ID = pjIndex;
                PJ3.Keys = GameObject.Find("P3").GetComponentsInChildren<Image>();
                PJ3.DiceText = GameObject.Find("DiceNumberP3").GetComponent<Text>();
                break;

            case 4:
                PJ4 = pjView.gameObject.GetComponent<PlayerManager>();
                PJ4.ID = pjIndex;
                PJ4.Keys = GameObject.Find("P4").GetComponentsInChildren<Image>();
                PJ4.DiceText = GameObject.Find("DiceNumberP4").GetComponent<Text>();
                break;
            
            default:
                break; 
        }
    }

    /*public void NumberOfMovements(int moves)
    {
        movesToAsing = moves;
    }*/

    public void RollDice()
    {
        movesToAsing = Random.Range(1, 7);

        Debug.Log(movesToAsing);

        pv.RPC("CallDice", RpcTarget.All, movesToAsing);

        asignMoves.Invoke();
    }

    public void CheckWinner()
    {
        if(PJ1.CheckWin())
        {
            if(pv.IsMine)
                SceneLoader.Instance.YouWin();
            else
                SceneLoader.Instance.YouLose();
        }
        else if(PJ2.CheckWin())
        {
            if(pv.IsMine)
                SceneLoader.Instance.YouWin();
            else
                SceneLoader.Instance.YouLose();
        }
        else if(PJ3.CheckWin())
        {
            if(pv.IsMine)
                SceneLoader.Instance.YouWin();
            else
                SceneLoader.Instance.YouLose();
        }
        else if(PJ4.CheckWin())
        {
            if(pv.IsMine)
                SceneLoader.Instance.YouWin();
            else
                SceneLoader.Instance.YouLose();
        }
    }

    /*[PunRPC]
    private void ShowOuija()
    {
        OuijaBoard.SetActive(true);
        OuijaPoints.SetActive(true);
    }

    public void DoneWithDice()
    {
        pv.RPC("HideOuija", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void HideOuija()
    {
        OuijaBoard.SetActive(false);
        OuijaPoints.SetActive(false);
    }*/

    [PunRPC]
    private void CallDice(int diceNumber)
    {
        DiceNumberSpeaker.clip = Nums[diceNumber-1];
        DiceNumberSpeaker.Play();
    }
}
