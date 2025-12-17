using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI : MonoBehaviour , IDamage
{
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;
    float roamTimer;
    bool playerInRange;

    [SerializeField] Renderer model;

    Vector3 startingpos;

    Color colorOrig;

    public enum BossState
    {
        Idle,
        Chasing,
        Attacking,
        Enraged,
        Dead
    }

    [Header("References")]
    public Transform Player;
    public NavMeshAgent Agent;
    public Animator animator;

    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Detection Ranges")]
    public float detectionRange = 30f;
    public float attackRange = 5f;

    [Header("Attack")]
    public float meleeDamage = 10;
    public float attackCooldown = 2f;

    [Header("Enrage Phase")]
    public float enrageHealthThreshold = 50;
    public float enrageSpeedMultiplier = 1.5f;
    public float enrageAttackMultiplier = 1.5f;

    private BossState currentState;
    private float attackTimer;
    private bool isEnraged;

    void checkRoam()
    {
        if (Agent.remainingDistance < 0.01f && roamTimer >= roamPauseTime)
        {
            roam();
        }
    }

    void locomotion()
    {
        float agentSpeedCur = Agent.velocity.normalized.magnitude;
        float agentSpeedAnim = animator.GetFloat("Speed");

    }

    void roam()
    {
        roamTimer = 0;
        Agent.stoppingDistance = 0;

        Vector3 ranPos = Random.insideUnitSphere * roamDist;
        ranPos += startingpos;

        NavMeshHit navHit;
        NavMesh.SamplePosition(ranPos, out navHit, roamDist, 1);
        Agent.SetDestination(navHit.position);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Idle;
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == BossState.Dead)
            return;

        attackTimer -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        switch (currentState)
        {
            case BossState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = BossState.Chasing;
                }
                break;

            case BossState.Chasing:
                Agent.SetDestination(Player.position);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = BossState.Attacking;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = BossState.Idle;
                }
                break;

            case BossState.Attacking:
                Agent.ResetPath();
                transform.LookAt(Player);

                if (distanceToPlayer > attackRange)
                {
                    currentState = BossState.Chasing;
                    return;
                }
                if (attackTimer <= 0)
                {
                    AttackPlayer();
                    attackTimer = attackCooldown;
                }
                break;

            case BossState.Enraged:
                Agent.SetDestination(Player.position);

                if (distanceToPlayer <= attackRange && attackTimer <= 0)
                {
                    AttackPlayer();
                    attackTimer = attackCooldown / 2;
                }
                break;
        }

        CheckEnrage();

        UpdateAnimations();

    }


    void ChangeState(BossState newState)
    {
               currentState = newState;

        if (animator != null)
        {
            animator.SetInteger("State", (int)newState);
        }
    }
 
    void AttackPlayer()
    {
        // Implement attack logic here (e.g., reduce player health)
        Debug.Log("Boss attacks the player for " + meleeDamage + " damage.");

        if (Random.value > 0.5f)
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Implement melee attack logic here
        Debug.Log("Boss performs a melee attack.");
    }

    void CheckEnrage()
    {
        if (!isEnraged && currentHealth <= enrageHealthThreshold)
        {
            isEnraged = true;
            Agent.speed *= enrageSpeedMultiplier;
            ChangeState(BossState.Enraged);
        }
    }

    void Die()
    {
        currentState = BossState.Dead;
        Agent.isStopped = true;

        if (animator != null)
            animator.SetTrigger("Die");

        GameManager.instance.updateGameGoal(-1);
        Destroy(gameObject, 5f);
    }

    void UpdateAnimations()
    {
        if (animator == null)
            return;

        float speed = Agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        bool attacking = currentState == BossState.Attacking || currentState == BossState.Enraged;
        animator.SetBool("IsAttacking", attacking);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Agent.stoppingDistance = 0;
        }
    }
    public void TakeDamage(int amount)
    {
       currentHealth -= amount;
        Agent.SetDestination(GameManager.instance.player.transform.position);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Die();
        }
        else
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

}