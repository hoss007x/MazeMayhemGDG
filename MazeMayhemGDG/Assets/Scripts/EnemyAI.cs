using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour , IDamage
{
    [SerializeField] int HP;
    [SerializeField] int FOV;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;

    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;
    [SerializeField] Transform shootPos;

    [SerializeField] GameObject dropItem;


    Color colorOrig;

    float shootTimer;
    float angleToPlayer;
    float roamTimer;
    float stoppingDistanceOrig;

    bool playerInRange;

    Vector3 playerDir;
    Vector3 startingpos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {
        colorOrig = model.material.color;
        GameManager.instance.updateGameGoal(1);
        startingpos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
    }


    // Update is called once per frame

    void Update()

    {
        shootTimer += Time.deltaTime;

        if (agent.remainingDistance < 0.01f)
            roamTimer += Time.deltaTime;

        if (playerInRange && canSeePlayer())
        {
            checkRoam();
        }
        else if (!playerInRange)
        {
            checkRoam();
        }
    }
    void checkRoam()
    {
        if (agent.remainingDistance < 0.01f && roamTimer >= roamPauseTime)
        {
            roam();
        }
    }

    void roam()
    {
        roamTimer = 0;
        agent.stoppingDistance = 0;

        Vector3 ranPos = Random.insideUnitSphere * roamDist;
        ranPos += startingpos;

        NavMeshHit navHit;
        NavMesh.SamplePosition(ranPos, out navHit, roamDist, 1);
        agent.SetDestination(navHit.position);
    }


    bool canSeePlayer()
    {
        playerDir = GameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);
            
        Debug.DrawRay(headPos.position, playerDir);
            
        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            Debug.Log(hit.collider.name);
                
            if (angleToPlayer <= FOV && hit.collider.CompareTag("Player"))
            {
                agent.SetDestination(GameManager.instance.player.transform.position);
                    
                if (shootTimer >= shootRate)
                {
                    shoot();
                }
                    
                if (agent.remainingDistance <= stoppingDistanceOrig)
                    faceTarget();

                agent.stoppingDistance = stoppingDistanceOrig;
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, faceTargetSpeed*Time.deltaTime);
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
            agent.stoppingDistance = 0;
        }
    }

    void shoot()
    {
        shootTimer = 0;
        Instantiate(bullet, shootPos.position, transform.rotation);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        agent.SetDestination(GameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            GameManager.instance.updateGameGoal(-1);
            if(dropItem != null)
                Instantiate(dropItem, transform.position, transform.rotation);

            Destroy(gameObject);
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
