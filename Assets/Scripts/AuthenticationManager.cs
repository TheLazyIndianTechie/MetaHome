using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AuthenticationManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void gameLoaded();

    // Start is called before the first frame update
    void Start()
    {
        gameLoaded();
        //if the platform is webgl then the game canvas will be focused only when user clicks on it.
        //by default, unity processes all the keyboard events before javascript DOM...
       
        #if !UNITY_EDITOR && UNITY_WEBGL
                // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
                WebGLInput.captureAllKeyboardInput = false;
        #endif
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function will be called in the javascript (index.html) file of webgl build. 
    public void SignIn()
    {
        //transition to main menu after successfull sign in.
        SceneManager.LoadScene((int)SceneIndex.MAIN_MENU_SCENE);
       
    }

    public void SignUp()
    {
        //do something after successfull signup
       
    }
}
