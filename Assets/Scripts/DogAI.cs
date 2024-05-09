using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI : MonoBehaviour
{
    [Header("Objects")]
    public GameObject aPoint;
    public GameObject bPoint;
    public CharacterMovement2D movement;
    public CharacterController2D controller;

    [Header("Stats")]
    public float runSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = bPoint.transform;
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == bPoint.transform)
        {
            rb.velocity = new Vector2(runSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-runSpeed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == bPoint.transform)
        {
            Flip();
            currentPoint = aPoint.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == aPoint.transform)
        {
            Flip();
            currentPoint = bPoint.transform;
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(aPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(bPoint.transform.position, 0.5f);
        Gizmos.DrawLine(aPoint.transform.position, bPoint.transform.position);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement.KBCounter = movement.KBTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                movement.KBFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                movement.KBFromRight = false;
            }
            controller.TakeDamage();
            Debug.Log("Hit Player!");
        }
    }
}
