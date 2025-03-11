using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum effects
{
    None,
    Health,
    Stamina,
}

public class PickupInfo : MonoBehaviour
{

    public effects effect = effects.None;
    public int effect_amt = 0;


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
                    player_controller.health = Mathf.Clamp(player_controller.health + effect_amt, 0, player_controller.maxHealth);
                    break;
                case effects.Stamina:
                    player_controller.stamina = Mathf.Clamp(player_controller.stamina + effect_amt, 0, player_controller.maxStamina);
                    break;
            }

            Destroy(gameObject);
        }
    }
}