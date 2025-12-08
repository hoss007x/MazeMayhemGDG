using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] float waitTimeOnWayPoint = 1f;
    [SerializeField] Path path;

    NavMeshAgent agent;
    Animator animator;

    float time = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.destination = path.GetCurrentWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
             time += Time.deltaTime;
            if (time >= waitTimeOnWayPoint)
            {
                time = 0f;
                agent.destination = path.GetNextWayPoint();
            }
        }    
    }






}
