using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    Vector3 direction;
    bool isAlive = true;

    void Start()
    {
        direction = Vector3.zero;
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && isAlive)
        {
            ChangeDirection();
        }
     /*   if (Input.GetMouseButtonDown(0) && !isAlive)
        {
            RestartGame();
        }*/
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cube")
        {
            RaycastHit raycastHit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (!Physics.Raycast(ray, out raycastHit))
            {
                isAlive = false;
                if (transform.GetChild(0))
                {
                    transform.GetChild(0).transform.parent = null;
                }
            }
        }
    }
}
