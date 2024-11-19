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
    private PlayerManager pjManager;
    private bool OnABifurcation;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        pjManager = GetComponent<PlayerManager>();
        GameManager.Instance.asignMoves.AddListener(GetNumber);
        GameManager.Instance.ShortCut.onClick.AddListener(GoShort);
        GameManager.Instance.SafeWay.onClick.AddListener(GoSafe);

        OnABifurcation = false;
    }

    IEnumerator Move()
    {
        Debug.Log(MovesAmount);
        while(MovesAmount > 0 && pjManager.MyTurn && !OnABifurcation)
        {
            Debug.Log("Moving");
            if(transform.position == nextNode.transform.position && !nextNode.IsABifurcation)
            {
                if(MovesAmount == 1)
                {
                    if(nextNode.HasKey)
                        pjManager.NewKey();

                    if(nextNode.IsScare)
                        nextNode.ChanceToScare();
                }
        
                nextNode = nextNode.neightbourds[0];
                MovesAmount--;
            }

            else if(transform.position == nextNode.transform.position && nextNode.IsABifurcation)
            {
                OnABifurcation = true;

                if(pjManager.MyTurn && pv.IsMine)
                    GameManager.Instance.ActiveCrossRoadButtons();
            }
            
            transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

        if(MovesAmount == 0)
            GameManager.Instance.ActivePassTurnButton();
    }

    private void GetNumber()
    {
        pv.RPC("GetDiceNumber", RpcTarget.AllBuffered);
        StartCoroutine("Move");
    }

    [PunRPC]
    public void GetDiceNumber()
    {
        if(pjManager.MyTurn && pv.IsMine)
        {
            MovesAmount = GameManager.Instance.movesToAsing;
        }
            
    }

    private void GoShort()
    {
        if(pjManager.MyTurn)
        {
            nextNode = nextNode.neightbourds[0];
            OnABifurcation = false;
        }

        GameManager.Instance.DeActiveCrossRoadButtons();

        StartCoroutine("Move");
            
    }

    private void GoSafe()
    {
        if(pjManager.MyTurn)
        {
            OnABifurcation = false;
            nextNode = nextNode.neightbourds[1];
        }

        GameManager.Instance.DeActiveCrossRoadButtons();

        StartCoroutine("Move");
            
    }
}
