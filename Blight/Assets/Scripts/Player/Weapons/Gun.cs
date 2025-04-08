using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject playerCamera;
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
        cooldownSpeed += Time.deltaTime * 20f;

        if (Input.GetButton("Fire1"))
        {
            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                //gunshot.PlayOneShot(singleShot);
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
        Vector3 fireDirection = playerCamera.transform.forward;
        Vector3 shootOrigin = shootPoint.transform.position;

        float currentSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);

        fireDirection = Quaternion.AngleAxis(UnityEngine.Random.Range(-currentSpread, currentSpread), playerCamera.transform.up) * fireDirection;
        fireDirection = Quaternion.AngleAxis(UnityEngine.Random.Range(-currentSpread, currentSpread), playerCamera.transform.right) * fireDirection;

        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(playerCamera.transform.position, fireDirection, out hit, Mathf.Infinity))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = playerCamera.transform.position + fireDirection * 1000f;
        }

        GameObject tempBullet = Instantiate(bullet, shootOrigin, Quaternion.LookRotation(fireDirection));
        tempBullet.GetComponent<MoveBullet>().hitPoint = targetPoint;
    }
}