using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameObject ouijaButton;
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 
    public PhotonView pv; 
    public Image[] Keys = new Image[4];

    private int keysIndex;

    void Awake()
    {
        keysIndex = 0;
        ouijaButton = FindInactiveObjectWithTag("OuijaButton");
        pv = GetComponent<PhotonView>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTurn = false;
        diceRolled = false;
    }

    GameObject FindInactiveObjectWithTag(string tag)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(tag) && !obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    [PunRPC]
    public void ActiveOuijaButton()
    {
        if(MyTurn && pv.IsMine && !diceRolled)
        {
            ouijaButton.SetActive(true);
        }
        else if(MyTurn && !pv.IsMine && !diceRolled)
        {
            ouijaButton.SetActive(false);
        }
    }

    public void DiceRolled()
    {
        pv.RPC("changeDiceState", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void changeDiceState()
    {
        diceRolled = true;
    }

    public void TurnToRoll()
    {
        diceRolled = false;
        pv.RPC("ActiveOuijaButton", RpcTarget.AllBuffered);
    }
    
    public void NewKey()
    {
        pv.RPC("ShowKey", RpcTarget.AllBuffered);

        keysIndex++;
    }

    [PunRPC]
    private void ShowKey()
    {
        Keys[keysIndex].color = new Color(1, 1, 1);
    }
}
