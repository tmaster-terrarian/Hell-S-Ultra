using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSi.Utility;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class JumpThroughPlatform : MonoBehaviour
{
    GameObject player;
    BoxCollider2D boxCollider2d;
    BoxCollider characterControllerCollider;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameManager.GetPlayer();

        spriteRenderer = GetComponent<SpriteRenderer>();

        boxCollider2d = GetComponent<BoxCollider2D>();
        boxCollider2d.size = new Vector2(spriteRenderer.bounds.size.x, 0.25f);
        boxCollider2d.offset = new Vector2(boxCollider2d.offset.x, -0.125f);

        GameObject collider3d = new GameObject(" ");
        BoxCollider boxCollider = collider3d.AddComponent<BoxCollider>();
        collider3d.transform.parent = transform;
        collider3d.transform.position = transform.position;
        boxCollider.size = new Vector3(boxCollider2d.size.x, boxCollider2d.size.y, 0.5f);
        boxCollider.center = new Vector3(boxCollider2d.offset.x, boxCollider2d.offset.y, 0);
        characterControllerCollider = boxCollider;
    }

    void Update()
    {
        if(player.transform.position.y < transform.position.y)
        {
            gameObject.layer = 7;
            boxCollider2d.isTrigger = true;
            characterControllerCollider.isTrigger = true;
        }
        else
        {
            gameObject.layer = 3;
            boxCollider2d.isTrigger = false;
            characterControllerCollider.isTrigger = false;
        }
    }
}
