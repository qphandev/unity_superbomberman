using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController currentPlayer;

    private int lives = 3;
    private int score = 0;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private float delayToSpawnPlayer = 1f;

    [SerializeField] private CameraController myCamera;

    [SerializeField] private Text myScore;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Method to set score (on enemy kill).
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log("IncrementScore " + score);
        myScore.text = "SCORE: " + score.ToString("D4");
    }

    public void PlayerDied()
    {
        // If player still has more than 0 lives left, take away their life counter and spawn a new player object
        if (lives > 0)
        {
            --lives;
            Debug.Log("Player has died. Remaining lives: " + lives);
            Invoke("SpawnPlayer", currentPlayer.GetDestroyDelayTime() + delayToSpawnPlayer);
        }
        else
        {
            Debug.Log("GameManager: No more lives; game over");
        }
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(6, 0, 5), Quaternion.identity, playerParentTransform);
        currentPlayer = player.GetComponent<PlayerController>();
        myCamera.SetPlayer(player);
    }
}
