using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameBrowser : MonoBehaviour
{
	[Serializable]
	public struct Games
	{
		public string Name;
		public string Cost;
		public string Description;
		public Sprite GamesIcon;
		
	}

	[SerializeField] List<Games> AllGames;
	[SerializeField] GameObject GameDetail;
	[HideInInspector] public int count;
	void Start()
	{
		GameObject PlacesButtonTemplate = transform.GetChild(0).gameObject;
		GameObject g;

		int N = AllGames.Count;

		for (int i = 0; i < N; i++)
		{
			g = Instantiate(PlacesButtonTemplate, transform);
			g.transform.GetChild(0).GetComponent<Image>().sprite = AllGames[i].GamesIcon;
			g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AllGames[i].Name;
			g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = AllGames[i].Cost;

			g.transform.GetChild(3).GetComponent<Button>().AddEventListener(i, ItemClicked);
		}

		Destroy(PlacesButtonTemplate);


	}

	void ItemClicked(int itemIndex)
	{
		GameDetail.SetActive(true);

		GameDetail.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllGames[itemIndex].Description;
		GameDetail.transform.GetChild(4).GetComponent<Image>().sprite = AllGames[itemIndex].GamesIcon;
		GameDetail.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = AllGames[itemIndex].Name;
		GameDetail.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = AllGames[itemIndex].Cost.ToString();


	}
}
