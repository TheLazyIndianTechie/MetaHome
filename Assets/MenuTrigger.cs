using System;
using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField] private KeyCode activationKeycode;
    private bool _menuState, _isUserInTrigger; //This passes the on/off state to listeners

    //Emit an event, letting systems know that player wants to open Menu!
    public static event Action<KeyCode, bool> OnMenuTriggered;


    private void Update()
    {
        if (!_isUserInTrigger) return;
        if (!Input.GetKeyDown(activationKeycode)) return;
        _menuState = !_menuState;
        OnMenuTriggered?.Invoke(activationKeycode, _menuState);
        Debug.Log(("Triggered an event with Menu state as " + _menuState));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isUserInTrigger = true;
        Debug.Log("Is user in trigger: "+ _isUserInTrigger);


    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isUserInTrigger = false;
        Debug.Log("Is user in trigger: "+ _isUserInTrigger);
        
        OnMenuTriggered?.Invoke(activationKeycode, false);
    }
}
