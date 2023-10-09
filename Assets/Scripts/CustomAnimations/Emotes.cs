using UnityEngine;
using UnityEngine.InputSystem;



public class Emotes : MonoBehaviour
	{

    [Header("Emote Input Values")]
	
	public string Emote01;
	public string Emote02;
	public string Emote03;

    [SerializeField]
	private Animator _anim;

    private void Update()
    {
        
        //This is purely a hack code for testing purposes.
        //Use action maps for production - @VinayVidyasagar

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            PlayEmoteAnimation(Emote01);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            PlayEmoteAnimation(Emote02);
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            PlayEmoteAnimation(Emote03);
        }
    }


    public void PlayEmoteAnimation(string triggerName)
    {
        _anim.SetTrigger(triggerName);
        Debug.Log("Emote key was pressed. Playing animation "+ triggerName);
    }


}

