using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
   public void ChooseGender(string gender)
   {
        GameManager.avatarUrlOfPlayer = gender;
        SceneManager.LoadScene("SampleScene");
   }
}
