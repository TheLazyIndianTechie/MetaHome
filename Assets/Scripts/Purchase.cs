using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using TMPro;


public class Purchase : MonoBehaviour
{
    private Button _buyItem;

    public ulong _cost;

    [SerializeField]
    private TMP_Text displayCoins;

    public NFTMarketPlace nftPanel;

    private void Awake()
    {
        _buyItem = transform.GetComponent<Button>();
        //displayCoins.SetText("{0} Coins", (int)Variables.Saved.Get("Coins"));
    }

    void Start()
    {
        //This calls the event listener by passing in an int as a parameter
        _buyItem.onClick.AddListener(()=> PurchaseStuff(_cost));
    }

    private void PurchaseStuff(ulong myPurchasePrice)
    {

        //myPurchasePrice = _cost;

        Debug.Log("You have clicked the button!");
        Debug.Log("You have charged been charged " + myPurchasePrice);

        //ulong currentWallet = (ulong)Variables.Saved.Get("Coins");

       //currentWallet -= myPurchasePrice;

       // Variables.Saved.Set("Coins", currentWallet);

        //string DisplayCoins = Variables.Saved.Get("Coins").ToString();

        //int x = (int)Variables.Saved.Get("Coins");

        //displayCoins.SetText("{0} Coins", x);

        //if (nftPanel != null)
        //    nftPanel.CloseItemClicked();

    }



}