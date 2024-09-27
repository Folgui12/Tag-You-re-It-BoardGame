using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neightbourds;
    public bool alreadyVisited;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            
            if(neightbourds.Count == 1)
            {
                Debug.Log("Going To Next Node");
                player.nextNode = neightbourds[0];
            }
            else
            {
                Debug.Log("Decidir el Camino");
            }
            
        }
    }
}
