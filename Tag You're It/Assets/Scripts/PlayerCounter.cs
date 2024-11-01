using Photon.Realtime;
using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    public static PlayerCounter Instance;

    public int Players;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        Players = 0; 
    }

    public void NewPlayer()
    {
        Players += 1;
        Debug.Log("Player "+ Players +" Joined");
    }
}
