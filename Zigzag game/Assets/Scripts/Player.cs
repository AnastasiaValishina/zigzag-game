using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    Vector3 direction;

    void Start()
    {
        direction = Vector3.zero;
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        if (Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (direction == Vector3.forward)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.forward;
        }
    }
}
