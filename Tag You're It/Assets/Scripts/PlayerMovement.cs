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

    IEnumerator Move()
    {
        if(nextNode == null)
            nextNode = GameObject.Find("Initial Node").GetComponent<Node>();

        while(MovesAmount > 0 && pjManager.MyTurn)
        {
            if(transform.position == nextNode.transform.position && !nextNode.IsABifurcation)
            {
                nextNode = nextNode.neightbourds[0];

                if(MovesAmount == 1)
                {
                    if(nextNode.HasKey)
                        pjManager.NewKey();

                    if(nextNode.IsScare)
                        nextNode.ChanceToScare();
                }
                     
                MovesAmount--;
            }
            else if(transform.position == nextNode.transform.position && nextNode.IsABifurcation)
            {
                if(pjManager.MyTurn)
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

        GameManager.Instance.ActivePassTurnButton();

    }

    public void GetDiceNumber()
    {
        Debug.Log(pv.IsMine);
        if(pv.IsMine)
        {
            Debug.Log("Move");
            MovesAmount = GameManager.Instance.movesToAsing;
            StartCoroutine("Move");
        }
            
    }

    private void GoShort(){
        if(pjManager.MyTurn)
            goShortCut = true;
    }

    private void GoSafe(){
        if(pjManager.MyTurn)
            goSafeWay = true;
    }
}
