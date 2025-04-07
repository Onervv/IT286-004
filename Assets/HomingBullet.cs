using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 20f;
    public float homingStrength = 5f;
    private Transform targetEnemy;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody missing on HomingBullet prefab");
        }

        FindNearestEnemy();
    }

    void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            Vector3 directionToTarget = (targetEnemy.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * homingStrength);
            rb.velocity = -transform.forward * speed;
        }
        else
        {
            rb.velocity = -transform.forward * speed;
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            transform.LookAt(targetEnemy);
        }
        else
        {
            Debug.Log("No enemies found!");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Health>().currentHealth -= 20;
        }

        Destroy(gameObject);
    }
}
