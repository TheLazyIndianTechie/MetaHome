using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AlertCanvasManager : MonoBehaviour
{
    private Canvas canvas;
    
    private static bool IsFirstTimeLoad;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        IsFirstTimeLoad = true;
    }

    private void OnEnable()
    {
        WowEventManager.StartListening(nameof(WowEvents.OnRPMAvatarLoaded), ShowCanvas);
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestMenu), ToggleCanvas);
    }

    private void OnDisable()
    {
        WowEventManager.StopListening(nameof(WowEvents.OnRPMAvatarLoaded), ShowCanvas);
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestMenu), ToggleCanvas);
    }

    private void ToggleCanvas(string canvasName)
    {
        if (canvasName == nameof(WowMenus.AlertCanvas))
        {
            ShowCanvas();
        }
        else
        {
            HideCanvas();
        }
    }

    private void ShowCanvas()
    {
        if(IsFirstTimeLoad)
        {
            if (!canvas.isActiveAndEnabled)
            {
                IsFirstTimeLoad = false;
                canvas.enabled = !canvas.isActiveAndEnabled;
            }
        } else
        {
            HideCanvas();

            TriggerMetaportRequest();
        }
    }

    private void HideCanvas()
    {
        if(canvas.isActiveAndEnabled)
        {
            IsFirstTimeLoad = false;

            canvas.enabled = !canvas.isActiveAndEnabled;
        }
    }

    private void TriggerMetaportRequest()
    {
        StartCoroutine(CallMetaport());
    }

    IEnumerator CallMetaport()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("New avatar loaded");

        // Trigger event to initiate metaport
        WowEventManager.TriggerEvent(nameof(WowEvents.OnPlayerRequestMetaport));
    }

    private void TriggerAvatarSelectionCanvas()
    {
        // Trigger event to open AvatarSelectionCanvas
        WowEventManager.TriggerEvent(nameof(WowEvents.OnPlayerRequestMenu), nameof(WowMenus.AvatarSelectionCanvas));
    }

    public void OnYesPressed()
    {
        HideCanvas();

        OnContinueWithSameAvatar();

        TriggerMetaportRequest();
    }

    public void OnCustomizePressed()
    {
        TriggerAvatarSelectionCanvas();
    }


    private void OnContinueWithSameAvatar()
    {
        UserData userData = (UserData)Variables.Saved.Get("UserDataJSON");

        string metaportDestination = (string)Variables.Application.Get("MetaPortDestination");

        Debug.Log("Log from Unity. User wants MetaPort to "+ metaportDestination +" continue with existing Avatar: " + userData.avatarUrl);

        ReactDataManager.Instance.CallSetActiveAvatar(gameObject.name, "", userData.avatarUrl, metaportDestination);
    }
}
