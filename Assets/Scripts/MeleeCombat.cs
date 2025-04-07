using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]

public class MeleeCombat : MonoBehaviour
{
    private Movement moveScript;
    private Stats stats;
    private Animator animator;
    
    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")] 
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime;
    void Start()
    {
        moveScript = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);

        targetEnemy = moveScript.targetEnemy;

        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= moveScript.stoppingDistance)
            {
                StartCoroutine(MeleeAttackInterval());
            }
        }
    }

    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackInterval);

        if (targetEnemy == null)
        {
            animator.SetBool("isAttacking", false);
            performMeleeAttack = true;
        }
    }

    private void MeleeAttack()
    {
        stats.TakeDamage(targetEnemy, stats.damage);

        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;
        
        animator.SetBool("isAttacking", false);
    }
    
    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        performMeleeAttack = true;
    }

}
