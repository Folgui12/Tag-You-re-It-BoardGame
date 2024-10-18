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

    void Awake()
    {
        pv = GetComponent<PhotonView>();
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
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        if(nextNode == null)
            nextNode = GameObject.Find("Initial Node").GetComponent<Node>();

        while(MovesAmount > 0)
        {
            if(MovesAmount > 0 && pv.IsMine)
            {
                if(transform.position == nextNode.transform.position && !nextNode.IsABifurcation)
                {
                    nextNode = nextNode.neightbourds[0];
                    MovesAmount--;
                }
                else if(nextNode.IsABifurcation)
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
            }
            yield return new WaitForSeconds(0.0001f);
        }
    }

    public void GetDiceNumber()
    {
        MovesAmount = GameManager.Instance.movesToAsing;
        StartMoving();
    }

    private void GoShort(){
        goShortCut = true;
    }

    private void GoSafe(){
        goSafeWay = true;
    }
}
