using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class PlayerData 
{
    //Player info
    public string PlayerName = (string)Variables.Saved.Get("PlayerName");
    public string PlayerMobile = (string)Variables.Saved.Get("PlayerMobile");
    public string PlayerEmail = (string)Variables.Saved.Get("PlayerEmail");
    

    //Avatar Details
    public string AvatarURL = (string)Variables.Saved.Get("AvatarURL");

    // Economy Info
    public int Coins = (int)Variables.Saved.Get("Coins");

    //Player Game Info
    public int WowRunScore = (int)Variables.Saved.Get("WowRunScore");
    public int WowCardsScore = (int)Variables.Saved.Get("WowCardsScore");

}
