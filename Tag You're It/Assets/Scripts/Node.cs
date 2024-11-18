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

    public void ChanceToScare()
    {
        Debug.Log("Tried To Scare");
        if(Random.Range(0, 10) == 7)
        {
            StartCoroutine("ShowJumpScare");
        }
    }

    IEnumerator ShowJumpScare()
    {
        JumpScare.SetActive(true);
        //Activar el sonido

        yield return new WaitForSeconds(0.5f);

        JumpScare.SetActive(false);
    }
}
