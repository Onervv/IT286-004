using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject homingGun;

    private GameObject activeWeapon;

    void Start()
    {
        activeWeapon = pistol;
        ActivateWeapon(pistol);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon(pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon(shotgun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWeapon(homingGun);
        }
    }

    void ActivateWeapon(GameObject weapon)
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        homingGun.SetActive(false);

        weapon.SetActive(true);
        activeWeapon = weapon;
    }
}
