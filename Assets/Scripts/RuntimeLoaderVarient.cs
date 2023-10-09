using UnityEngine;
using Siccity.GLTFUtility;
using UnityEditor;
using Mirror;
using TMPro;
using Unity.VisualScripting;

namespace ReadyPlayerMe
{

    public class RuntimeLoaderVarient : MonoBehaviour
    {
        [SerializeField]
        private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";

        [SerializeField]
        private RuntimeAnimatorController starterAssetAnimator;

        //[SerializeField]
        // private TextMeshProUGUI playerNameText;

        [SerializeField]
        private GameObject loadingPannel;
        private void Start()
        {
            // string displayName = (string)Variables.Saved.Get("PlayerName");

            //avatarUrl = GameManager.avatarUrlOfPlayer;



            LoadAvatar(avatarUrl);
        }
        private void LoadAvatar(string avatarUrl)
        {
            Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
            var avatarLoader = new AvatarLoader();
            avatarLoader.OnCompleted += (sender, args) =>
            {
                Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");
                //string avatarName = avatarLoader.GetAvatarNameFromScene();1
                GameObject go = GameObject.Find("Armature").transform.parent.gameObject;
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localRotation = Quaternion.Euler(0, 0, 0);
                go.AddComponent<AnimationEventsRPMVarient>();
                go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;

                loadingPannel.SetActive(false);
            };
            avatarLoader.OnFailed += (sender, args) =>
            {
                Debug.Log(args.Type);
            };

            avatarLoader.LoadAvatar(avatarUrl);
        }
    }
}
