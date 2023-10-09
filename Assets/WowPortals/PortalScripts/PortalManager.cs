using System;
using UnityEngine;
using WowQuests;

namespace WowPortals.PortalScripts
{
    public class PortalManager : MonoBehaviour
    {
        //Creating an event to inform other systems that portals have been activated.
        public static event Action OnPortalActivated;

        [SerializeField] private GameObject vortex;
        [SerializeField] private ParticleSystem vortexParticles;
        [SerializeField] private bool isPortalActive;

        private void OnEnable() => WowQuestManager.OnAllQuestsCompleted += PortalActivation; 
        

        private void OnDisable() => WowQuestManager.OnAllQuestsCompleted -= PortalActivation;

        private void OnTriggerEnter(Collider other)
        {
            switch (isPortalActive)
            {
                case true:
                    Debug.Log("Player has entered an active Portal");

                    ActivatePortals();
                    break;
                case false:
                    Debug.Log("Player has entered an inactive portal");

                    DeactivatePortals();
                    break;
            }
        }

        private void ActivatePortals()
        {
            vortexParticles.Play();
            if (vortex != null) vortex.SetActive(true);
            OnPortalActivated?.Invoke();
        }

        private void DeactivatePortals()
        {
            vortexParticles.Play();

            if (vortex != null) vortex.SetActive(false);
        }

        public void PortalActivation(bool activationStatus)
        {
            isPortalActive = activationStatus;
            Debug.Log("Portal activation status: " + isPortalActive);
        }
    }
}