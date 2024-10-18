using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neightbourds;
    public bool IsABifurcation;
}
