using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using WowQuests;

public class EconomyManager : MonoBehaviour
{

    public static event Action<int, int> OnQuestParamsChecked;

    private void OnEnable()
    {
        CollectiblesManager.OnCollectiblePickedUp += HandleCrystalCollectionEconomy;
    }

    private void OnDisable()
    {
        CollectiblesManager.OnCollectiblePickedUp -= HandleCrystalCollectionEconomy;
    }

    public void HandleCrystalCollectionEconomy(string collectibleMessage, string collectibleType, int collectibleValue)
    {

        int collectiblesCount = (int)Variables.Application.Get(collectibleType);

        Debug.Log("Currently owned " + collectibleType + ": " + collectiblesCount);

        collectiblesCount += collectibleValue;

        Debug.Log("Collectibles Count is now: " + collectiblesCount);

        Variables.Application.Set(collectibleType, collectiblesCount);

        OnQuestParamsChecked?.Invoke(DefineCollectibleMappings(collectibleType), collectiblesCount);
    }


    public int DefineCollectibleMappings(string collectibleType)
    {
        int result;

        switch (collectibleType)
        {
            case "Crystals":
                result = 1;
                break;
            case "Shards":

                result = 2;
                break;
            default:
                result = -1;
                break;
        }
        return result;
    }
}
