using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InteriorSceneGameManager : MonoBehaviour
{
    [SerializeField] private GameObject nftWallImage, emptyWallImage;

    [SerializeField] private Canvas loadingPanel;

    // Testing string for Editor purposes
    private string json = "{\"nfts\":[{\"collectionId\":0,\"nftId\":0,\"owner\":{\"AccountId\":\"5D7sGJu7iCXMNbTdHBDt3irFydbPaaSM5HUPqjiV1RtPSNfx\"},\"metadata\":\"https://ipfs.io/ipfs/QmQK5R4j7rM8bcNsJnJE6VQWcNTSQmV6kR1EcwTxodGDHe\",\"cost\":320000,\"nftName\":\"BoredApeYachtClub #3329\",\"description\":\"BoredApeYachtClub #3329\"},{\"collectionId\":0,\"nftId\":1,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmRZA3Bf1nJAYEGJYGsxZEWPfjpX2hpeS4fmdGaK5SdB5b\",\"cost\":2100000,\"nftName\":\"BoredApeYachtClub #3634\",\"description\":\"BoredApeYachtClub #3634\"},{\"collectionId\":0,\"nftId\":7,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmbRmz6c2a4FUF7qr8jJ1QBvjwnTFNYkM6mSyGTf1PHx3R\",\"cost\":6500000,\"nftName\":\"BoredApeYachtClub #4651\",\"description\":\"BoredApeYachtClub #4651\"},{\"collectionId\":0,\"nftId\":8,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmQYLq7cCtM9xy37eAiwJhJG25CfxDTunkuxV5Zf7cJzHR\",\"cost\":430000,\"nftName\":\"BoredApeYachtClub #527\",\"description\":\"BoredApeYachtClub #527\"},{\"collectionId\":0,\"nftId\":9,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmZwfgoWzTqo7c13A63XjcWcw3tJUEksfHiUbikgeUDvEt\",\"cost\":540000,\"nftName\":\"BoredApeYachtClub #8914\",\"description\":\"BoredApeYachtClub #8914\"},{\"collectionId\":0,\"nftId\":10,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmVersEsEXuANWjgWKL2UVex9AJp1Yzh6CsLYuiQuLbWjp\",\"cost\":650000,\"nftName\":\"BoredApeYachtClub #2193\",\"description\":\"BoredApeYachtClub #2193\"},{\"collectionId\":0,\"nftId\":11,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmXpW2aZtGgYWxVJF3XtvKsYMFPzffZBrVckhkBsPudFTp\",\"cost\":760000,\"nftName\":\"BoredApeYachtClub #9712\",\"description\":\"BoredApeYachtClub #9712\"}]}";

    // Start is called before the first frame update
    void Start()
    {
        FetchUserOwnedNFT();
    }

    private void OnEnable()
    {
        WowEventManager.StartListening(nameof(WowEvents.OnDisplayInfo), ShowInteriorSceneLoadingCanvas);
        WowEventManager.StartListening(nameof(WowEvents.OnRPMAvatarLoaded), HideInteriorSceneLoadingCanvas);

        ReactDataManager.OnGetUserOwnedNFTCollectionDataCallback += LoadStoredNFTWallImage;
        ReactDataManager.OnSetCurrentNFTSuccessCallback += FetchUserOwnedNFT;
    }

    private void OnDisable()
    {
        WowEventManager.StopListening(nameof(WowEvents.OnDisplayInfo), ShowInteriorSceneLoadingCanvas);
        WowEventManager.StopListening(nameof(WowEvents.OnRPMAvatarLoaded), HideInteriorSceneLoadingCanvas);

        ReactDataManager.OnGetUserOwnedNFTCollectionDataCallback -= LoadStoredNFTWallImage;
        ReactDataManager.OnSetCurrentNFTSuccessCallback -= FetchUserOwnedNFT;
    }

    private void LoadStoredNFTWallImage()
    {
        // Fetch the UserData which contains the details about current NFT displayed in the NFT wall
        UserData userData = Variables.Saved.Get(VisualScriptingVariables.UserDataJSON.ToString()) as UserData;

        // Fetch the User owned NFT List
        List<NFTData> nfts = new List<NFTData>();
        NFTCollectionData nftCollection = (NFTCollectionData)Variables.Saved.Get(VisualScriptingVariables.UserOwnedNFTCollectionData.ToString());
        nfts.AddRange(nftCollection.nfts);

        // Find the selected nft from the user owned nft list
        if (userData.currentNft.Count == 2 && nfts.Count > 0)
        {
            NFTData nftData = nfts.Find(nft => nft.collectionId == userData.currentNft[0] && nft.nftId == userData.currentNft[1]);

            if (nftData != null)
            {
                // Download and set the nft image on the wall
                StartCoroutine(DownloadSetNFTWall(nftData.metadata));
            }
            else
            {
                // Show a default image for the nft wall
                Image nftImage = nftWallImage.GetComponent<Image>();

                // Create a new 256x256 texture ARGB32 (32 bit with alpha) and no mipmaps
                var texture = new Texture2D(256, 256, TextureFormat.ARGB32, false);

                // set the pixel values
                texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
                texture.SetPixel(1, 0, Color.clear);
                texture.SetPixel(0, 1, Color.white);
                texture.SetPixel(1, 1, Color.black);

                // Apply all SetPixel calls
                texture.Apply();

                // Create a new sprite using the above texture and apply to the image
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                nftImage.overrideSprite = sprite;
            }
        }
    }

    IEnumerator DownloadSetNFTWall(string imageURL)
    {
        Image nftImage = nftWallImage.GetComponent<Image>();

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.result);
        } 
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            nftImage.overrideSprite = sprite;
        }
    }

    // Fetch user owned NFTs by interacting with the React App
    private void FetchUserOwnedNFT()
    {
#if !(UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX || UNITY_EDITOR_WIN)
        ReactDataManager.Instance.CallGetUserOwnedNFTCollectionData(gameObject.name, nameof(UpdateUserOwnedNFT));
#else
        UpdateUserOwnedNFT(json);
#endif
    }

    public void UpdateUserOwnedNFT(string userNftJSON)
    {
        ReactDataManager.Instance.GetUserOwnedNFTCollectionData(userNftJSON);
    }

    public void ExitInteriorHomeScene()
    {
        SceneManager.LoadScene((int)SceneIndex.INITIALIZATION_SCENE);
    }

    private void ShowInteriorSceneLoadingCanvas(string avatarLoadingStatus)
    {
        loadingPanel.enabled = true;
        
        loadingPanel?.GetComponentInChildren<TextMeshProUGUI>().SetText(avatarLoadingStatus);
    }

    private void HideInteriorSceneLoadingCanvas()
    {
        loadingPanel.enabled = false;
        Debug.Log("Hidden loading canvas");
    }
}