//using ReadyPlayerMe;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;
//using TMPro;
//using Unity.VisualScripting;

//public class GestureManager : NetworkBehaviour
//{
//    private string currentTargetPlayerName;

//    private TextMeshProUGUI calllingText;

//    [SerializeField] private float range = 20f;

//    [SerializeField] private Transform shootPoint;
//    // Start is called before the first frame update
//    void Start()
//    {
//        calllingText = GameObject.FindGameObjectWithTag("CallTxt").GetComponent<TextMeshProUGUI>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            ShootRayCast();
//        }
//    }

//    [Command(requiresAuthority = false)]
//    private void BroadcastCallMessage(string message)
//    {
//        UpdateCallUI(message);
//        Debug.Log(message);
//    }

//    [ClientRpc]
//    private void UpdateCallUI(string message)
//    {
//        // notificationSound.Play();

//        calllingText.text = message;

//        Debug.Log(message);

//        Invoke(nameof(ResetCallText), 5);
//    }


//    public void ResetCallText()
//    {
//        calllingText.text = "";
//    }

//    public void ShootRayCast()
//    {
//        RaycastHit hit;
//        Ray newRay = new Ray(shootPoint.position, shootPoint.forward);
//        if (Physics.Raycast(newRay, out hit, range))
//        {
//            if (hit.collider.CompareTag("Player"))
//            {
//                currentTargetPlayerName = hit.collider.GetComponent<RuntimeLoader>().playerNameText.text;
//                string message = "<color=red>" + (string)Variables.Saved.Get("PlayerName") + "</color>" + " IS WAVING AT " + "<color=green>" + currentTargetPlayerName + "</color>";
//                BroadcastCallMessage(message);
//            }
//        }
//    }
//}
