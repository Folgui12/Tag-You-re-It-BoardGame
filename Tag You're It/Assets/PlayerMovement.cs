using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int MovesAmount;
    public Node nextNode;
    private PhotonView pv;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        GameManager.Instance.asignMoves.AddListener(GetDiceNumber);
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
                else
                {
                    Debug.Log("Elegí un camino");
                }
                transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, 0.5f * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void GetDiceNumber()
    {
        MovesAmount = GameManager.Instance.movesToAsing;
    }
}
