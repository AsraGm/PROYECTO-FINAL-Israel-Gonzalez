using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1; // Daño que causa el enemigo

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Heatlh health = collision.gameObject.GetComponent<Heatlh>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
