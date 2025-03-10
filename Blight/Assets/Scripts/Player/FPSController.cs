
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSController : MonoBehaviour
    {
        // Movement
        public Camera playerCamera;
        public float walkSpeed = 6f;
        public float runSpeed = 12f;
        public float jumpPower = 7f;
        public float gravity = 10f;
        public bool canMove = true;

        public float lookSpeed = 2f;
        public float lookXLimit = 45f;

        // FOV Effects
        public float normalFOV = 60f;
        public float sprintFOV = 80f;
        public float fovSmoothSpeed = 5f;

        Vector3 _moveDirection = Vector3.zero;
        private float _rotationX;

        // Player Interface
        public Image staminaBar;
        public float stamina, maxStamina = 100f;
        public float sprintCost = 10f;
        public Image healthBar;
        public float health, maxHealth = 100f;


        CharacterController _characterController;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Set initial health and stamina
            health = maxHealth;
            stamina = maxStamina;
        }

        void Update()
        {

            #region Handles Movment

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isTryingToRun = Input.GetKey(KeyCode.LeftShift);
            bool canSprint = stamina > 0;
            bool isRunning = isTryingToRun && canSprint; // Only true if sprinting is allowed
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = _moveDirection.y;
            _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            #endregion

            #region Handles Jumping

            if (Input.GetButton("Jump") && canMove && _characterController.isGrounded)
            {
                _moveDirection.y = jumpPower;
            }
            else
            {
                _moveDirection.y = movementDirectionY;
            }

            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion

            #region Handles Rotation

            _characterController.Move(_moveDirection * Time.deltaTime);

            if (canMove)
            {
                _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion

            #region Handles FOV Effect

            float targetFOV = isRunning ? sprintFOV : normalFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovSmoothSpeed);

            #endregion

            #region Handles Stamina & Health

// Update UI elements
            healthBar.fillAmount = health / maxHealth;
            staminaBar.fillAmount = stamina / maxStamina;
// Handle stamina changes
            if (isRunning)
            {
                stamina -= sprintCost * Time.deltaTime;

                // Drain health when stamina is critically low
                if (stamina <= 20)
                {
                    health -= 5f * Time.deltaTime;
                }
            }
            else
            {
                // Regenerate stamina when not running
                stamina += (sprintCost / 2) * Time.deltaTime;
            }

// Calculate stamina color - interpolate between red and green
            float staminaRatio = stamina / maxStamina;
            staminaBar.color = Color.Lerp(Color.red, Color.green, staminaRatio);

// Ensure values stay within bounds
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            health = Mathf.Clamp(health, 0, maxHealth);

        #endregion
        }
    } 
}

    

