using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class PlayerInfoTracker : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameDisplay;


    private void Start()
    {
        ReadPlayerDataFromVisualScripting();
    }


    public void ReadPlayerDataFromVisualScripting()
    {
        string playerName = (string)Variables.Saved.Get("PlayerName");

        nameDisplay.SetText(playerName);
        Debug.Log("Current player is " + Variables.Saved.Get("PlayerName"));

    }
}