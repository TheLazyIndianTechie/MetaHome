using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //This script has a collection of menus that can be closed on particular user actions.
    //Prevents manual linking of multiple onclick handlers in editor
    //By Vinay Vidyasagar

    public GameObject[] menusToBeClosed;


    public void CloseAllOpenMenus()
    {
        foreach (var menu in menusToBeClosed)
        {
            menu.SetActive(false);
        }
    }
}
