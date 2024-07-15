using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeedX = 4;
    [SerializeField] float moveSpeedY;
    [SerializeField] float moveSpeedZ = 4;

    // Time it takes for player to destroy gameObject when dies
    [SerializeField] private float destroyTime = 2f;

    [SerializeField] private int maxBombs = 2;
    private int currentBombsPlaced = 0;

    [SerializeField] GameObject bombPrefab;

    // Caching: You want to have a global reference because apparently this is an expensive declaration
    Rigidbody myRigidBody;

    private bool hasControl = true;

    private GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasControl)
        {
            Movement();
            PlaceBomb();
        }
    }

    public float GetDestroyDelayTime()
    {
        return destroyTime;
    }

    // Movement
    private void Movement()
    {
        float deltaTime = Time.deltaTime;

        Vector3 newVelocity = new Vector3();

        // WASD Controllers
        bool hasPressedWKey = Input.GetKey(KeyCode.W);
        bool hasPressedSKey = Input.GetKey(KeyCode.S);
        bool hasPressedDKey = Input.GetKey(KeyCode.D);
        bool hasPressedAKey = Input.GetKey(KeyCode.A);

        if (hasPressedWKey)
        {
            newVelocity += new Vector3(0f, 0f, moveSpeedZ);
        }
        if (hasPressedSKey)
        {
            newVelocity += new Vector3(0f, 0f, -moveSpeedZ);
        }
        if (hasPressedDKey)
        {
            newVelocity += new Vector3(moveSpeedX, 0f, 0f);
        }
        if (hasPressedAKey)
        {
            newVelocity += new Vector3(-moveSpeedX, 0f, 0f);
        }

        myRigidBody.velocity = newVelocity;
    }

    // Place Bomb
    private void PlaceBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (currentBombsPlaced < maxBombs))
        {
            Debug.Log("Bomb Placed");
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bomb.transform.position = new Vector3(Mathf.Round(transform.position.x), 0f, Mathf.Round(transform.position.z));
            ++currentBombsPlaced;
        }
    }

    public void Die()
    {
        // Tell player manager that player died
        // Play die animation
        // Remove player from scene

        // Take away control once player dies
        hasControl = false;

        // Destroy itself after delay
        Destroy(gameObject, destroyTime);

        myGameManager.PlayerDied();
        Debug.Log("PlayerController: Player died");
    }


    public void bombExploded()
    {
        --currentBombsPlaced;
    }
}
