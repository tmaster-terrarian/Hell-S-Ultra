using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hurter : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.layer)
        {
            case 8 when gameObject.layer == 7:
                break;
            default:
                if (collider.GetComponent<Entity>() != null)
                {
                    collider.GetComponent<Entity>().Hurt(damage);
                }
                break;
        }
    }
}
