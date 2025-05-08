using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WEAPON : MonoBehaviour
{
    public int damage;
    public float range;
    public float fireRate; //cadencia del arma
    public float accuracy; // punteria

    public int currentAmmo;
    public int currentMaxAmmo;
    public int ammo;
    public int maxAmmo;

    public abstract void Shoot(); 

    public abstract void Reload();

    public bool CheckAmmo()
    {
        return currentAmmo <= 0 && ammo <= 0;
    }
}
