using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RollMoves : MonoBehaviourPunCallbacks
{
    /*public List<Transform> childs;
    public int pointsToVisit;
    public Transform pointer;
    private Transform[] currentPoints;
    private int pointIndex;
    private PhotonView pv;

    private void OnEnable()
    {
        pv = GetComponent<PhotonView>();

        pv.RPC("ResetRoll", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void ResetRoll()
    {
        pointIndex = 0;
        foreach (Transform child in transform)
        {
            childs.Add(child);
        }

        currentPoints = new Transform[pointsToVisit];

        for(int i = 0; i < currentPoints.Length; i++)
        {
            currentPoints[i] = childs[Random.Range(0, childs.Count-1)];
            if(i == currentPoints.Length-1 && currentPoints[i].GetComponent<Number>() == null)
                i--;
        }

        Number lastNumber = currentPoints[currentPoints.Length-1].GetComponent<Number>();

        GameManager.Instance.NumberOfMovements(lastNumber.num.Number);

        photonView.RPC("Roll", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Roll()
    {
        StartCoroutine("RollDice");
    }
    
    IEnumerator RollDice()
    {
        while(pointIndex < currentPoints.Length)
        {
            if(pointer.gameObject.activeInHierarchy && pointer.position != currentPoints[pointIndex].position)
            {
                pointer.position = Vector3.MoveTowards(pointer.position, currentPoints[pointIndex].position, .08f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                pointIndex++;
            }

            yield return new WaitForSeconds(.00000001f);
        }

        GameManager.Instance.asignMoves.Invoke();
        GameManager.Instance.DoneWithDice();
    }*/
}
