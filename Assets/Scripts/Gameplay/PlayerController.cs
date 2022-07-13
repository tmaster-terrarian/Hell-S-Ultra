using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using CSi.Utility;
using CSi.InputV2;

[RequireComponent(typeof(Entity))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] float walkSpeed = 6.0f;
	[SerializeField] float gravity = -13.0f;
	[SerializeField] float jumpHeight = 2.0f;
	[SerializeField] LayerMask groundLayer;
	[SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
	[SerializeField] GameObject head = null;
	[SerializeField] Sprite headSprite = null;

	Vector3 headPos;
	Vector3 camPos;

	bool isGrounded = false;
	float velocityY = 0.0f;
	float jumpForce;

	CharacterController controller = null;
	Rigidbody2D rb = null;
	SpriteRenderer sr;

	Vector2 targetDir = Vector2.zero;
	Vector2 currentDir = Vector2.zero;
	Vector2 currentDirVelocity = Vector2.zero;

	[HideInInspector] public Vector3 velocity;

	public bool canInput = true;
	public bool dead = false;

	Entity entity;
	GameObject mainCam;

	[HideInInspector] public Vector2 minCameraBounds;
	[HideInInspector] public Vector2 maxCameraBounds;

	// object-based camera bounds. takes priority over manual if it is not null.
	// the bounds are defined by the object's scale. min = new Vector3(-transform.scale.x/2, -transform.scale.y/2); max = new Vector3(transform.scale.x/2, transform.scale.y/2);
	[HideInInspector] public Transform cameraBoundsTransform = null;

	// vertical scale of camera
	public float cameraSizeY;
	Vector2 cameraSize;

	BoxCollider2D collider2d;

	public GameObject punchProjectile;
	public float punchCooldownTime = 0.1f;

	Vector2 controllerMove;

	void Start()
	{
		controller = GetComponent<CharacterController>();

		rb = GetComponent<Rigidbody2D>();

		sr = GetComponent<SpriteRenderer>();

		SpriteRenderer headSr = head.AddComponent<SpriteRenderer>();
		headSr.sprite = headSprite;
		headSr.sortingOrder = 6;
		head.transform.parent = null;

		entity = GetComponent<Entity>();
		entity.SetIsPlayer(true);

		canInput = true;

		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		cameraSize = new Vector2(16, 9);
	}

	void Update()
	{
		if(!dead)
		{
			if(sr.flipX == false)
			{
				headPos = transform.position + new Vector3(-0.064f, 0.9375f, 0);
				camPos = new Vector3(0.5f, 0.5f, -10);
			}
			if(sr.flipX == true)
			{
				headPos = transform.position + new Vector3(0.064f, 0.9375f, 0);
				camPos = new Vector3(-0.5f, 0.5f, -10);
			}
		}

		if(cameraBoundsTransform != null)
		{
			BoxCollider2D cameraBoundsCollider = cameraBoundsTransform.GetComponent<BoxCollider2D>();

			minCameraBounds = new Vector2(-(cameraBoundsCollider.size.x / 2) + (cameraSize.x / 2) + cameraBoundsCollider.offset.x, -(cameraBoundsCollider.size.y / 2) + (cameraSize.y / 2) + cameraBoundsCollider.offset.y);
			maxCameraBounds = new Vector2( (cameraBoundsCollider.size.x / 2) - (cameraSize.x / 2) + cameraBoundsCollider.offset.x,  (cameraBoundsCollider.size.y / 2) - (cameraSize.y / 2) + cameraBoundsCollider.offset.y);
		}
		else
		{
			minCameraBounds = new Vector2(-Mathf.Infinity, -Mathf.Infinity);
			maxCameraBounds = new Vector2(Mathf.Infinity, Mathf.Infinity);
		}

		head.transform.position = Vector3.Lerp(head.transform.position, headPos, 0.55f);
		head.transform.position = Utility.RoundVector(head.transform.position, Utility.onePixel);

		if(cameraBoundsTransform != null)
		{
			mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(Mathf.Clamp(transform.position.x + camPos.x, minCameraBounds.x, maxCameraBounds.x), Mathf.Clamp(transform.position.y + camPos.y, minCameraBounds.y, maxCameraBounds.y), camPos.z), 5f * Time.fixedDeltaTime);
		}
		else
		{
			mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, transform.position + camPos, 5f * Time.fixedDeltaTime);
		}

		transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		isGrounded = Physics2D.OverlapBox(transform.position + new Vector3(0, 0.25f, 0), new Vector2(0.1f, 0.5f), 0, groundLayer);

		UpdateAttack();

		if(dead)
		{
			Kill();
		}
	}

	void FixedUpdate()
	{
		UpdateMovement();
	}

	void UpdateMovement()
	{
		if(canInput)
			//targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			targetDir = new Vector2(InputV2.globalHorizontal, InputV2.globalVertical);
		else targetDir = Vector2.zero;

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

		if(canInput)
		{
			if(isGrounded /*|| controller.isGrounded*/)
			{
				velocityY = Mathf.Clamp(velocityY, 0.0f, float.MaxValue);
				if(InputV2.OnKeyDown(KeyCode.Space) || InputV2.OnKeyDown(KeyCode.JoystickButton0))
				{
					jumpForce = Mathf.Sqrt(jumpHeight * -2.15f * gravity);
					SFX.Play("ovumelJump00");
				}
				if(!Input.GetKeyDown(KeyCode.Space) && !InputV2.OnKeyDown(KeyCode.JoystickButton0))
				{
					jumpForce = 0.0f;
				}
			}
		}

		velocityY += gravity * Time.deltaTime;

		//velocity = ((transform.right * currentDir.x) * walkSpeed) + (Vector3.up * jumpForce) + (Vector3.up * velocityY);
		velocity = ((Vector2.right * currentDir.x) * walkSpeed) + (Vector2.up * jumpForce) + (Vector2.up * velocityY);

		//controller.Move(velocity * Time.deltaTime);
		rb.velocity = velocity;
		rb.position = Utility.RoundVector(transform.position, Utility.onePixel);
	}

	void UpdateAttack()
	{
		bool punching = false;

		if(canInput)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				punching = true;
			}
			else punching = false;
		}

		if(punching)
		{
			StartCoroutine(Punch());
			StartCoroutine(PunchCooldown(punchCooldownTime));
		}
	}

	public void OnTouchTrigger(Collider2D collider)
	{
		if(!dead)
		{
			if(collider.gameObject.layer == 6)
			{
				Kill();
			}
			if(collider.gameObject.CompareTag("room"))
			{
				cameraBoundsTransform = collider.transform;
			}
		}
	}

	public void OnLeaveTrigger(Collider2D collider)
	{
		if(!dead)
		{
			
		}
	}

	public void OnTouchCollider(Collision2D collision)
	{
		if(!dead)
		{
			if(collision.gameObject.layer == 6)
			{
				Kill();
			}
		}
	}

	public void Kill()
	{
		headPos = transform.position + Vector3.up;
		camPos = transform.position + Vector3.up;
		entity.Die();
	}

	public IEnumerator Punch()
	{
		GameObject go = GameObject.Instantiate(punchProjectile, transform.position, Quaternion.identity, transform);
		if(sr.flipX == false)
		{
			go.transform.localPosition = new Vector3(0.375f, 0.3125f);
		}
		if(sr.flipX == true)
		{
			go.transform.localPosition = new Vector3(-0.375f, 0.3125f);
		}
		yield return new WaitForSeconds(0.1f);
		Destroy(go);
		yield return null;
	}

	public IEnumerator PunchCooldown(float time)
	{
		yield return new WaitForSeconds(time);
	}
}
