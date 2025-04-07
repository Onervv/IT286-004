using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float cooldownSpeed;
    public float fireRate;
    public float recoilCooldown;
    private float accuracy;
    public float maxSpreadAngle;
    public float timeTillMaxSpread;
    public GameObject bullet;
    public GameObject shootPoint;
    public AudioSource gunshot;
    public AudioClip singleShot;

    void Update()
    {
        cooldownSpeed += Time.deltaTime * 60f;

        if (Input.GetButton("Fire1"))
        {
            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                gunshot.PlayOneShot(singleShot);
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }
        }
        else
        {
            recoilCooldown -= Time.deltaTime;
            if (recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }
        }
    }

    void Shoot()
    {
        float currentSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);
        Vector3 spreadDirection = shootPoint.transform.forward;
        spreadDirection = Quaternion.Euler(
            Random.Range(-currentSpread, currentSpread),
            Random.Range(-currentSpread, currentSpread),
            0
        ) * spreadDirection;

        Instantiate(bullet, shootPoint.transform.position, Quaternion.LookRotation(spreadDirection));
    }
}