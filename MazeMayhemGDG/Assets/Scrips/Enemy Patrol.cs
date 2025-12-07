using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float speed = 3f;
    public float detectionRange = 10f;
    public Transform Player;

    private int CurrentPatrolIndex = 0;

    private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            Patrol();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetpatrolpoint = patrolPoints[CurrentPatrolIndex];
        Vector3 direction = (targetpatrolpoint.position - transform.position).normalized;

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetpatrolpoint.position) < 0.1f)
        {
            CurrentPatrolIndex = (CurrentPatrolIndex + 1) % patrolPoints.Length;
        }

    }

    private void OnDrawGizmos()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.red;
            foreach (Transform point in patrolPoints)
            {
                if (point != null)
                    Gizmos.DrawSphere(point.position, 0.3f);
            }

            Gizmos.color = Color.green;
            for (int i = 0; i < patrolPoints.Length; i++)
            {

                if (patrolPoints[i] != null && patrolPoints[(i + 1) % patrolPoints.Length] != null)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[(i + 1) % patrolPoints.Length].position);
                }
            }

        }

        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);


    }
}
