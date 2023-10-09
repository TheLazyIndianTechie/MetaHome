using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NFTMarketPlaceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas, menuCanvas;

    // Testing string for Editor purposes
    private string json = "{\"nfts\":[{\"collectionId\":0,\"nftId\":0,\"owner\":{\"AccountId\":\"5D7sGJu7iCXMNbTdHBDt3irFydbPaaSM5HUPqjiV1RtPSNfx\"},\"metadata\":\"https://ipfs.io/ipfs/QmQK5R4j7rM8bcNsJnJE6VQWcNTSQmV6kR1EcwTxodGDHe\",\"cost\":320000,\"nftName\":\"BoredApeYachtClub #3329\",\"description\":\"BoredApeYachtClub #3329\"},{\"collectionId\":0,\"nftId\":1,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmRZA3Bf1nJAYEGJYGsxZEWPfjpX2hpeS4fmdGaK5SdB5b\",\"cost\":2100000,\"nftName\":\"BoredApeYachtClub #3634\",\"description\":\"BoredApeYachtClub #3634\"},{\"collectionId\":0,\"nftId\":7,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmbRmz6c2a4FUF7qr8jJ1QBvjwnTFNYkM6mSyGTf1PHx3R\",\"cost\":6500000,\"nftName\":\"BoredApeYachtClub #4651\",\"description\":\"BoredApeYachtClub #4651\"},{\"collectionId\":0,\"nftId\":8,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmQYLq7cCtM9xy37eAiwJhJG25CfxDTunkuxV5Zf7cJzHR\",\"cost\":430000,\"nftName\":\"BoredApeYachtClub #527\",\"description\":\"BoredApeYachtClub #527\"},{\"collectionId\":0,\"nftId\":9,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmZwfgoWzTqo7c13A63XjcWcw3tJUEksfHiUbikgeUDvEt\",\"cost\":540000,\"nftName\":\"BoredApeYachtClub #8914\",\"description\":\"BoredApeYachtClub #8914\"},{\"collectionId\":0,\"nftId\":10,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmVersEsEXuANWjgWKL2UVex9AJp1Yzh6CsLYuiQuLbWjp\",\"cost\":650000,\"nftName\":\"BoredApeYachtClub #2193\",\"description\":\"BoredApeYachtClub #2193\"},{\"collectionId\":0,\"nftId\":11,\"owner\":{\"AccountId\":\"5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY\"},\"metadata\":\"https://ipfs.io/ipfs/QmXpW2aZtGgYWxVJF3XtvKsYMFPzffZBrVckhkBsPudFTp\",\"cost\":760000,\"nftName\":\"BoredApeYachtClub #9712\",\"description\":\"BoredApeYachtClub #9712\"}]}";


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCanvas?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCanvas?.SetActive(false);
            menuCanvas?.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (dialogueCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Y))
            {
                //Deactivate Dialogue box and Open the requisite canvas
                dialogueCanvas?.SetActive(false);
                menuCanvas?.SetActive(true);

                HandleReactFetchNFT();
            }

            if (Input.GetKey(KeyCode.N))
            {
                //Deactivate Dialogue box
                dialogueCanvas?.SetActive(false);
            }
        }

        else if (!dialogueCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.F))
            {
                dialogueCanvas?.SetActive(true);
            }
        }

        if (menuCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.X))
            {
                menuCanvas?.SetActive(false);
            }
        }
    }

    private void HandleReactFetchNFT()
    {
#if !(UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX)
        ReactDataManager.Instance.CallGetNFTCollectionData(gameObject.name, nameof(RetrieveNFTDetails));
#else
        RetrieveNFTDetails(json);
#endif
    }

    public void RetrieveNFTDetails(string nftsInJSON)
    {
        Debug.Log("NFTS received from React: " + nftsInJSON);
        ReactDataManager.Instance.GetNFTCollectionData(nftsInJSON);
    }
}
