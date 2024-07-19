using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField, Range(1, 1000)] private int scoreIncrement = 1; 

    Rigidbody myRigidBody;

    private bool isMoving = true;
    private bool movingForward = true;
    private int waypointDestination = 0;

    [SerializeField] private float minDelayTime = 0.25f;
    [SerializeField] private float maxDelayTime = 3f;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

        if (target.Length == 0)
        {
            isMoving = false;
            Debug.LogError("Enemy " + gameObject.name + " has no waypoints!");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            myRigidBody.MovePosition(Vector3.MoveTowards(transform.position, target[waypointDestination].position, Time.deltaTime * moveSpeed));
            if (Vector3.Distance(transform.position, target[waypointDestination].position) < 0.1f)
            {
                isMoving = false;
                if (movingForward)
                {
                    if (waypointDestination >= target.Length - 1)
                    {
                        movingForward = false;
                        Invoke("GoBackwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
                    }
                    else
                    {
                        Invoke("GoForwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
                    }
                }
                else
                {
                    if (waypointDestination <= 0)
                    {
                        movingForward = true;
                        Invoke("GoForwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
                    }
                    else
                    {
                        Invoke("GoBackwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
                    }
                }
            }
        }
    }

    private void GoForwardWaypoint()
    {
        ++waypointDestination;
        isMoving = true;
    }
    private void GoBackwardWaypoint()
    {
        --waypointDestination;
        isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bomb")
        {
            isMoving = false;
            Debug.Log("ENEMY CONTROLLER: Enemy has collided with bomb");
            if(movingForward)
            {
                Invoke("GoBackwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
            } else
            {
                Invoke("GoForwardWaypoint", Random.Range(minDelayTime, maxDelayTime));
            }
            movingForward = !movingForward;

        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ENEMY CONTROLLER: Enemy has collided with player");
            collision.gameObject.GetComponent<PlayerController>().Die();
            isMoving = false;
        }
    }

    public void Die()
    {
        // access gamemanager to update the score when enemy dies
        FindObjectOfType<GameManager>().UpdateScore(scoreIncrement);
        Destroy(gameObject);
    }
}
