using System;
using UnityEngine;

namespace WowQuests
{
    public class WowQuestManager : MonoBehaviour
    {
        public static Action<bool> OnAllQuestsCompleted;

        private void Start()
        {
            OnAllQuestsCompleted?.Invoke(true);
            Debug.Log("Emitting OnAllQuestsCompleted to be true");
        }
    }
}
