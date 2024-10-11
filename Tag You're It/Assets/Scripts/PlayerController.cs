using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool playersTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerCanPlay()
    {
        playersTurn = true;
    }

    public void PlayerCantPlay()
    {
        playersTurn = false;
    }
}
