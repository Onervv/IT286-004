using UnityEngine;

public class ReflectableEnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody rb;
    private bool reflected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        ParrySystem parry = col.gameObject.GetComponent<ParrySystem>();
        if (!reflected && parry != null && parry.IsParrying())
        {
            // Reflect bullet
            Vector3 reflectDirection = -transform.forward;
            transform.rotation = Quaternion.LookRotation(reflectDirection);
            rb.velocity = reflectDirection * speed;
            reflected = true;
            gameObject.tag = "PlayerBullet";
            return;
        }

        if (reflected && col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Health>().currentHealth -= 20;
            Destroy(gameObject);
        }
        else if (!reflected && col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}