using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEAPONHANDLER : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Weapon actualWeapon;

    private System.Action shoot;

    private void Start()
    {
        Shoot = AutomaticShoot;

        switch (currentWeapon.GetWeaponType())
        {
            case WeaponType.AutoRifle:
                shootMethod = AutomaticShoot;
                break;

            case WeaponType.Pistol:
                shootMethod = SemiAutomaticShoot;
                break;

            case WeaponType.Shotgun:
                shootMethod = SemiAutomaticShoot; // O podrías tener un método PumpActionShoot
                break;

            default:
                Debug.LogWarning("Tipo de arma no reconocido, usando disparo semiautomático por defecto");
                shootMethod = SemiAutomaticShoot;
                break;
        }
    }

    private void Update()
        {
            Shoot();
        }

        private void AutomaticShoot()
        {
            if (actualWeapon.CheackAmmo() && Input.GetMouseButtom(0))
            {
                actualWeapon.Shoot();
            }
        }

        private void SemiAutomaticShoot()
        {
            if (actualWeapon.CheackAmmo() && Input.GetMouseButtom(0))
            {
                actualWeapon.Shoot();
            }
        }
    }
}