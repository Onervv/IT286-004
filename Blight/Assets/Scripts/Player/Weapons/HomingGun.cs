using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingGun : MonoBehaviour
{
    public float fireRate = 0.5f;
    public GameObject homingBullet;
    public GameObject shootPoint;
    public AudioSource gunshot;
    public AudioClip singleShot;
    private float cooldownSpeed;

    void Update()
    {
        cooldownSpeed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToHomingGun();
        }

        if (Input.GetButton("Fire1") && cooldownSpeed >= fireRate)
        {
            ShootHomingBullet();
            gunshot.PlayOneShot(singleShot);
            cooldownSpeed = 0;
        }
    }

    void ShootHomingBullet()
    {
        Debug.Log("Homing Bullet Fired!");

        GameObject homingBulletInstance = Instantiate(homingBullet, shootPoint.transform.position, shootPoint.transform.rotation);

        if (homingBulletInstance != null)
        {
            HomingBullet bulletScript = homingBulletInstance.GetComponent<HomingBullet>();

            if (bulletScript == null)
            {
                Debug.LogError("HomingBullet script is missing on the bullet prefab!");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate HomingBullet!");
        }
    }

    void SwitchToHomingGun()
    {
        Debug.Log("Switched to Homing Gun");
    }
}
