using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PlayerController player;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explodeRange = 5f;
    [SerializeField] private float explodeDelay = 2f;
    [SerializeField] private float explodeSpeed = 200f;
    private float explosionTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        InvokeRepeating("CountDownBombTimer", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer += Time.deltaTime;
        if (explosionTimer > explodeDelay)
        {
            Explode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has exited the trigger");
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    /*    private void CountDownBombTimer()
        {

            if (bombTickingTimer < bombDelayTime)
            {
                Debug.Log("CountDownBombTimer: tick");
                ++bombTickingTimer;
            } else
            {
                Debug.Log("CountDownBombTimer: BOOM!");
                Destroy(gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
        }*/

    /// <summary>
    /// When bomb explodes, send particle to all four directions.
    /// Decrease bomb limit counter.
    /// </summary>
    public void Explode()
    {
        GameObject explosionLeft = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionLeft.GetComponent<Explosion>().SetExplosion(Vector3.left, explodeSpeed, explodeRange);

        GameObject explosionRight = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionRight.GetComponent<Explosion>().SetExplosion(Vector3.right, explodeSpeed, explodeRange);

        GameObject explosionForward = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionForward.GetComponent<Explosion>().SetExplosion(Vector3.forward, explodeSpeed, explodeRange);

        GameObject explosionBackward = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionBackward.GetComponent<Explosion>().SetExplosion(Vector3.back, explodeSpeed, explodeRange);
        Destroy(gameObject);

        // Decreases bomb limit counter
        player.bombExploded();
    }
}
