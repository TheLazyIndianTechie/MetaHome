using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimationEventsRPM : NetworkBehaviour
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

