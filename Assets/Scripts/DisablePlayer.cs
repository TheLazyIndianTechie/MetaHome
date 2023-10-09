using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayer : MonoBehaviour
{
    public GameObject player;
    public StationTriggers stationTriggers;
    void OnEnable()
    {
         if(player!=null)
	 {
		player.SetActive(false);
	 }
	 if(stationTriggers == null)
	 {
		stationTriggers = FindObjectOfType<StationTriggers>();
	 }
    }

    void OnDisable()
    {
         if(player!=null)
	 {
		player.SetActive(true);
		stationTriggers.InvertCheckStatus();
	 }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
