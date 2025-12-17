using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI : MonoBehaviour , IDamage
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

    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;


    Color colorOrig;

    float shootTimer;
    float angleToPlayer;
    float roamTimer;
    float stoppingDistanceOrig;

    bool playerInRange;
    bool isPlayingSteps;

    Vector3 playerDir;
    Vector3 startingpos;
    Vector3 itemPOS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        colorOrig = model.material.color;

        startingpos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
    }


    // Update is called once per frame

    void Update()

    {
        shootTimer += Time.deltaTime;

        locomotion();

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

    void locomotion()
    {
        float agentSpeedCur = agent.velocity.normalized.magnitude;
        float agentSpeedAnim = anim.GetFloat("Speed");

        anim.SetFloat("Speed", Mathf.MoveTowards(agentSpeedAnim, agentSpeedCur, Time.deltaTime * animTranSpeed));
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
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, faceTargetSpeed * Time.deltaTime);
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
        anim.SetTrigger("Shoot");
       
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        agent.SetDestination(GameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            
            GameManager.instance.updateGameGoal(-1);
            if (dropItem != null)
            {
                itemPOS = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(dropItem, itemPOS, transform.rotation);
               
            }
            anim.SetTrigger("Dead");

            // You win condition
            Invoke("youWin", 2f);
            
        }
        else
        {
           
            StartCoroutine(flashRed());
        }
    }

    private void youWin()
    {

        GameManager.instance.youWin();
        Destroy(gameObject);
    }

    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }





}