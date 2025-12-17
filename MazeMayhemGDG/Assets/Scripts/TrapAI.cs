using UnityEngine;
using UnityEngine.AI;

public class TrapAI : MonoBehaviour
{
    [SerializeField] int FOV;


    [SerializeField] float shootRate;


    [SerializeField] GameObject dart;
    [SerializeField] Transform trapPos;
    [SerializeField] Transform shootPos;

    bool playerInRange;

    float angleToPlayer;
    float shootTimer;

    Vector3 playerDir;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (playerInRange && canSeePlayer()) { }
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
        }
    }
    bool canSeePlayer()
    {
        playerDir = GameManager.instance.player.transform.position - trapPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        Debug.DrawRay(trapPos.position, playerDir);

        RaycastHit hit;
        if(Physics.Raycast(trapPos.position, playerDir, out hit))
        {
            Debug.Log(hit.collider.name);

            if(angleToPlayer <= FOV && hit.collider.CompareTag("Player"))
            {
               if(shootTimer >= shootRate)
                {
                    shoot();
                }
               return true;
            }
        }
        return false; 
    }
    void shoot()
    {
        shootTimer = 0;
        Instantiate(dart, shootPos.position, transform.rotation);
    }
}
