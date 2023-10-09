using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEventsRPM : MonoBehaviour
{
    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            transform.GetComponentInParent<StarterAssets.PlayerMovement>().PlayFootStep();
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            transform.GetComponentInParent<StarterAssets.PlayerMovement>().PlayLandSound();
        }
    }
}

