using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowMovementNumber : MonoBehaviour
{
    public static ShowMovementNumber Instance;

    public GameObject DiceShower;

    public Text numberToShow;

    public PhotonView pv;

    private int number; 

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        pv = GetComponent<PhotonView>();
    }

    public void SetRPCNumber(int num)
    {
        number = num;

        StartCoroutine("NumberOnScreen");
    }

    IEnumerator NumberOnScreen()
    {
        numberToShow.text = number.ToString();

        yield return new WaitForSeconds(1f);

        DiceShower.SetActive(false);
    }

}
