using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;



public class NFTMarketPlace : MonoBehaviour
{
	[Serializable]
	public struct NFT
	{
		public string Name;
		public string Description;
		public string Scarcity;
		public Sprite NFTIcon;
		public int Cost;
	}

	[SerializeField] List<NFT> allnfts;
	[SerializeField] GameObject NFTDetails;

	[HideInInspector] public int count;
	void Start()
	{
		GameObject NFTtemplate = transform.GetChild(0).gameObject;
		GameObject g;

		int N = allnfts.Count;
 
		for (int i = 0; i < N; i++)
		{
			g = Instantiate(NFTtemplate, transform);
			g.transform.GetChild(0).GetComponent<Image>().sprite = allnfts[i].NFTIcon;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allnfts[i].Name;
			g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allnfts[i].Scarcity;
			g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = allnfts[i].Cost.ToString();

			g.transform.GetChild(4).GetComponent<Button>().AddEventListener(i, ItemClicked);

			

		}

		Destroy(NFTtemplate);

		
		
	}

	void ItemClicked(int itemIndex)
	{
		Debug.Log(itemIndex + "Card");

		NFTDetails.SetActive(true);

		NFTDetails.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = allnfts[itemIndex].Description;
		NFTDetails.transform.GetChild(4).GetComponent<Image>().sprite = allnfts[itemIndex].NFTIcon;
		NFTDetails.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = allnfts[itemIndex].Name;
		NFTDetails.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = allnfts[itemIndex].Cost.ToString();

		NFTDetails.transform.GetChild(8).GetComponent<Button>().AddEventListener(itemIndex,BuyBtn);



	}
	public void BuyBtn(int index)
    {
		// for buy button implementation
		Debug.Log(index + "Buy");

    }
	
}
