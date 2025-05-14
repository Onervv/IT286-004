
using System;
//using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine.VFX;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSController : MonoBehaviour
    {
        // public static FPSController instance;
        
        // Movement
        public Camera playerCamera;
        public float walkSpeed = 3f;
        public float runSpeed = 7f;
        public float jumpPower = 5f;
        public float gravity = 15f;
        public bool canMove = true;

        public float lookSpeed = 4f;
        public float lookXLimit = 45f;

        // FOV Effects
        public float normalFOV = 60f;
        public float sprintFOV = 80f;
        public float fovSmoothSpeed = 5f;

        Vector3 _moveDirection = Vector3.zero;
        private float _rotationX;

        public GameObject deathVFX;
        public GameObject deathScreen;

        // Player Interface
        public Image staminaBar;
        public float stamina, maxStamina = 100f;
        public float sprintCost = 10f;
        public Image healthBar;
        public float health, maxHealth = 100f;
        
        CharacterController _characterController;

        // Debufs
        public GameObject[] debufs;


       
        //Stats
        public int EnemyKills = 0;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Set initial health and stamina
            health = maxHealth;
            stamina = maxStamina;
            // DontDestroyOnLoad(gameObject); 
        }

        // void Awake()
        // {
        //     // Check if there's already an instance
        //     if (instance != null && instance != this)
        //     {
        //         Destroy(gameObject); // Destroy duplicate
        //         return;
        //     }
        //
        //     // Make this the instance and persist across scenes
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        
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
            
            // healthBarUI.SetHealth(health);

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

            if (health == 0) StartCoroutine(PlayDeathSequence());
        }

        private IEnumerator PlayDeathSequence()
        {
            deathVFX.SetActive(true);
            yield return new WaitForSeconds(3f);
            deathScreen.SetActive(true);
            deathVFX.SetActive(false);
            health = -1;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject spawnPoint = GameObject.Find("Spawn Point");
            gameObject.transform.position = spawnPoint.transform.position;
        }

        public void IncreaseKillCount()
        {
            EnemyKills++;
            if (EnemyKills % 10 == 0) // Change to 5
                ApplyDebuff();
        }

        void ApplyDebuff()
        {
            Debug.Log("SPAWNED DEBUFF");
            Instantiate(debufs[UnityEngine.Random.Range(0, debufs.Length)], gameObject.transform.position, Quaternion.identity);
        }

    }
}

    

