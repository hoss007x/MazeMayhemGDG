using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
  NavMeshAgent agent;

  public Transform[] Waypoints;

   int WaypointIndex;

   Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     agent = GetComponent<NavMeshAgent>();
      UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = Waypoints[WaypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        WaypointIndex++;
        if (WaypointIndex >= Waypoints.Length)
        {
            WaypointIndex = 0;
        }
    }

}
