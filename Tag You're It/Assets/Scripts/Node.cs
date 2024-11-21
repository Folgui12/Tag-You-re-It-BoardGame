using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject JumpScare;
    public List<Node> neightbourds;
    public bool IsABifurcation;

    public bool HasKey;

    public bool IsScare;

    public bool IsSafeSpot;

    public bool NoRoadAhead;

    public void ChanceToScare()
    {
        /*Debug.Log("Tried To Scare");

        int ran = Random.Range(0, 5);

        if(ran == 2 || ran == 4)
        {
            
        }*/

        StartCoroutine("ShowJumpScare");
    }

    IEnumerator ShowJumpScare()
    {
        Debug.Log("SCARED");

        JumpScare.SetActive(true);

        yield return new WaitForSeconds(1f);

        JumpScare.SetActive(false);
    }
}
