using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Scripting;
using Unity.VisualScripting;

public class RollMoves : MonoBehaviourPunCallbacks
{
    public List<Transform> childs;
    public int pointsToVisit;
    public Transform pointer;
    private Transform[] currentPoints;
    private int pointIndex;
    private PhotonView photonView;

    private void OnEnable()
    {
        photonView = GetComponent<PhotonView>();
        photonView.RPC("ResetRoll", RpcTarget.AllBuffered);
        photonView.RPC("StartAnimation", RpcTarget.AllBuffered);
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
    }

    [PunRPC]
    private void StartAnimation()
    {
        if(photonView.IsMine)
            StartCoroutine("RollDice");
    }
    
    IEnumerator RollDice()
    {
        Number lastNumber = null;

        while(pointIndex < currentPoints.Length)
        {
            if(pointer.gameObject.activeInHierarchy && pointer.position != currentPoints[pointIndex].position)
            {
                pointer.position = Vector3.MoveTowards(pointer.position, currentPoints[pointIndex].position, .008f);
                lastNumber = currentPoints[pointIndex].GetComponent<Number>();
            }
            else
            {
                yield return new WaitForSeconds(1f);
                pointIndex++;
            }

            yield return new WaitForSeconds(.000001f);
            Debug.Log("Player Moving");
        }

        if(lastNumber!=null)
        {
            GameManager.Instance.movesToAsing = lastNumber.num.Number;
            GameManager.Instance.asignMoves.Invoke();
        }

        GameManager.Instance.DoneWithDice();
    }

}
