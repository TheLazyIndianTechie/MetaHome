//using UnityEngine;
//using Siccity.GLTFUtility;
//using UnityEditor;
//using Mirror;
//using TMPro;
//using Unity.VisualScripting;
//using System.Runtime.InteropServices;

//namespace ReadyPlayerMe
//{

//    public class RuntimeLoader : NetworkBehaviour
//    {
//        [SerializeField]
//        private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";

//        [SerializeField]
//        private RuntimeAnimatorController starterAssetAnimator;


//        public TextMeshProUGUI playerNameText;

//        private GameObject loadingPannel;


//        public string playerName;

//        [DllImport("__Internal")]
//        private static extern void displayChatToggle();

//        private void Start()
//        {
//            if (isLocalPlayer)
//            {
//                string displayName = (string)Variables.Saved.Get("PlayerName");

//                playerName = displayName;

//                loadingPannel = GameObject.FindGameObjectWithTag("Loading Pannel");
//                if (!GameManager.testMode)
//                {
//                    avatarUrl = GameManager.avatarUrlOfPlayer;
//                }

//                InformAllClients(avatarUrl, displayName);
//                //LoadBuffer();
//                LoadPreviousClients(gameObject);
//            }
//        }

//        [Command]
//        private void InformAllClients(string avatarUrl, string displayName)
//        {
//            LoadAvatar(avatarUrl, displayName);
//        }


//        //this will load the previous clients.
//        [Command]
//        private void LoadPreviousClients(GameObject target)
//        {
//            LoadAvatarOnNewClient(target);
//        }


//        [ClientRpc(includeOwner = false)]
//        private void LoadAvatarOnNewClient(GameObject target)
//        {
//            // LoadOnNewClient(target);

//            //here we are getting avatar url of local player.
//            //finding local player..
//            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
//            GameObject localPlayer = allPlayers[0];
//            for (int i = 0; i < allPlayers.Length; i++)
//            {
//                if (allPlayers[i].GetComponent<NetworkBehaviour>().isLocalPlayer)
//                {
//                    localPlayer = allPlayers[i];
//                    Debug.Log("this is local player!");
//                }
//            }

//            string playerAU = localPlayer.GetComponent<RuntimeLoader>().avatarUrl;
//            uint networkid = localPlayer.GetComponent<NetworkBehaviour>().netId;
//            Debug.Log("This player's net id is:" + networkid);
//            Debug.Log("this player's url is:" + playerAU);
//            string displayName = localPlayer.GetComponent<RuntimeLoader>().playerName; ;
//            LoadOnNewClient(target, playerAU, networkid, displayName);
//        }

//        [Command(requiresAuthority = false)]
//        private void LoadOnNewClient(GameObject target, string playerAU, uint networkid, string displayName)
//        {
//            NetworkIdentity targetIdentity = target.GetComponent<NetworkIdentity>();

//            //finding local player..
//            /* GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
//             GameObject localPlayer = allPlayers[0];
//             for (int i = 0; i < allPlayers.Length; i++)
//             {
//                 if (allPlayers[i].GetComponent<NetworkBehaviour>().isLocalPlayer)
//                 {
//                     localPlayer = allPlayers[i];
//                     Debug.Log("this is local player!");
//                 }
//             }

//             string playerAU = localPlayer.GetComponent<RuntimeLoader>().avatarUrl;
//             uint networkid = localPlayer.GetComponent<NetworkBehaviour>().netId;
//             Debug.Log("This player's net id is:" + networkid);
//             Debug.Log("this player's url is:" + playerAU);*/
//            LoadIndividualOnTarget(targetIdentity.connectionToClient, playerAU, networkid, displayName);
//        }

//        [TargetRpc]
//        private void LoadIndividualOnTarget(NetworkConnection target, string playeravatarUrl, uint nId, string displayName)
//        {
//            Debug.Log(nId);
//            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
//            GameObject player = players[0];
//            for (int i = 0; i < players.Length; i++)
//            {
//                Debug.Log(players[i].GetComponent<NetworkBehaviour>().netId);
//                if (players[i].GetComponent<NetworkBehaviour>().netId == nId)
//                {
//                    player = players[i];
//                }
//            }
//            Debug.Log($"Started loading previous avatars. [{Time.timeSinceLevelLoad:F2}]");
//            Debug.Log(playeravatarUrl);
//            var avatarLoader = new AvatarLoader();
//            avatarLoader.OnCompleted += (sender, args) =>
//            {
//                string avatarName = avatarLoader.GetAvatarNameFromScene();
//                GameObject go = GameObject.Find(avatarName);
//                go.transform.parent = player.transform;
//                go.transform.localPosition = new Vector3(0, 0, 0);
//                go.transform.localRotation = Quaternion.Euler(0, 0, 0);
//                go.AddComponent<AnimationEventsRPM>();
//                go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;
//                Debug.Log($"Loaded avatar previous. [{Time.timeSinceLevelLoad:F2}]");

//                player.GetComponent<RuntimeLoader>().playerNameText.text = displayName;
//                Debug.Log("player name for previous avatar is: " + displayName);
//            };
//            avatarLoader.OnFailed += (sender, args) =>
//            {
//                Debug.Log(args.Type);
//                player.GetComponent<RuntimeLoader>().playerNameText.text = displayName;
//                Debug.Log("player name for previous avatar is: " + displayName);
//            };

//            avatarLoader.LoadAvatar(playeravatarUrl);

//        }

//        //this will load avatar on client..
//        [ClientRpc]
//        private void LoadAvatar(string avatarUrl, string displayName)
//        {
//            Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
//            Debug.Log(gameObject.GetComponent<NetworkBehaviour>().netId);
//            var avatarLoader = new AvatarLoader();
//            avatarLoader.OnCompleted += (sender, args) =>
//            {
//                Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");
//                string avatarName = avatarLoader.GetAvatarNameFromScene();
//                GameObject go = GameObject.Find(avatarName);
//                go.transform.parent = transform;
//                go.transform.localPosition = new Vector3(0, 0, 0);
//                go.transform.localRotation = Quaternion.Euler(0, 0, 0);
//                go.AddComponent<AnimationEventsRPM>();
//                go.GetComponent<Animator>().runtimeAnimatorController = starterAssetAnimator;


//                loadingPannel.SetActive(false);
//                displayChatToggle();

//                playerNameText.text = displayName;
//            };
//            avatarLoader.OnFailed += (sender, args) =>
//            {
//                Debug.Log(args.Type);
//                playerNameText.text = displayName;

//            };

//            avatarLoader.LoadAvatar(avatarUrl);
//        }
//    }
//}
