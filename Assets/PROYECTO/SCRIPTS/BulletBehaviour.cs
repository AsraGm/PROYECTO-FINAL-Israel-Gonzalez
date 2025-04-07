using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Desroy on Floor")]
    public LayerMask groundLayer;

    public float destroyDelay = 0f;

    public void OnCollisionEnter(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
