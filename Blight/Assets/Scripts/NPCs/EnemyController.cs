using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    float max_hp = 100;
    public float health = 100;

    NavMeshAgent agent;
    public GameObject target;
    public GameObject Player;

    public int scan_radius = 50;
    public int attack_radius = 10;

    public float damage_amt = 0.01f;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        target = SearchRadius(scan_radius);
        if (!target)
        {
            agent.destination = agent.transform.position;
            return;
        }
            
        if (SearchRadius(attack_radius))
        {
            target.GetComponent<FPSController>().health -= damage_amt; 
        }

        agent.destination = target.transform.position;
    }

    public GameObject SearchRadius(int radius)
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < radius) 
            return Player;

        return null;
    }

    public void Enemy_Death()
    {
        // Can add enemy drops here
        Destroy(gameObject);
    }


    public void TakeDamage(float dmg_amt)
    {
        if ((health -= dmg_amt) <= 0) 
            Enemy_Death();
    }


}

