using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float fallingDelay = 1f;
    [SerializeField] GameObject collectable;

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
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallingDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(fallingDelay);
        gameObject.SetActive(false);
    }

    public void ActivateCollectable()
    {
        collectable.SetActive(true);
    }
}
