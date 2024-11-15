using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int MovesAmount;
    public Node nextNode;
    private PhotonView pv;
    private bool goShortCut;
    private bool goSafeWay;
    private PlayerManager pjManager;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        pjManager = GetComponent<PlayerManager>();
        GameManager.Instance.asignMoves.AddListener(GetDiceNumber);
        GameManager.Instance.ShortCut.onClick.AddListener(GoShort);
        GameManager.Instance.SafeWay.onClick.AddListener(GoSafe);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMoving()
    {
        if(pjManager.MyTurn)
            StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        if(nextNode == null)
            nextNode = GameObject.Find("Initial Node").GetComponent<Node>();

        Debug.Log("Player Moving");

        while(MovesAmount > 0)
        {
            if(transform.position == nextNode.transform.position && !nextNode.IsABifurcation)
            {
                nextNode = nextNode.neightbourds[0];
                MovesAmount--;
            }
            else if(transform.position == nextNode.transform.position && nextNode.IsABifurcation)
            {
                GameManager.Instance.ActiveCrossRoadButtons();

                if(goShortCut)
                {
                    nextNode = nextNode.neightbourds[0];
                    MovesAmount--;
                    goShortCut = false;
                    GameManager.Instance.DeActiveCrossRoadButtons();
                }

                else if(goSafeWay)
                {
                    nextNode = nextNode.neightbourds[1];
                    MovesAmount--;
                    goSafeWay = false;
                    GameManager.Instance.DeActiveCrossRoadButtons();
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

        Debug.Log("DoneMoving");

        GameManager.Instance.ActivePassTurnButton();

    }

    public void GetDiceNumber()
    {
        if(pjManager.MyTurn)
            MovesAmount = GameManager.Instance.movesToAsing;

        StartMoving();
    }

    private void GoShort(){
        if(pv.IsMine)
            goShortCut = true;
    }

    private void GoSafe(){
        if(pv.IsMine)
            goSafeWay = true;
    }
}
