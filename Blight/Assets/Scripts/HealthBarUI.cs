// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// using UnityEngine;
// using UnityEngine.UI;
//
// public class HealthBarUI : MonoBehaviour
// {
//     public Image healthImage;
//
//     public Sprite fullHealthSprite;
//     public Sprite highHealthSprite;
//     public Sprite medHealthSprite;
//     public Sprite lowHealthSprite;
//     public Sprite noHealthSprite;
//
//     public float health = 100f;
//     public float maxHealth = 100f;
//
//     void Update()
//     {
//         float healthPercent = (health / maxHealth) * 100f;
//
//         if (healthPercent >= 80)
//         {
//             healthImage.sprite = fullHealthSprite;
//         }
//         else if (healthPercent >= 60)
//         {
//             healthImage.sprite = highHealthSprite;
//         }
//         else if (healthPercent >= 40)
//         {
//             healthImage.sprite = medHealthSprite;
//         }
//         else if (healthPercent >= 20)
//         {
//             healthImage.sprite = lowHealthSprite;
//         }
//         else
//         {
//             healthImage.sprite = noHealthSprite;
//         }
//     }
//
//     // Optional: Call this from your player script
//     public void SetHealth(float newHealth)
//     {
//         health = Mathf.Clamp(newHealth, 0, maxHealth);
//     }
//
// }


// This was a massive waste of time
