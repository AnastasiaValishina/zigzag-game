using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float fallingDelay = 1f;
    [SerializeField] GameObject collectable;
    public int orderNumber;

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (orderNumber == 0)
            {
                GameField.Instance.CreateFiveCubes();
            }
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallingDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(fallingDelay);
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.SetActive(false);
    }

    public void ActivateCollectable()
    {
        collectable.SetActive(true);
    }
}
