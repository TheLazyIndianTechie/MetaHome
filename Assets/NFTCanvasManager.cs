using System;
using TMPro;
using UnityEngine;
public class NFTCanvasManager : MonoBehaviour
{
    private Canvas _menuCanvas;
    private TextMeshProUGUI _keyCode;

    private void Awake()
    {
        _menuCanvas = GetComponent<Canvas>();
        _keyCode = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() => MenuTrigger.OnMenuTriggered += ActivateCanvas;
    private void OnDisable() => MenuTrigger.OnMenuTriggered -= ActivateCanvas;
    private void ActivateCanvas(KeyCode keycode, bool menuActivationState)
    {
        _menuCanvas.enabled = menuActivationState;
        
    }
}
