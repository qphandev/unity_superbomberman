using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Rigidbody myRigidBody;
    private Vector3 explodeDirection = Vector3.right;
    private float explodeSpeed = 200f;
    private float explodeRange = 5f;

    private Vector3 spawnPoint;

    private Transform explosionParentTransform;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        myRigidBody.velocity = explodeDirection * explodeSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, spawnPoint) >= explodeRange)
        {
            Debug.Log("This is explodeRange: " + explodeRange);
            Destroy(gameObject);
        }
    }
    
    // this is public so we can access it from other scripts
    public void SetExplosion(Vector3 direction, float speed, float range)
    {
        explodeDirection = direction;
        explodeSpeed = speed;
        explodeRange = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Disperse if collided with wall
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.tag == "Bomb")
        {
            other.GetComponent<Bomb>().Explode();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().Die();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Die();
        }
        else if (other.gameObject.tag == "DestructibleWall")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
