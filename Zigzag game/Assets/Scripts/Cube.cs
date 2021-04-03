using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float fallingDelay = 1f;
    [SerializeField] GameObject collectable;
    public int orderNumber;

    private void Start()
    {
        Player.onGameOver += Reset;
    }

    private void Reset()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.localScale = new Vector3(1f, 3f, 1f);
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (orderNumber == 0)
            {
                GameField.Instance.GenerateFiveOfFive();

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
        transform.localScale = new Vector3(1f, 3f, 1f);
        gameObject.SetActive(false);
    }

    public void ActivateCollectable()
    {
        collectable.SetActive(true);
    }
}
