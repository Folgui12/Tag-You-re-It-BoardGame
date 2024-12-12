using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource steps;
    public int MovesAmount;
    public Node nextNode;
    private PhotonView pv;
    private PlayerManager pjManager;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        pjManager = GetComponent<PlayerManager>();
        GameManager.Instance.asignMoves.AddListener(GetNumber);
        GameManager.Instance.ShortCut.onClick.AddListener(GoShort);
        GameManager.Instance.SafeWay.onClick.AddListener(GoSafe);
    }

    IEnumerator Move()
    {
        while(MovesAmount > 0 && pjManager.MyTurn)
        {
            if(!steps.isPlaying && pv.IsMine)
                steps.Play();

            if(transform.position == nextNode.transform.position)
            {
                if(!nextNode.IsABifurcation)
                {
                    if(MovesAmount == 1)
                    {
                        if(nextNode.HasKey)
                            pjManager.NewKey();

                        if(nextNode.IsScare)
                            nextNode.ChanceToScare();
                    }

                    if(nextNode.name != "BeforeNoRoadAhead")
                    {
                        if(nextNode.name == "FinalNode")
                        {
                            GameManager.Instance.CheckWinner();
                        }

                        nextNode = nextNode.neightbourds[0];
                    }
                        
                    else
                    {
                        if(!nextNode.NoRoadAhead)
                        {
                            nextNode = nextNode.neightbourds[0];
                        }

                        if(nextNode.NoRoadAhead)
                        {
                            nextNode = nextNode.neightbourds[1];
                            nextNode.NoRoadAhead = false;
                            continue;
                        }

                        nextNode.NoRoadAhead = true;
                    }
                    
                    MovesAmount--;
                }

                else if(nextNode.IsABifurcation)
                {
                    if(pjManager.MyTurn && pv.IsMine)
                        GameManager.Instance.ActiveCrossRoadButtons();
                    
                    MovesAmount--;

                    break;
                }

            }

            transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

        steps.Stop();

        if(MovesAmount == 0 && pjManager.MyTurn && pv.IsMine)
        {
            pv.RPC("SetDiceText", RpcTarget.All, 0);
            pjManager.DiceText.text = " ";
            GameManager.Instance.ActivePassTurnButton();
        }
            
    }

    private void GetNumber()
    {
        if(pjManager.MyTurn)
        {
            MovesAmount = GameManager.Instance.movesToAsing;
            pv.RPC("SetDiceText", RpcTarget.All, MovesAmount);
        }
            
        pv.RPC("MovePlayer", RpcTarget.All);
    }

    [PunRPC]
    private void SetDiceText(int number)
    {
        pjManager.DiceText.text = number.ToString();
        if(number > 0)
        {
            ShowMovementNumber.Instance.DiceShower.SetActive(true);
            ShowMovementNumber.Instance.SetRPCNumber(number);
        }
    }


    [PunRPC]
    public void MovePlayer()
    {
        StartCoroutine("Move");
    }

    private void GoShort()
    {
        if(pjManager.MyTurn)
        {
            nextNode = nextNode.neightbourds[0];
        }

        GameManager.Instance.DeActiveCrossRoadButtons();

        pv.RPC("MovePlayer", RpcTarget.All);
            
    }

    private void GoSafe()
    {
        if(pjManager.MyTurn)
        {
            nextNode = nextNode.neightbourds[1];
        }

        GameManager.Instance.DeActiveCrossRoadButtons();

        pv.RPC("MovePlayer", RpcTarget.All);
            
    }
}
