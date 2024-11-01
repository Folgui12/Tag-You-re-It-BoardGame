using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameObject ouijaButton;
    private GameObject Ouija;
    
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 
    public PhotonView pv; 
    int playerIndex;

    void Awake()
    {
        ouijaButton = FindInactiveObjectWithTag("OuijaButton");
        Ouija = FindInactiveObjectWithTag("OUIJA");
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
        /*if(MyTurn && pv.IsMine && !diceRolled)
        {
            diceRolled = true; 
        }*/
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

    public void ActiveOuijaButton()
    {
        if(MyTurn && pv.IsMine && !diceRolled)
            ouijaButton.SetActive(true);
    }
}
