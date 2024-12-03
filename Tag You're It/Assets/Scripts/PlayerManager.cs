using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private GameObject ouijaButton;
    public bool MyTurn;
    public int PersonalTurn;
    public bool diceRolled;
    public int ID; 
    public PhotonView pv; 
    public Image[] Keys = new Image[4];   

    private int keysIndex;

    private CanvasGroup DiceCanvasGroup; 
    public Text DiceText;

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
        DiceText = FindInactiveObjectWithTag("DiceText").GetComponent<Text>();
        DiceCanvasGroup = DiceText.GetComponentInParent<CanvasGroup>();
        if (DiceCanvasGroup == null)
        {
                Debug.LogError("CanvasGroup no encontrado en el padre de DiceText.");
        }
        
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

    public void ShowDiceResult(int number)
    {
        if (DiceCanvasGroup == null)
        {
            Debug.LogError("DiceCanvasGroup no está asignado.");
            return;
        }

        DiceText.text = number.ToString();

        // Animación
        DiceCanvasGroup.alpha = 0;
        DiceCanvasGroup.transform.localScale = Vector3.zero;

        DiceCanvasGroup.DOFade(1, 0.5f);
        DiceCanvasGroup.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        // Opcional: Desaparece después de 2 segundos
        DiceCanvasGroup.DOFade(0, 0.5f).SetDelay(2f);
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

