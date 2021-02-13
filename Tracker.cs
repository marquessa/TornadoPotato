//https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment 
// Feb 2 2021
// MMackenzie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Tracker : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    //public GameOjbect player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float sightRange, attackRange;
    public bool playerInSightRange, playInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("potato").transform;
        //player = FindGameObject("OBJ").transform;
        //player = GameObject.Find("/potato").transform;
        //player = GetObject<OBJ>();
        //player = GetTag<OBJ>();
        //player = GetObject<potato>();
        //player = GameTag.Find("OBJ").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && !playInAttackRange) Patroling();
        if (playerInSightRange && !playInAttackRange) ChasePlayer();
        //if (playerInSightRange && playInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SeachWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distancetoWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distancetoWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SeachWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    

}
