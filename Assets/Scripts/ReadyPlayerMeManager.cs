using UnityEngine;
using Siccity.GLTFUtility;
using Unity.VisualScripting;

namespace ReadyPlayerMe
{
    public class ReadyPlayerMeManager : MonoBehaviour
    {
        [SerializeField]
        private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/535c4949-de82-49b8-96ab-a9b5453badad.glb";

        [SerializeField]
        private RuntimeAnimatorController starterAssetAnimator;



        private void Awake()
        {
            //avatarUrl = (string)Variables.Saved.Get("AvatarURL");// Pulls the saved Variable from from Visual Scripting DB
            
        }

        private void Start()
        {
            Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");

            var avatarLoader = new AvatarLoader();
            avatarLoader.OnCompleted += (sender, args) =>
            {
                Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");
                string NotificationMessage = $"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]";
                Variables.Application.Set("PlayerActivity", NotificationMessage);

                Invoke(nameof(GetMyCustomAvatarSorted), 5);
            };
            avatarLoader.OnFailed += (sender, args) =>
            {
                Debug.Log(args.Type);
                string NotificationMessage = $"Error "+args.Type;
                Variables.Application.Set("PlayerActivity", NotificationMessage);
            };
            
           avatarLoader.LoadAvatar(avatarUrl);

           



        }


        public void GetMyCustomAvatarSorted()
        {
            string avatarName = "Armature";
            //GameObject go = GameObject.Find(avatarName);

            GameObject go = GameObject.Find(avatarName).transform.parent.gameObject;
            if (!go )
            {
                Debug.Log("Unable to find Avatar");
                return;
            }

            else
            {
                Debug.Log("Successfully found object avatar");
                go.transform.parent = transform;
                Debug.Log("Transformed successsfully");
                go.transform.localPosition = new Vector3(0, 0, 0);
                Debug.Log("Localpost successsfully");
                go.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("LocalRot successsfully");
                go.AddComponent<AnimationEventsRPMVarient>();
                Debug.Log("AnimationEWvent successsfully");
                go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;
                Debug.Log("WowSuccessful animation successsfully");
                go.AddComponent<StarterAssets.ThirdPersonController>();

                return;
            }

        }
    }
}
