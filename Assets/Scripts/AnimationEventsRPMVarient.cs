using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimationEventsRPMVarient : MonoBehaviour
{
    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            transform.GetComponentInParent<StarterAssets.PlayerMovementSinglePlayer>().PlayFootStep();
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            transform.GetComponentInParent<StarterAssets.PlayerMovementSinglePlayer>().PlayLandSound();
        }
    }
}

