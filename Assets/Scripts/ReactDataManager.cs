using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

enum VisualScriptingVariables
{
    UserDataJSON,
    NFTCollectionData,
    UserOwnedNFTCollectionData,
    AvatarCollectionData,
}

public class ReactDataManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GetUserData(string gameObjectName, string functionName);

    [DllImport("__Internal")]
    private static extern void GetNFTCollectionData(string gameObjectName, string functionName);

    [DllImport("__Internal")]
    private static extern void GetUserOwnedNFTCollectionData(string gameObjectName, string functionName);

    [DllImport("__Internal")]
    private static extern void BuyNFT(string gameObjectName, string functionName1, string functionName2, int collectionID, int nftID, ulong nftCost);

    [DllImport("__Internal")]
    private static extern void SetCurrentNFT(string gameObjectName, string functionName1, int collectionID, int nftID);
    
    [DllImport("__Internal")]  
    private static extern void SellNFT(int collectionID, int nftID, int nftCost);

    [DllImport("__Internal")]
    private static extern void GetAllAvatars(string gameObjectName, string functionName);

    [DllImport("__Internal")]
    private static extern void SetActiveAvatar(string gameObjectName, string functionName, string currentAvatarUrl, String worldName);


    public static ReactDataManager Instance { get; private set; }

    // Delegates & Events
    public delegate void GetUserDataCallback();
    public static event GetUserDataCallback OnGetUserDataCallback;

    public delegate void GetNFTCollectionDataCallback();
    public static event GetNFTCollectionDataCallback OnGetNFTCollectionDataCallback;

    public delegate void GetUserOwnedNFTCollectionDataCallback();
    public static event GetUserOwnedNFTCollectionDataCallback OnGetUserOwnedNFTCollectionDataCallback;

    public delegate void SetCurrentNFTSuccessCallback();
    public static event SetCurrentNFTSuccessCallback OnSetCurrentNFTSuccessCallback;
    public static event SetCurrentNFTSuccessCallback OnSetCurrentNFTFailCallback;

    public delegate void GetAvatarCollectionDataCallback();
    public static event GetAvatarCollectionDataCallback OnGetAvatarCollectionDataCallback;

    // Lifecycle Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Dispatch events from Unity to React app
    public void CallGetUserData(string gameObjectName, string functionName)
    {
        GetUserData(gameObjectName, functionName);
    }

    public void CallGetNFTCollectionData(string gameObjectName, string functionName)
    {
        GetNFTCollectionData(gameObjectName, functionName);
    }

    public void CallBuyNFT(string gameObjectName, string functionName1, string functionName2, int collectionID, int nftID, ulong nftCost)
    {
        Debug.Log("React Data Manager calling: " + gameObjectName + " , " + functionName1 + " , " + functionName2 + " , " + collectionID + " , " + nftID + " , " + nftCost);
        BuyNFT(gameObjectName, functionName1, functionName2, collectionID, nftID, nftCost);
    }

    public void CallGetUserOwnedNFTCollectionData(string gameObjectName, string functionName)
    {
        GetUserOwnedNFTCollectionData(gameObjectName, functionName);
    }

    public void CallSetCurrentNFT(string gameObjectName, string functionName, int collectionID, int nftID)
    {
        // Locally modify the currentNFT selected for display in the NFT wall
        UserData userData = (UserData)Variables.Saved.Get(VisualScriptingVariables.UserDataJSON.ToString());
        userData.selectedLocalNft.Clear();
        userData.selectedLocalNft.Add(collectionID);
        userData.selectedLocalNft.Add(nftID);
        Variables.Saved.Set(VisualScriptingVariables.UserDataJSON.ToString(), userData);

        SetCurrentNFT(gameObjectName, functionName, collectionID, nftID);
    }

    public void CallSellNFT(int collectionID, int nftID, int nftCost)
    {
        SellNFT(collectionID, nftID, nftCost);
    }

    public void CallGetAvatarCollectionData(string gameObjectName, string functionName)
    {
        GetAllAvatars(gameObjectName, functionName);
    }

    public void CallSetActiveAvatar(string gameObjectName, string functionName, string newAvatarUrl, string worldName)
    {
        SetActiveAvatar(gameObjectName, functionName, newAvatarUrl, worldName);
    }

    // Handle React callbacks
    public void GetUserData(string dataJSON)
    {
        // Deserialize the JSON data
        UserData userData = JsonUtility.FromJson<UserData>(dataJSON);

        // Save the data into visual scripting object
        Variables.Saved.Set(VisualScriptingVariables.UserDataJSON.ToString(), userData);

        // Dispatch Unity delegate event
        OnGetUserDataCallback?.Invoke();
    }

    public void GetNFTCollectionData(string dataJSON)
    {
        // Deserialize the JSON data
        NFTCollectionData collection = JsonUtility.FromJson<NFTCollectionData>(dataJSON);

        Debug.Log("NFT Collection count - " + collection == null || collection.nfts == null ? 0 : collection.nfts.Count);

        // Save the data into visual scripting object
        if (collection != null)
        {
            Variables.Saved.Set(VisualScriptingVariables.NFTCollectionData.ToString(), collection);
        }

        // Dispatch Unity delegate event
        OnGetNFTCollectionDataCallback?.Invoke();
    }

    public void GetUserOwnedNFTCollectionData(string dataJSON)
    {
        // Deserialize the JSON data
        NFTCollectionData collection = JsonUtility.FromJson<NFTCollectionData>(dataJSON);

        // Save the data into visual scripting object
        if (collection != null)
        {
            Variables.Saved.Set(VisualScriptingVariables.UserOwnedNFTCollectionData.ToString(), collection);
        }

        // Dispatch Unity delegate event
        OnGetUserOwnedNFTCollectionDataCallback?.Invoke();
    }

    public void GetCurrentNFTModifyStatus(int status)
    {
        if (status == 1)
        {
            // Confirm the local selected NFT
            UserData userData = (UserData)Variables.Saved.Get(VisualScriptingVariables.UserDataJSON.ToString());
            userData.currentNft.Clear();
            userData.currentNft.AddRange(userData.selectedLocalNft);
            Variables.Saved.Set(VisualScriptingVariables.UserDataJSON.ToString(), userData);

            OnSetCurrentNFTSuccessCallback?.Invoke();
        }
        else
        {
            OnSetCurrentNFTFailCallback?.Invoke();
        }
    }

    public void GetAvatarCollectionData(string dataJSON)
    {
        // Deserialize the JSON data
        AvatarCollectionData collection = JsonUtility.FromJson<AvatarCollectionData>(dataJSON);

        // Save the data into visual scripting object
        if (collection != null)
        {
            Variables.Saved.Set(VisualScriptingVariables.AvatarCollectionData.ToString(), collection);
        }

        // Dispatch Unity delegate event
        OnGetAvatarCollectionDataCallback?.Invoke();
    }
}