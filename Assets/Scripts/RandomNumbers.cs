using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RandomNumbers : MonoBehaviour
{


public TMP_Text _randomNumberText;


public void RandomNumberGenerator()
    {
        var x = Random.Range(1,10000);
        _randomNumberText.SetText(x.ToString());

    }
}
