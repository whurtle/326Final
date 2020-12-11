using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] wayPoints;

    private int waypointIndex;

    public LayerMask groundLayer;

    public UnityEngine.AI.NavMeshAgent enemyAgent;

    public Transform playerCharacter;

    public Transform enemyModel;

    private bool spotted = false;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        enemyAgent.updateRotation = false;

        NextWaypoint();
    }

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spotted)
        {
            var playerLocation = GetPlayerLocation();
            enemyAgent.SetDestination(playerLocation);
            LookTowards(playerCharacter);
        }
        else if (ReachedDestination())
        {
            NextWaypoint();
        }
    }

    private bool ReachedDestination()
    {
        // Check if we've reached the destination
        if (!enemyAgent.pathPending)
        {
            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void NextWaypoint()
    {
        enemyAgent.SetDestination(wayPoints[waypointIndex].position);
        LookTowards(wayPoints[waypointIndex]);
        waypointIndex = (waypointIndex + 1) % wayPoints.Length;
    }

    private Vector3 GetPlayerLocation()
    {
        return playerCharacter.position;
    }

    private void LookTowards(Transform target)
    {
        // fast rotation
        float rotSpeed = 1f;

        // distance between target and the actual rotating object
        Vector3 D = target.position - transform.position;

        
        // calculate the Quaternion for the rotation
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed);

        //Apply the rotation 
        transform.rotation = rot;

        // put 0 on the axys you do not want for the rotation object to rotate
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Debug.Log("I see you");
            this.spotted = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            this.spotted = false;
        }
    }
}
