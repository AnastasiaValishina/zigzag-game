using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float fallingDelay = 1f;
    [SerializeField] GameObject collectable;

    public enum CubeType
    {
        trail,   // "ведущий" куб, относительно которого генерируется дорожка
        adjacent // соседний куб генерируется вместе в ведущим для сохранения ширины дорожки
    }
    public CubeType cubeType;

    void Update()
    {
        if (Player.Instance.transform.position.x > transform.position.x &&
            Player.Instance.transform.position.z > transform.position.z)
        {
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallingDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(fallingDelay);
        CreateNewCube();
        RecycleCube();
    }

    private void CreateNewCube()             // создать новый куб вместо упавшего
    {
        if (cubeType == CubeType.trail)
        {
            GameField.Instance.CreateCubeInRandomDirection();
        }
    }

    public void RecycleCube() // вернуть куб в пул кубов для повторного использования
    {
        GetComponent<Rigidbody>().isKinematic = true;
        cubeType = CubeType.trail;
        collectable.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ActivateCollectable()
    {
        collectable.SetActive(true);
    }
}
