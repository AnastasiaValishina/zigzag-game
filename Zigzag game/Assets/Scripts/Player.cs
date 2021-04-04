using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    
    bool isAlive = true;
    Vector3 direction;
    CollectablesController collectablesController;

    public delegate void GameOver();
    public static event GameOver onGameOver;

    static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    void Start()
    {
        direction = Vector3.zero;
        collectablesController = FindObjectOfType<CollectablesController>();
        UIController.onStartGame += SetPlayerActive;
    }

    void Update()
    {
        if (UIController.Instance.gameStarted)
        {
            Move();
        }

        if (transform.position.y < 1)
        {
            isAlive = false;
            UIController.Instance.ShowGameOverScreen();
        }
    }

    private void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && isAlive)
        {
            ChangeDirection();
        }
        if (Input.GetMouseButtonDown(0) && !isAlive)
        {
            if (onGameOver != null)
            {
                onGameOver();
                direction = Vector3.zero;
                transform.position = new Vector3(0f, 1.75f, 0f);
                isAlive = true;
            }
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

    public bool IsAlive()
    {
        return isAlive;
    }

    void SetPlayerActive()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
