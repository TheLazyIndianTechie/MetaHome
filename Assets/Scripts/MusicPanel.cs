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
		public string Album;
		public string Duration;
		public string Date;
		public string Playlist;
		public string Cost;
		public AudioClip song;
	}

	[SerializeField] List<Places> AllMusic;
	[SerializeField] GameObject MusicDetails;
	[HideInInspector] public int count;
	[SerializeField] private Transform grid;
	[SerializeField] private float factor;
	[SerializeField] private Slider songSlider;
	public AudioSource source;

	private int songIndex;
	private bool isPaused = false;
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
			g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Album;
			g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Duration;
			g.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = AllMusic[i].Date;

			g.transform.GetChild(6).GetComponent<Button>().AddEventListener(i, ItemClicked);
			
		}

		//Translate the grid down
		if(N>4)
		{
			grid.localPosition = new Vector3(grid.localPosition.x,grid.localPosition.y + (N*factor), grid.localPosition.z);
		}

		Destroy(PlacesButtonTemplate);


	}


	void ItemClicked(int itemIndex)
	{
		MusicDetails.SetActive(true);
        MusicDetails.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "NOW PLAYING: " + AllMusic[itemIndex].Title;
		MusicDetails.transform.GetChild(3).GetComponent<Image>().sprite = AllMusic[itemIndex].MusicIcon;
        MusicDetails.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = AllMusic[itemIndex].Title;
		MusicDetails.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "by " + AllMusic[itemIndex].Artist;

		songIndex = itemIndex;

		if (AllMusic[itemIndex].song != null)
		{
			source.clip = AllMusic[itemIndex].song;
			source.time = 0f;
			source.Play();
			SongSlider();
		}
		//MusicDetails.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "NOW PLAYING: " + AllMusic[itemIndex].Title;
 
		//MusicDetails.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = AllMusic[itemIndex].Cost;


	}

	void SongSlider()
	{
		int songLengthMins = (int)((AllMusic[songIndex].song.length) / 60);
		int songLengthSeconds = (int)((AllMusic[songIndex].song.length) % 60);
		
		int currentTimeMins = (int)(source.time / 60);
		int currentTimeSeconds = (int)(source.time % 60);
		MusicDetails.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = (currentTimeMins.ToString() + ":" + currentTimeSeconds.ToString() + "/" +
																					songLengthMins.ToString() + ":" + songLengthSeconds.ToString());

		songSlider.value = source.time / AllMusic[songIndex].song.length;
		Invoke("SongSlider", 1f);
	}

	public void SongTimer()
	{
			source.time = songSlider.value * AllMusic[songIndex].song.length;
	}

	public void Pause()
	{
		isPaused = !isPaused;
		if (isPaused)
			source.Pause();
		else
			source.UnPause();
	}

	public void ChangeSong(int index)
	{
		int newIndex = songIndex + index;
		if(newIndex < 0 || newIndex > AllMusic.Count-1)
			return;
		songIndex = newIndex;
		ItemClicked(newIndex);
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
