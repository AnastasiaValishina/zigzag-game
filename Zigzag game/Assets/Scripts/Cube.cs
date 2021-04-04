using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float fallingDelay = 1f;
    [SerializeField] GameObject collectable;
    [SerializeField] float offset = 2f;

    private void Start()
    {
        Player.onGameOver += RecycleCube;
    }

    void Update()
    {
        if (Player.Instance.transform.position.x > transform.position.x &&
            Player.Instance.transform.position.z > transform.position.z)
        {
            StartCoroutine(FallDown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
   //         GameField.Instance.CreateCubeInRandomDirection();
    //        GameField.Instance.ActivateCollectablesRandomly();
           //GameField.Instance.ActivateCollectablesInOrder();
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallingDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(fallingDelay);
        RecycleCube();
    }

    private void RecycleCube()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GameField.Instance.CreateCubeInRandomDirection(); // создать новый куб
        gameObject.SetActive(false);
    }

    public void ActivateCollectable()
    {
        collectable.SetActive(true);
    }
}
