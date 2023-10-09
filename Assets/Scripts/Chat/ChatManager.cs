using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ChatManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI chatText;


    [SerializeField] private TextMeshProUGUI chatInputText;

    [SerializeField] private TextMeshProUGUI unreadMessagesText;

    [SerializeField] private string[] nameColors;

    [SerializeField] private AudioSource notificationSound;


    private string myColor;

    [SerializeField] private GameObject chatUI;

    [SerializeField] private GameObject chatBtn;
   

    private int unreadMessages = 0;

    private GameObject localPlayer;

    private bool isChatMode = false;
    // Start is called before the first frame update
    void Start()
    {
        myColor = nameColors[(int)Random.Range(0, nameColors.Length)];
        FindLocalPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            chatUI.SetActive(true);
            chatBtn.SetActive(false);
            OnOpenCloseChatBox();
            isChatMode = true;
            TogglePlayerInput(isChatMode);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            chatUI.SetActive(false);
            chatBtn.SetActive(true);
            OnOpenCloseChatBox();
            isChatMode = false;
            TogglePlayerInput(isChatMode);
        }
    }

    public void SendChatMessage()
    {
         
         string message = $"<color={myColor}>" + (string)Variables.Saved.Get("PlayerName") + "</color>" + ": " + chatInputText.text + "\n";
         BroadcastChatMessage(message);
        // Debug.Log(message);
        
    }

    [Command(requiresAuthority = false)]
    private void BroadcastChatMessage(string message)
    {
        UpdateChatUI(message);
       // Debug.Log(message);
    }


    [ClientRpc]
    private void UpdateChatUI(string message)
    {
        notificationSound.Play();
        chatText.text += message;
        unreadMessages++;
        unreadMessagesText.text = unreadMessages.ToString();

        if (unreadMessages > 10)
        {
            unreadMessagesText.text = "10+";
        }
       // Debug.Log(message);
    }

    public void OnOpenCloseChatBox()
    {
        isChatMode = !isChatMode;
        unreadMessages = 0;
        unreadMessagesText.text = unreadMessages.ToString();
        TogglePlayerInput(isChatMode);
    }

    private void FindLocalPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (localPlayer != null)
        {
            localPlayer = allPlayers[0];
        }
        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i].GetComponent<NetworkBehaviour>().isLocalPlayer)
            {
                localPlayer = allPlayers[i];
                Debug.Log("this is local player!");
            }
        }
    }

    private void TogglePlayerInput(bool ischatmode)
    {
        localPlayer.GetComponent<PlayerInput>().enabled = !isChatMode;
    }

    public void OpenedChatUI()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
                // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
                WebGLInput.captureAllKeyboardInput = false;
#endif
        TogglePlayerInput(true);
        Debug.Log("opened chat UI");
    }

    public void ClosedChatUI()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
                // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
                WebGLInput.captureAllKeyboardInput = true;
#endif
        TogglePlayerInput(false);
    }
}
