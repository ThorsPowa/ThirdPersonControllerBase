using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField] private GameObject weaponLogicLeft;
    [SerializeField] private GameObject weaponLogicRight;

// Left Weapon
    public void EnableWeaponLeft()
    {
        weaponLogicLeft.SetActive(true);
    }

    public void DisableWeaponLeft()
    {
        weaponLogicLeft.SetActive(false);
    }

// Right Weapon
    public void EnableWeaponRight()
    {
        weaponLogicRight.SetActive(true);
    }

    public void DisableWeaponRight()
    {
        weaponLogicRight.SetActive(false);
    }
}