using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShrinkingButton.Scripts
{
    public class ButtonReveal : MonoBehaviour
    {
        [SerializeField] private GameObject player, button, buttonText;
        public float minPlayerDistance = 3.0f, maxPlayerDistance = 20.0f, resizeScale = 5.0f;
        
        [SerializeField] private string keyCode;
        private TextMeshProUGUI _buttonTextContent;

        private void Awake()
        {
            _buttonTextContent = buttonText.GetComponent<TextMeshProUGUI>();
            Debug.Log("The module is: " + _buttonTextContent);
        }

        private void Update()
        {
            UpdateNavigationGuidance();
        }

        public void UpdateNavigationGuidance()
        {
            var currentPlayerDistance = CalculatePlayerDistance();

            //Activate the button only when the player is nearby
            button.SetActive(currentPlayerDistance <= maxPlayerDistance);

            //Activate text only when the player is nearby
            buttonText.SetActive((currentPlayerDistance <= minPlayerDistance));


            //Always rotate the button object to face the player
            button.transform.LookAt(player.transform);

            ShrinkButton(currentPlayerDistance);
            SetButtonText(keyCode);
        }

        private float CalculatePlayerDistance()
        {
            var currentPlayerDistance = Vector3.Distance(player.transform.position, transform.position);
            return currentPlayerDistance;
        }

        private void ShrinkButton(float currentPlayerDistance)
        {
            if (button.activeInHierarchy)
            {
                button.transform.localScale = (currentPlayerDistance / resizeScale) * (transform.localScale);
            }
        }

        private void SetButtonText(string text)
        {
            _buttonTextContent.text = text;

        }
    }
}