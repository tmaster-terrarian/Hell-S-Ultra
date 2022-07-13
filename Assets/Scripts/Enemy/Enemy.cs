using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSi.Utility;

[RequireComponent(typeof(CharacterController), typeof(Entity))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int aiIndex = 0;   // 0: braindead. does nothing.
                                        // 1: simple walker; walks towards player blindly.
                                        // 2: simple walker but it jumps every second if possible.
	[SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
	[SerializeField] float walkSpeed = 6.0f;
	[SerializeField] float gravity = -13.0f;
	[SerializeField] float jumpHeight = 2.0f;
    [SerializeField] LayerMask groundLayer;

    GameObject player;
    PlayerController playerController;
    CharacterController controller;
    Entity entity;
    SpriteRenderer sr;

    bool isGrounded = false;
	float velocityY = 0.0f;
	float jumpForce;

    Vector2 targetDir;
	Vector2 currentDir = Vector2.zero;
	Vector2 currentDirVelocity = Vector2.zero;

    [HideInInspector]
	public Vector3 velocity;

    void Start()
    {
        player = GameManager.GetPlayer();
        playerController = player.GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
        controller.detectCollisions = false;
        entity = GetComponent<Entity>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch(aiIndex)
        { //my first ever switch, what could go wrong?
            case 0:
                targetDir = Vector2.zero;
                break;
            case 1:
                if(player.transform.position.x < transform.position.x)
                {
                    targetDir = new Vector2(-1, 0);
                }
                else if(player.transform.position.x > transform.position.x)
                {
                    targetDir = new Vector2(1, 0);
                }
                break;
            case 2:
                if(player.transform.position.x < transform.position.x)
                {
                    targetDir = new Vector2(-1, 0);
                }
                else if(player.transform.position.x > transform.position.x)
                {
                    targetDir = new Vector2(1, 0);
                }
                break;
            default:
                aiIndex = 0;
                break;
        }

		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		isGrounded = Physics2D.OverlapBox(transform.position + new Vector3(0, 0.25f, 0), new Vector2(0.1f, 0.5f), 0, groundLayer);

		targetDir.Normalize();

		currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

		if(currentDir.x > 0)
		{
			sr.flipX = false;
		}
		if(currentDir.x < 0)
		{
			sr.flipX = true;
		}

		if(controller.isGrounded)
		{
			/*if(jump == true)
			{
                jump = false;
				jumpForce = Mathf.Sqrt(jumpHeight * -2.15f * gravity);
			}
			if(jump == false)
			{
				jumpForce = 0.0f;
			}*/
			velocityY = Mathf.Clamp(velocityY, 0.0f, float.MaxValue);

            StartCoroutine(Jump());
            jumpForce = 0;
		}

		velocityY += gravity * Time.deltaTime;

		velocity = ((transform.right * currentDir.x) * walkSpeed) + (Vector3.up * jumpForce) + (Vector3.up * velocityY);

		controller.Move(velocity * Time.unscaledDeltaTime);
		transform.position = Utility.RoundVector(transform.position, Utility.onePixel);
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(1);
		jumpForce = Mathf.Sqrt(jumpHeight * -2.15f * gravity);
    }
}
