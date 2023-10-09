using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] private Canvas loadingPanel;

    private void Awake()
    {
        Debug.Log("Lobby Scene Initialized...");
    }

    private void OnEnable()
    {
        WowEventManager.StartListening(nameof(WowEvents.OnDisplayInfo), ShowLobbyLoadingCanvas);
        WowEventManager.StartListening(nameof(WowEvents.OnRPMAvatarLoaded), HideLobbyLoadingCanvas);
    }

    private void OnDisable()
    {
        WowEventManager.StopListening(nameof(WowEvents.OnDisplayInfo), ShowLobbyLoadingCanvas);
        WowEventManager.StopListening(nameof(WowEvents.OnRPMAvatarLoaded), HideLobbyLoadingCanvas);
    }

    private void ShowLobbyLoadingCanvas(string avatarLoadingStatus)
    {
        loadingPanel.enabled = true;
        loadingPanel?.GetComponentInChildren<TextMeshProUGUI>().SetText(avatarLoadingStatus);
    }

    private void HideLobbyLoadingCanvas()
    {
        loadingPanel.enabled = false;
        Debug.Log("Hidden loading canvas");
    }

}
