using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public static class ButtonExtension
{
	public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener(delegate () {
			OnClick(param);
		});
	}
}

public class PropertiesPanel : MonoBehaviour
{
	[Serializable]
	public struct Places
	{
		public string Title;
		public string MembersOnline;
		public Sprite PlacesIcon;
		public string Location;
	}

	[SerializeField] List<Places> AllPlaces;

	[HideInInspector] public int count;
	void Start()
	{
		GameObject PlacesButtonTemplate = transform.GetChild(0).gameObject;
		GameObject g;

		int N = AllPlaces.Count;

		for (int i = 0; i < N; i++)
		{
			g = Instantiate(PlacesButtonTemplate, transform);
			g.transform.GetChild(0).GetComponent<Image>().sprite = AllPlaces[i].PlacesIcon;
			g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AllPlaces[i].Title;
			g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = AllPlaces[i].MembersOnline;
			g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllPlaces[i].Location;


			g.GetComponent<Button>().AddEventListener(i, ItemClicked);
		}

		Destroy(PlacesButtonTemplate);


	}

	void ItemClicked(int itemIndex)
	{
		Debug.Log( itemIndex);
		Debug.Log("name " + AllPlaces[itemIndex].Title);
		
	}
	
}
