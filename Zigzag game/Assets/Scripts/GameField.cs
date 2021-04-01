using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] Cube cubePrefab;

    Cube currentCube;
    static GameField instance;

    public static GameField Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameField>();
            }
            return instance;
        }
    }

    void Start()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                InstantiateCube(x, z);
            }
        }
        for (int i = 0; i < 20; i++)
        {
            CreateCubeInRandomDirection();
        }
    }

    void Update()
    {

    }

    void InstantiateCube(float x, float z)
    {
        Vector3 cubePosition = new Vector3(x, 0f, z);
        Cube cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        cube.transform.parent = transform;
        cube.name = "(x" + x + ", z" + z + " )";
        currentCube = cube;
     //   currentCube.SetPositon(x, z);
    }

    public void CreateCubeInRandomDirection()
    {
        int randomDirectionIndex = Random.Range(0, 2);
        switch (randomDirectionIndex)
        {
            case 0: InstantiateCube(currentCube.transform.position.x + 1, currentCube.transform.position.z);
                break;
            case 1: InstantiateCube(currentCube.transform.position.x, currentCube.transform.position.z + 1);
                break;
        }
    }
}
