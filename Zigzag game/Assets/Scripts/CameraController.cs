using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Player player;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (player.isAlive)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
