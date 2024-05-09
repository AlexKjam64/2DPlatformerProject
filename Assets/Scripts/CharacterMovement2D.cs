using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
	[Header("Objects")]
	public CharacterController2D controller;
	public Animator animator;
	public Rigidbody2D rb;

	[Header("Stats")]
	public float runSpeed;

	[Header("Knockback")]
	public float KBForce;
	public float KBCounter;
	public float KBTotalTime;
	public bool KBFromRight;

	float horizontalMove = 0f;
	bool jump = false;

	// Update is called once per frame
	void Update()
	{
		controller.Health();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}
	}

	public void OnLanding()
    {
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate()
	{
		if (KBCounter <= 0)
        {
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
			jump = false;
        }
        else
        {
			if(KBFromRight == true)
            {
				rb.velocity = new Vector2(-KBForce, KBForce);
			}
			if(KBFromRight == false)
            {
				rb.velocity = new Vector2(KBForce, KBForce);
			}

			KBCounter -= Time.deltaTime;
        }
	}
}
