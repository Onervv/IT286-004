using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game_Function
{
    public class EnterLevel : MonoBehaviour
    {
        // UI References
        public GameObject uiPrompt; 
        public Button yesButton;
    

        private bool _playerInside; // To track if the player is inside the trigger

        private void Start()
        {
            // Ensure the UI prompt is initially disabled
            if (uiPrompt != null)
            {
                uiPrompt.SetActive(false);
            }
        
            if (yesButton != null) 
                yesButton.onClick.AddListener(OnYesPressed);
        
        }

        // Trigger when the player enters the box collider
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger has the "Player" tag
            if (other.CompareTag("Player"))
            {
                _playerInside = true;

                // Show the UI prompt
                if (uiPrompt != null)
                {
                    uiPrompt.SetActive(true);
                
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                }
            }
        }

        // Trigger when the player exits the box collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInside = false;

                // Hide the UI prompt
                if (uiPrompt != null)
                {
                    uiPrompt.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        // Action when "Yes" is pressed
        private void OnYesPressed()
        {
            if (_playerInside)
            {
                // Hide the prompt and load the new scene
                if (uiPrompt != null)
                {
                    uiPrompt.SetActive(false);
                    
                    // Lock the cursor back and hide it
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            
                Debug.Log("Loading new scene...");

                // Load the new scene (replace "NewSceneName" with your scene's name)
                SceneManager.LoadScene("Cave-System");
            }
        }
    
        public void PauseGameForUI(bool showUI)
        {
            if (showUI)
            {
                // Unlock the cursor and make it visible
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Optionally disable the player's camera movement
                enabled = false; // "enabled" refers to the FPSController script
            }
            else
            {
                // Lock the cursor back and hide it
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Re-enable the player's camera movement
                enabled = true;
            }
        }

    }
}