using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float fireRate = 1.0f;
    private float cooldownSpeed;

    public int pellets = 8;
    public float spreadAngle = 10.0f;
    public GameObject bullet;
    public GameObject shootPoint;
    public AudioSource gunshot;
    public AudioClip shotgunBlast;

    void Update()
    {
        cooldownSpeed += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownSpeed >= fireRate)
        {
            Shoot();
            gunshot.PlayOneShot(shotgunBlast);
            cooldownSpeed = 0;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Quaternion spread = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );

            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation * spread);
            tempBullet.GetComponent<MoveBullet>().hitPoint = shootPoint.transform.position + (shootPoint.transform.forward * 100);
        }
    }
}
