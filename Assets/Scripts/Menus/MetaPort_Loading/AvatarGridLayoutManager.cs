using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AvatarGridLayoutManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private GameObject itemAvatar;

    [SerializeField]
    private int contentItemWidth = 300;

    // For Testing in Editor mode only
    private string json = "{\"avatarArray\":[\"https://api.readyplayer.me/v1/avatars/638f616e75f8551b54c92356.glb\", \"https://api.readyplayer.me/v1/avatars/637509fb152ef07e24248c44.glb\", \"https://api.readyplayer.me/v1/avatars/637615ac5764c3e56af9d52e.glb\", \"https://api.readyplayer.me/v1/avatars/63ce26691718944c37d3c475.glb\"]}";

    private List<string> avatarURLs = new();
    private List<GameObject> gridItems = new();
    private int selectedItemIndex;

    private void Start()
    {
        PopulateAvatarCollection();
        UpdateUI();
    }

    private void PopulateAvatarCollection()
    {
        avatarURLs.Clear();

        // Fetch from Visual Scripting variables
        AvatarCollectionData avatarCollection = (AvatarCollectionData)Variables.Saved.Get(nameof(VisualScriptingVariables.AvatarCollectionData));
        UserData userData = (UserData)Variables.Saved.Get(nameof(VisualScriptingVariables.UserDataJSON));

        // Remove the current UserAvatarURL from the list of avatars to display
        var items = avatarCollection.avatarArray;
        
        if(items != null && items.Count > 0)
        {
            items.Remove(userData.avatarUrl);
            avatarURLs.AddRange(items);
        }
    }

    private void UpdateUI()
    {
        if (gridItems.Count > 0)
        {
            for (int i = 0; i < gridItems.Count; i++)
            {
                Destroy(gridItems[i]);
            }

            gridItems.Clear();
        }

        itemAvatar.SetActive(true);

        GameObject gameObject;

        int count = avatarURLs.Count;

        for (int i = 0; i < count; i++)
        {
            // Create a new runtime instance of the grid item
            gameObject = Instantiate(itemAvatar, transform);
            
            // Load the image into the grid item
            StartCoroutine(SetImage(gameObject, GetImageURL(avatarURLs[i])));

            // Attach a OnClick listener to the grid item
            gameObject.GetComponent<Button>().AddEventListener(i, OnGridItemClicked);

            // Add it to the gamobject list
            gridItems.Add(gameObject);
        }

        itemAvatar.SetActive(false);
    }

    private string GetImageURL(string avatarURL)
    {
        return avatarURL.Replace(".glb", ".png");
    }

    IEnumerator SetImage(GameObject gameObject, string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.result);
        }
        else
        {
            Texture2D avatarTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if(avatarTexture.width > 400 || avatarTexture.height > 400)
            {
                GPUTextureScaler.Scale(avatarTexture, contentItemWidth, contentItemWidth);
            }
            Sprite sprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2(avatarTexture.width / 2, avatarTexture.height / 2));
            gameObject.GetComponent<Image>().overrideSprite = sprite;
            gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
    private void OnGridItemClicked(int gridItemIndex)
    {
        selectedItemIndex = gridItemIndex;

#if !(UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX)
        // Trigger react event to set new avatar
        ReactDataManager.Instance.CallSetActiveAvatar(gameObject.name, nameof(ReceiveAvatarSetAcknowledgement), avatarURLs[gridItemIndex], "MetaGallery");
#else
        ReceiveAvatarSetAcknowledgement(1);
#endif

        Debug.Log(avatarURLs[gridItemIndex] + " at index = " + gridItemIndex + " has been set as Active");
    }

   
    public void ReceiveAvatarSetAcknowledgement(int status)
    {
        if(status == 0)
        {
            Debug.LogError("SetAvatarFailed with exit code 0");
        }

        Debug.Log("Requesting to load the new avatar URL - " + avatarURLs[selectedItemIndex]);

        OnReceiveUpdatedUserData(avatarURLs[selectedItemIndex]); 
    }

    public void OnReceiveUpdatedUserData(string newAvatarUrl)
    {
        WowEventManager.TriggerEvent(nameof(WowEvents.OnPlayerRequestChangeAvatar), newAvatarUrl);
    }
}