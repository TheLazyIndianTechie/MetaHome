using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MusicPanel : MonoBehaviour
{
    
	[Serializable]
	public struct Places
	{
		public string Title;
		public string Artist;
		public Sprite MusicIcon;
		public string Playlist;
		public string Cost;
	}

	[SerializeField] List<Places> AllMusic;
	[SerializeField] GameObject MusicDetails;
	[HideInInspector] public int count;
	void Start()
	{
		GameObject PlacesButtonTemplate = transform.GetChild(0).gameObject;
		GameObject g;

		int N = AllMusic.Count;

		for (int i = 0; i < N; i++)
		{
			g = Instantiate(PlacesButtonTemplate, transform);
			g.transform.GetChild(0).GetComponent<Image>().sprite = AllMusic[i].MusicIcon;
			g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Title;
			g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Artist;
			g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Playlist;


			g.transform.GetChild(4).GetComponent<Button>().AddEventListener(i, ItemClicked);
			
		}

		Destroy(PlacesButtonTemplate);


	}


	void ItemClicked(int itemIndex)
	{
		MusicDetails.SetActive(true);

		MusicDetails.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllMusic[itemIndex].Artist;
		MusicDetails.transform.GetChild(4).GetComponent<Image>().sprite = AllMusic[itemIndex].MusicIcon;
		MusicDetails.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = AllMusic[itemIndex].Title;
		MusicDetails.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = AllMusic[itemIndex].Cost;


	}

	/*void BuyBtn(int itemIndex)
	{
		Debug.Log(itemIndex);
		Debug.Log("name " + AllMusic[itemIndex].Title);

	}

	void PlayBtn(int itemIndex)
	{
		Debug.Log(itemIndex);
		Debug.Log("Play Button Clicked");

	}
	*/

}
