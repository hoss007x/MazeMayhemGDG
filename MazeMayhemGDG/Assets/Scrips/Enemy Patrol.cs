using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int TargetPoint;
    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == patrolPoints[TargetPoint].position)
        {
           increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[TargetPoint].position, speed * Time.deltaTime);

    }

    void increaseTargetInt()
    {
        TargetPoint++;
        if(TargetPoint >= patrolPoints.Length)
        {
            TargetPoint = 0;
        }
    }

}
