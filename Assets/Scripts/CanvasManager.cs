using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***---------CANVAS MANAGER SCRIPT--------*****
//***** by Vinay Vidyasagar *******//
//Purpose: Whenever a canvas is activated in the scene, this script will release the mouselock. Attach this script to the main canvas

public class CanvasManager : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Canvas " + this.name + " is enabled");
        SetCursorState(false);
    }

    private void OnDisable()
    {
        Debug.Log("Canvas " + this.name + " is disabled");
        SetCursorState(true);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
