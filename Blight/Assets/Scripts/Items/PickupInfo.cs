using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum effects
{
    None,
    Health,
    Stamina,
    Speed,
    Jump,
    Gravity,
}

public class PickupInfo : MonoBehaviour
{
    public effects effect = effects.None;
    public int effectAmt;
    public float duration = 10f; // Duration for temporary effects like speed, jump, gravity
    
    // Effect Amounts
    public float effectAmtSpeed = 4f;
    public float effectAmtJump = 10f;
    public float effectAmtGravity = 2f;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Pickup Entered");

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            FPSController player_controller = player.GetComponent<FPSController>();

            switch (effect)
            {
                case effects.Health:
                    player_controller.health = Mathf.Clamp(player_controller.health + effectAmt, 0, player_controller.maxHealth);
                    break;

                case effects.Stamina:
                    player_controller.stamina = Mathf.Clamp(player_controller.stamina + effectAmt, 0, player_controller.maxStamina);
                    break;

                case effects.Speed:
                    StartCoroutine(TemporaryStatChange(() => player_controller.runSpeed += effectAmtSpeed, () => player_controller.runSpeed -= effectAmtSpeed));
                    break;

                case effects.Jump:
                    StartCoroutine(TemporaryStatChange(() => player_controller.jumpPower += effectAmtJump, () => player_controller.jumpPower -= effectAmtJump));
                    break;

                case effects.Gravity:
                    StartCoroutine(TemporaryStatChange(() => player_controller.gravity -= effectAmtGravity, () => player_controller.gravity += effectAmtGravity));
                    break;
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator TemporaryStatChange(System.Action applyEffect, System.Action revertEffect)
    {
        applyEffect.Invoke();
        yield return new WaitForSeconds(duration);
        revertEffect.Invoke();
    }
}