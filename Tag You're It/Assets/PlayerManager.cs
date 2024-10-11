using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Button ouijaButton;
    public GameObject Ouija;
    
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 

    void Awake()
    {
        ouijaButton = GameObject.FindWithTag("OuijaButton").GetComponent<Button>();
        Ouija = FindInactiveObjectWithTag("OUIJA");
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTurn = false;
        diceRolled = false;
        ouijaButton.onClick.AddListener(RollTheDice);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void RollTheDice()
    {
        Ouija.SetActive(true);
    }

    public void MyTurnToPlay()
    {
        MyTurn = true;
    }
}
