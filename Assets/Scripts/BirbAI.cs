using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class BirbAI : MonoBehaviour
{
    /*[Header("Objects")]
    public Transform target;
    public Transform theBirb;
    public CharacterMovement2D movement;
    public CharacterController2D controller;

    [Header("Stats")]
    public float speed;
    public float nextWaypointDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void Update()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f){
            theBirb.transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (force.x <= -0.01f)
        {
            theBirb.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement.KBCounter = movement.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
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
    }*/
}
