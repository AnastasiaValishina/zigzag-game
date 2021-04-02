using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    public bool isAlive = true;

    Vector3 direction;
    CollectablesController collectablesController;

    void Start()
    {
        direction = Vector3.zero;
        collectablesController = FindObjectOfType<CollectablesController>();
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && isAlive)
        {
            ChangeDirection();
        }
        if (Input.GetMouseButtonDown(0) && !isAlive)
        {
            SceneManager.LoadScene(0);
        }
        if (transform.position.y < 0)
        {
            isAlive = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            other.gameObject.SetActive(false);
            collectablesController.AddCollectable();
        }
    }    
}
