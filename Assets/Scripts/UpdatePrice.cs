using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using TMPro;
public class UpdatePrice : MonoBehaviour
{
    [SerializeField]
    private TMP_Text displayCoins;
    // Start is called before the first frame update

    private void Awake()
    {
        displayCoins.SetText("{0} Coins", (int)Variables.Saved.Get("Coins"));
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        displayCoins.SetText("{0} Coins", (int)Variables.Saved.Get("Coins"));
    }
}
