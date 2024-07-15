using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController currentPlayer;

    private int lives = 3;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private float delayToSpawnPlayer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    public void PlayerDied()
    {
        // If player still has more than 0 lives left, take away their life counter and spawn a new player object
        if (lives > 0)
        {
            --lives;
            Debug.Log("Player has died. Remaining lives: " + lives);
            Invoke("SpawnPlayer", currentPlayer.GetDestroyDelayTime() + delayToSpawnPlayer);
        } else
        {
            Debug.Log("GameManager: No more lives; game over");
        }
    }

    private void SpawnPlayer()
    {
        Debug.Log("SpawnPlayer called");
        GameObject player = Instantiate(playerPrefab, new Vector3(6, 0, 5), Quaternion.identity, playerParentTransform);
        currentPlayer = player.GetComponent<PlayerController>();
    }
}