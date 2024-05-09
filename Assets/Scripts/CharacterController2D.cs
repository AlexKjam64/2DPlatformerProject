using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Objects")]
	public AudioSource damageSound;
	public Animator animator;
	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;
	public GameObject LoseUI;

	[Header("Stats")]
	[SerializeField] private int health = 5;
	[SerializeField] private int numOfHearts = 5;
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_WallSpeed = 2f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] public bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

	[Header("Checks")]
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private LayerMask m_WhatIsWall;                            // A mask determining what is wall to the character
	[SerializeField] private Transform m_WallCheck;                             // A position marking where to check if the player is walled.


	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_WallRadius = .2f;		// Radius of the overlap circle to determine if grounded
	public bool m_Walled;				// Whether or not the player is grounded.
	private bool wallSlide;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;


	private bool isWallJumping;
	private float wallJumpingDirection;
	private float wallJumpingTime = 0.1f;
	private float wallJumpingCounter;
	private float wallJumpingDuration = 0.3f;
	private Vector2 wallJumpingPower = new Vector2(8f, 8f);

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;
	/*public UnityEvent OnWallEvent;*/

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if(OnLandEvent == null)
        {
			OnLandEvent = new UnityEvent();
        }
		/*if (OnWallEvent == null)
		{
			OnWallEvent = new UnityEvent();
		}*/
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		bool wasWalled = m_Walled;
		m_Grounded = false;
		m_Walled = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		Collider2D[] collidersG = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < collidersG.Length; i++)
		{
			if (collidersG[i].gameObject != gameObject)
			{
				m_Grounded = true;
                if (!wasGrounded)
                {
					OnLandEvent.Invoke();
                }
			}
		}

		Collider2D[] collidersW = Physics2D.OverlapCircleAll(m_WallCheck.position, k_WallRadius, m_WhatIsWall);
		for (int i = 0; i < collidersW.Length; i++)
		{
			if (collidersW[i].gameObject != gameObject) 
			{
				m_Walled = true;
                /*if (!wasWalled)
                {
                    OnWallEvent.Invoke();
                }*/
            }
		}
	}

	public void TakeDamage()
    {
		damageSound.Play();
		if (health == 1)
        {
			health = 0;
			LoseUI.SetActive(true);
			Time.timeScale = 0f;
		}
        else
        {
			health--;

		}
    }

	public void Heal()
    {
        if (health < 5)
        {
			health++;
			//Debug.Log("Healed by 1");
        }
        else
        {
			//Debug.Log("Max Health: " + health);
		}
    }

    public void Health()
    {
		int i;
		if (health > numOfHearts)
        {
			health = numOfHearts;
		}

		for(i = 0; i < hearts.Length; i++)
        {
			if(i < health)
            {
				hearts[i].sprite = fullHeart;
            }
            else
            {
				hearts[i].sprite = emptyHeart;
			}

			if(i < numOfHearts)
            {
				hearts[i].enabled = true;
            }
            else
            {
				hearts[i].enabled = false;
			}
        }
    }

	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

       if (m_Walled)
		{
			wallSlide = true;
			OnLandEvent.Invoke();
			animator.SetBool("IsWalled", true);
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y, -m_WallSpeed, float.MaxValue));
		}
		else
		{
			wallSlide = false;
			animator.SetBool("IsWalled", false);
		}

		WallJump();
	}

	private void WallJump()
	{
		if (wallSlide)
		{
			m_AirControl = true;
			wallJumpingDirection = -transform.localScale.x;
			wallJumpingCounter = wallJumpingTime;

			CancelInvoke(nameof(StopWallJumping));
		}
		else
		{
			m_AirControl = true;
			wallJumpingCounter -= Time.deltaTime;
		}

		if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
		{
			isWallJumping = true;
			m_Rigidbody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
			wallJumpingCounter = 0f;

			if (isWallJumping)
			{
				m_AirControl = false;
				Flip();
			}

			Invoke(nameof(StopWallJumping), wallJumpingDuration);
		}
	}

	private void StopWallJumping()
	{
		isWallJumping = false;
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}