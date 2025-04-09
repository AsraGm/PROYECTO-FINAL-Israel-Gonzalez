using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULLETBEHAVIOUR : MonoBehaviour
{
    [Header("Destroy")]
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
