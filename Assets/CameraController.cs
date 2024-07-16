using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] Camera _camera;
    [SerializeField] Vector3 offset;
    private Transform playerTransform;

    private void LateUpdate()
    {
        if (playerTransform != null) transform.position = playerTransform.position + offset;
        
    }

    public void SetPlayer(GameObject player)
    {
        playerTransform = player.transform;
    }
}
