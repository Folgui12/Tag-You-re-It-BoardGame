using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int MovesAmount;
    public Node nextNode;
    public bool MyTurn;
    public bool diceRolled;
    public GameObject Ouija;
    private Button ouijaButton;

    // Start is called before the first frame update
    void Start()
    {
        MyTurn = true;
        diceRolled = false;
        Ouija = FindInactiveObjectWithTag("OUIJA");
        GameManager.Instance.asignMoves.AddListener(GetDiceNumber);
        ouijaButton = GameObject.FindWithTag("OuijaButton").GetComponent<Button>();
        ouijaButton.onClick.AddListener(RollTheDice);
    }

    // Update is called once per frame
    void Update()
    {
        if(MyTurn && diceRolled)
        {
            if(nextNode == null)
                nextNode = GameObject.Find("Initial Node").GetComponent<Node>();

            if(MovesAmount > 0)
            {
                if(transform.position == nextNode.transform.position && nextNode.neightbourds.Count == 1)
                    nextNode = nextNode.neightbourds[0];
                else
                {
                    Debug.Log("Elegí un destino");
                }
                transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, 0.5f * Time.deltaTime);
            }
        }
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

    public void GetDiceNumber()
    {
        MovesAmount = GameManager.Instance.movesToAsing;
        diceRolled = true;
    }
}
