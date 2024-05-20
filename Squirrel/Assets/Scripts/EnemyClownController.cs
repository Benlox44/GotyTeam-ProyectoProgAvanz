using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownController : StateMachineBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    [SerializeField] private float attackRange;
    //[SerializeField] private float attackCooldown;
    //private float lastAttackTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       target = FindObjectOfType<PlayerController>().transform;
       rb = animator.GetComponent<Rigidbody2D>();
       //Nueva implementancion
       //lastAttackTime = -attackCooldown;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Extender
        if (Vector2.Distance(target.position, rb.position) <= attackRange) {
            animator.SetBool("isAttacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("isAttacking", false);
    }
}
