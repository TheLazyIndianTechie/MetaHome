using System;
using UnityEngine;

//***---------PANEL MANAGER SCRIPT--------*****
//***** by Vinay Vidyasagar *******//
//Purpose: Whenever a canvas is activated in the scene, this script will release the mouselock. Attach this script to the main canvas

public class NFTMarketplacePanelManager : MonoBehaviour
{
    
    public static event Action<int> OnNFTMarketplaceOpened, OnNFTMarketplaceClosed; 
    

    private void OnEnable()
    {
        Debug.Log("Canvas " + this.name + " is enabled");
        SetCursorState(false);
        OnNFTMarketplaceOpened?.Invoke(101);
    }

    private void OnDisable()
    {
        Debug.Log("Canvas " + this.name + " is disabled");
        SetCursorState(true);
        OnNFTMarketplaceClosed?.Invoke(101);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}