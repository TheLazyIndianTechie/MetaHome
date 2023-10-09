using ReadyPlayerMe;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UpdateAvatarLoader : MonoBehaviour
{
    [SerializeField]
    private RuntimeAnimatorController starterAssetAnimator;

    [SerializeField]
    private GameObject animationRig;

    private void OnEnable()
    {
        // Subscribe to the events
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestChangeAvatar), ReloadAvatar);
        WowEventManager.StartListening(nameof(WowEvents.OnPlayerRequestMetaport), PerformMetaport);
    }

    private void OnDisable()
    {
        // Unsubscribe to the events
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestChangeAvatar), ReloadAvatar);
        WowEventManager.StopListening(nameof(WowEvents.OnPlayerRequestMetaport), PerformMetaport);
    }

    private void ReloadAvatar(string avatarURL)
    {
        if (string.IsNullOrEmpty(avatarURL))
        {
            Debug.LogError("Haven't received Avatar URL");
        } else
        {
            Debug.Log("Reloading the new AvatarURL - " + avatarURL);

            var avatarLoader = new AvatarLoader();
            Debug.Log($"Started reloading avatar. [{Time.timeSinceLevelLoad:F2}]");

            // Remove previous avatar
            GameObject go = GameObject.Find("Armature").transform.parent.gameObject;
            if (go != null)
            {
                Destroy(go);
            }

            avatarLoader.OnCompleted += (sender, args) =>
            {
                Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");

                GameObject go = GameObject.Find("Armature").transform.parent.gameObject;
                if (go != null)
                {
                    go.transform.parent = transform;
                }
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localRotation = Quaternion.Euler(0, 0, 0);

                // Add animator and necessary callbacks to handle walk and land animation audio
                go.AddComponent<RuntimeAnimationSoundLoader>();

                go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;

                // Add animation rigging
                AddAnimationRigging(go);

                // Trigger OnInitialized Event
                WowEventManager.TriggerEvent(nameof(WowEvents.OnRPMAvatarLoaded));
            };

            avatarLoader.OnFailed += (sender, args) =>
            {
                WowEventManager.TriggerEvent(nameof(WowEvents.OnRPMAvatarLoaded));
                WowEventManager.TriggerEvent(WowEvents.OnGameError.ToString(), args.Type.ToString());
                Debug.LogError(args.Message);
            };

            avatarLoader.LoadAvatar(avatarURL);
        }
    }

    private void AddAnimationRigging(GameObject go)
    {
        if (animationRig != null)
        {
            // Move the Rig gameobject inside the newly created runtime avatar
            animationRig.transform.parent = go.transform;

            // Add the RigBuilder component to the parent
            go.transform.AddComponent<RigBuilder>();

            // Find the Chest (Spine1) & Head gameobject
            Transform chest = go.transform.Find("Armature/Hips/Spine/Spine1");
            Transform head = go.transform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck/Head");

            // Add constraint to rig for the Chest animation
            Transform bodyLookAt = animationRig.transform.Find("BodyLookAt");
            MultiAimConstraint bodyMultiAimConstraint = bodyLookAt.gameObject.GetComponent<MultiAimConstraint>();
            bodyMultiAimConstraint.data.constrainedObject = chest;

            // Add constraint to rig for the Head animation
            Transform headLookAt = animationRig.transform.Find("HeadLookAt");
            MultiAimConstraint headMultiAimConstraint = headLookAt.gameObject.GetComponent<MultiAimConstraint>();
            headMultiAimConstraint.data.constrainedObject = head;

            // Create the Rig object with the Rig component in it
            Rig rig = animationRig.AddComponent<Rig>();

            // Create the rigBuilder component and add the above Rig to its RigLayer
            RigBuilder rigbuilder = go.transform.GetComponent<RigBuilder>();
            rigbuilder.layers.Clear();
            rigbuilder.layers.Add(new RigLayer(rig, true));
            rigbuilder.enabled = true;
            rigbuilder.Build();
        }
    }

    private void PerformMetaport()
    {
        StartCoroutine(TranslateCharacterPostiion(3f));
    }

    IEnumerator TranslateCharacterPostiion(float timeDuration)
    {
        Vector3 startingPosition = transform.position;
        Vector3 finalPosition = new Vector3(transform.position.x, transform.position.y - 36f, transform.position.z);
        float elapsedTime = 0;

        while(elapsedTime < timeDuration)
         {
            transform.position = Vector3.Lerp(startingPosition, finalPosition, (elapsedTime / timeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Avatar animated");

        // Call metaport function
        LinkMaster.LaunchPage();
    }
}
