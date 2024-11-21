using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private GameObject ouijaButton;
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 
    public PhotonView pv; 
    public Image[] Keys = new Image[4];
    public Text DiceText;

    private int keysIndex;

    private GameObject PauseMenu;
    private bool PauseActive;
    private Button LeaveRoomButton;

    void Awake()
    {
        keysIndex = 0;
        ouijaButton = FindInactiveObjectWithTag("OuijaButton");
        pv = GetComponent<PhotonView>(); 
        PauseMenu = FindInactiveObjectWithTag("Pause");
        PauseActive = false;
        LeaveRoomButton = FindInactiveObjectWithTag("Leaver").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTurn = false;
        diceRolled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseActive = !PauseActive;

            PauseMenu.SetActive(PauseActive);
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

        if(keysIndex > 4)
            keysIndex = 4;
    }

    [PunRPC]
    private void ShowKey()
    {
        Keys[keysIndex].color = new Color(1, 1, 1);
    }

    public bool CheckWin()
    {
        return keysIndex == 4 && MyTurn;
    }
}

