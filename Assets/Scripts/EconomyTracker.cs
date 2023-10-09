using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EconomyTracker : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinDisplay;

    [SerializeField]
    private string currency;

    private void Start()
    {
        ReadCoinsFromVisualScripting();
    }


    public void ReadCoinsFromVisualScripting()
    {

        int currentWallet;
        currentWallet = (int)Variables.Saved.Get("Coins");

        coinDisplay.SetText("{0}"+currency, currentWallet);
        Debug.Log("Coins are set to " + Variables.Saved.Get("Coins"));


    }
}
