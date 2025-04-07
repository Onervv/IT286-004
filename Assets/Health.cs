using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;

    void Start()
    {

    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}