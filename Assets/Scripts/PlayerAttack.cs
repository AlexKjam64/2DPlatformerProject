using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Objects")]
    public Animator animator;
    public Transform attackPoint;
    public AudioSource attackSound;
    public AudioSource dogDamageSound;
    public AudioSource birbDamageSound;
    public CharacterController2D charController;

    [Header("Stats")]
    public float attackRange;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (!charController.m_Walled && Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsJumping", false);
            Attack();
        }
    }

    public void Attack()
    {
        // We play attack animation and sound
        animator.SetTrigger("IsAttacking");
        attackSound.Play();
        // This will detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Will apply some sort of damage
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Birb")
            {
                birbDamageSound.Play();
                Destroy(enemy.gameObject);
                Debug.Log("Birb");
            }
            if (enemy.gameObject.tag == "Dog")
            {
                dogDamageSound.Play();
                Destroy(enemy.gameObject);
                Debug.Log("Dog");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
