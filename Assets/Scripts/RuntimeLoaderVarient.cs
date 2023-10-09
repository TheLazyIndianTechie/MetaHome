using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

namespace ReadyPlayerMe
{
    public class RuntimeLoaderVarient : MonoBehaviour
    {
        [SerializeField]
        private RuntimeAnimatorController starterAssetAnimator;

        [SerializeField]
        private TextMeshProUGUI playerNameText;

        [SerializeField]
        private GameObject loadingPannel;

        [SerializeField]
        private GameObject pauseControls;

        [SerializeField]
        private Canvas hudOverlay;

        private UserData userData;

        private string defaultAvatarURL = "https://api.readyplayer.me/v1/avatars/637730fda9869f44e5e64f98.glb";

        private void Awake()
        {
            userData = (UserData)Variables.Saved.Get("UserDataJSON");
        }

        private void Start()
        {
            if (loadingPannel != null)
                loadingPannel.SetActive(true);
            if (hudOverlay != null)
                hudOverlay.enabled = false;
            if (pauseControls != null)
                pauseControls.SetActive(false);

            // Load Avatar
            LoadAvatar(userData.avatarUrl);

        }


        private void LoadAvatar(string avatarUrl)
        {
            if (string.IsNullOrEmpty(avatarUrl))
            {
                Debug.Log("Haven't received Avatar URL");
            } else
            {
                Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
                var avatarLoader = new AvatarLoader();
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
                    go.AddComponent<AnimationEventsRPMVarient>();
                    go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;

                    if (loadingPannel != null)
                        loadingPannel.SetActive(false);
                    if (hudOverlay != null)
                        hudOverlay.enabled = true;
                    if (pauseControls != null)
                        pauseControls.SetActive(true);
                    
                    
                };
                avatarLoader.OnFailed += (sender, args) =>
                {
                    avatarLoader.LoadAvatar(defaultAvatarURL);
                    Debug.Log(args.Type);
                };

                avatarLoader.LoadAvatar(avatarUrl);
            }
        }
    }
}
