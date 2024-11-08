using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject ouijaButton;
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 
    public PhotonView pv; 

    void Awake()
    {
        ouijaButton = FindInactiveObjectWithTag("OuijaButton");
        pv = GetComponent<PhotonView>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTurn = false;
        diceRolled = false;
    }

    // Update is called once per frame
    void Update()
    {
        pv.RPC("ActiveOuijaButton", RpcTarget.AllBuffered);
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
}
