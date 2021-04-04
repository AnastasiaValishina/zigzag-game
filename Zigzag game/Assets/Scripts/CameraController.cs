using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.Instance.transform.position;
    }

    void Update()
    {
        if (Player.Instance.IsAlive())
        {
            transform.position = Player.Instance.transform.position + offset;
        }
    }
}
