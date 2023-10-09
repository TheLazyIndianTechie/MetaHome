using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject avataLoading, avatarSelectionPanel;

    private Canvas canvas;

    private string json = "{\"avatarArray\":[\"https://api.readyplayer.me/v1/avatars/638f616e75f8551b54c92356.glb\", \"https://api.readyplayer.me/v1/avatars/637509fb152ef07e24248c44.glb\", \"https://api.readyplayer.me/v1/avatars/637615ac5764c3e56af9d52e.glb\", \"https://api.readyplayer.me/v1/avatars/63ce26691718944c37d3c475.glb\"]}";

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestMenu), ToggleCanvas);
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestMenu), FetchAvatarData);
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestChangeAvatar), OnNewAvatarURLLoaded);
        ReactDataManager.OnGetAvatarCollectionDataCallback += OnAvatarDataFetched;
    }

    private void OnDisable()
    {
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestMenu), ToggleCanvas);
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestMenu), FetchAvatarData);
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestChangeAvatar), OnNewAvatarURLLoaded);
        ReactDataManager.OnGetAvatarCollectionDataCallback -= OnAvatarDataFetched;
    }

    private void OnNewAvatarURLLoaded(string newAvatarURL)
    {
        HideCanvas();
    }

    private void FetchAvatarData(string canvasName)
    {
        if (canvasName == nameof(WowMenus.AvatarSelectionCanvas))
        {
            HandleFetchAvatar();
        }
    }

    private void HandleFetchAvatar()
    {
        
#if !(UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX)
        ReactDataManager.Instance.CallGetAvatarCollectionData(gameObject.name, nameof(RetrieveAllAvatars));
#else
        RetrieveAllAvatars(json);
#endif
    }

    public void RetrieveAllAvatars(string dataJSON)
    {
        Debug.Log("Unity Log from + " + this.name + "Received Avatar URLs: " + dataJSON);

        ReactDataManager.Instance.GetAvatarCollectionData(dataJSON);
    }

    private void ToggleCanvas(string canvasName)
    {
        if (canvasName == nameof(WowMenus.AvatarSelectionCanvas))
        {
            ShowCanvas();
        } else
        {
            HideCanvas();
        }
    }

    private void ShowCanvas()
    {
        if (!canvas.isActiveAndEnabled)
        {
            canvas.enabled = !canvas.isActiveAndEnabled;
        }
    }

    private void HideCanvas()
    {
        if(canvas.isActiveAndEnabled)
        {
            canvas.enabled = !canvas.isActiveAndEnabled;
        }
    }

    private void OnAvatarDataFetched()
    {
        avataLoading.SetActive(false);
        avatarSelectionPanel.SetActive(true);
    }
}