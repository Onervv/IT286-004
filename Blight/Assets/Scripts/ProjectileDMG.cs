using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damageAmount = 25f;

    void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }
    }
}
