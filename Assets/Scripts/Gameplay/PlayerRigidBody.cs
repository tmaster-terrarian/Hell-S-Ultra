using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidBody : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer != 7)
        {
            Debug.Log("player entered a trigger: " + other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            playerController.OnTouchTrigger(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer != 7)
        {
            playerController.OnLeaveTrigger(other);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        playerController.OnTouchCollider(other);
    }
}
