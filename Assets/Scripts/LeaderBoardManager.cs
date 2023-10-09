using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
public class LeaderBoardManager : MonoBehaviour
{
    private int index = 0;
  
    public TextMeshProUGUI playernames;

    public TextMeshProUGUI[] playerCoins;

    

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerEmail;
    public TextMeshProUGUI playerCoin;
    //player info section (test) 
    void Awake()
    {
        int testCoins = (int)Variables.Saved.Get("Coins");
        playerCoin.text = testCoins.ToString();
        playerEmail.text = (string)Variables.Saved.Get("PlayerEmail");
        playerName.text = (string)Variables.Saved.Get("PlayerName");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetTopPlayerName(string displayName)
    {
        //Variables.Saved.Set("PlayerName", displayName);
   
        playernames.text += displayName + "\n";
      
    }

    public void ResetGui()
    {
        playernames.text = "";
        for (int i = 0; i < playerCoins.Length; i++)
        {
            playerCoins[i].text = "0 Coins";
        }
        index = 0;
    }

    public void GetTopPlayerCoin(int coins)
    {
        if (index > 4)
        {
            index = 0;
        }
        //Variables.Saved.Set("Coins", coins);
        playerCoins[index].text = coins.ToString() + " Coins";
        //int testCoins = (int)Variables.Saved.Get("Coins");
       // testCoinsText.text = testCoins.ToString();
        index++;
    }
}
