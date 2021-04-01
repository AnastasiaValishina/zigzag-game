using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float xPos;
    public float zPos;
    void Start()
    {
        xPos = transform.position.x;
        zPos = transform.position.z;
    }

    public void SetPositon(float x, float z)
    {
        xPos = x;
        zPos = z;
    }

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameField.Instance.CreateCubeInRandomDirection();
        }
    }
}
