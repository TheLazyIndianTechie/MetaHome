using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void updateCoinsCount();

    [DllImport("__Internal")]
    private static extern void addOrDeductCoins(int coins);


    public static string avatarUrlOfPlayer;

    public bool isServerBuild = false;

    [SerializeField]
    private Mirror.NetworkManager networkManager;

    public static bool testMode = false;


    //GUI
    [SerializeField]
    private TextMeshProUGUI coinsCountText;

    [SerializeField]
    private TextMeshProUGUI playerName;

    [SerializeField]
    private Image playerAvatarImage;

    // [SerializeField]
    //private GameObject disconnectBtn;

    private int currentCoins;
    private void Awake()
    {
        playerName.text = (string)Variables.Saved.Get("PlayerName");
        int coins = (int)Variables.Saved.Get("Coins");
        coinsCountText.text = coins.ToString();

        currentCoins = coins;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServerBuild)
        {
            networkManager.StartServer();
        }
        else
        {
            networkManager.StartClient();
        }

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //todo something platform specific.
        }
    }
    
    public void UpdateCoinText(int coins)
    {
        Variables.Saved.Set("Coins", coins);
        coinsCountText.text = coins.ToString();
        currentCoins = coins;
    }

    public void OnCloseLeaderBoard()
    {
        updateCoinsCount();
    }
    
    public void AddCoins(int coins)
    {
        addOrDeductCoins(coins);
    }

    public void DeductCoins(int coins)
    {
        if (currentCoins >= coins)
        {
            addOrDeductCoins(-coins);
        }
       
    }

    public void StopClient()
    {
        networkManager.StopClient();
    }
}
