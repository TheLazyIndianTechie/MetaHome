using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteriorGameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI coinsCountText;

    // Start is called before the first frame update
    void Start()
    {
        int coins = (int)Variables.Saved.Get("Coins");
        coinsCountText.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
