using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Rigidbody myBody;
	private Animator anim;

	private bool isPlayerMoving;

	private float playerSpeed = 0.5f;
	private float rotationSpeed = 4f;

	private float jumpForce = 3f;
	private bool canJump;

	private float moveHorizontal, moveVertical;

	private float rotY;

	public Transform groundCheck;
	public LayerMask groundLayer;

	public GameObject damagePoint;

	void Awake()
    {
		myBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start () {
		rotY = transform.localRotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMoveKeyboard();
		AnimatePlayer();
		Attack();
		IsOnGround();
		Jump();
	}
	void FixedUpdate()
	{
		MoveandRotate();
	}
	void PlayerMoveKeyboard()
    {
		if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
        {
			moveHorizontal = -1;
        }
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
		{
			moveHorizontal = 0;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			moveHorizontal = 1;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) 
		{
			moveHorizontal = 0;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			moveVertical = 1;
		}
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
		{
			moveVertical = 0;
		}
	}
	void MoveandRotate()
    {
        if (moveVertical != 0)
        {
			myBody.MovePosition(transform.position + transform.forward * (moveVertical * playerSpeed));
        }
		rotY += moveHorizontal * rotationSpeed;
		myBody.rotation = Quaternion.Euler(0f,rotY, 0f);
    }
	void AnimatePlayer()
    {
        if (moveVertical != 0)
        {
            if (!isPlayerMoving)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_Animation))
                {
					isPlayerMoving = true;
					anim.SetTrigger(MyTags.RUN_Trigger);
                }
            }
        }
        else
        {
			if (isPlayerMoving)
			{
				if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_Animation))
				{
					isPlayerMoving = false;
					anim.SetTrigger(MyTags.STOP_Trigger);
				}
			}
		}
    }
	void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_Animation)|| !anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ATTACK_Animation))
            {
				anim.SetTrigger(MyTags.ATTACK_Animation);
            }
        }
    }
	void IsOnGround()
    {
		canJump = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);
    }
	void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
				canJump = false;
				myBody.MovePosition(transform.position + transform.up * (jumpForce * playerSpeed));

				anim.SetTrigger(MyTags.JUMP_Trigger);
            }
        }
    }
	void ActivateDamagePoint()
	{
		damagePoint.SetActive(true);
	}
	void DectivateDamagePoint()
	{
		damagePoint.SetActive(false);
	}
}
