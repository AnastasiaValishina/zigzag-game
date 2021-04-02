using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] Cube cubePrefab;
    public List<Cube> cubePool;
    public int cubeIndex = 0;

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
        cubePool = PregenerateCubePool(30);

        CreateCubeAt(0, 0);

        for (int i = 0; i < 5; i++)
        {
            CreateFiveCubes();
        }
    }

    List<Cube> PregenerateCubePool(int amountOfCubes)
    {
        for (int i = 0; i < amountOfCubes; i++) 
        {
            Cube cube = Instantiate(cubePrefab);
            cube.transform.parent = transform;
            cube.gameObject.SetActive(false);
            cubePool.Add(cube);
        }
        return cubePool;
    }

    public Cube[] CreateFiveCubes()
    {
        Cube[] cubeArray = new Cube[5];

        for (int i = 0; i < cubeArray.Length; i++)
        {
            Cube cube = CreateCubeInRandomDirection();
            cubeArray[i] = cube;
        }
        int randomIndex = Random.Range(0, cubeArray.Length);
        cubeArray[randomIndex].ActivateCollectable();

        return cubeArray;
    }


    Cube CreateCubeAt(float x, float z)
    {
        Cube cube = RequestCubeFromPool();
        cube.transform.position = new Vector3(x, 0f, z);
        cube.transform.parent = transform;
        cube.name = "(x" + x + ", z" + z + " )";
        cube.orderNumber = cubeIndex;
        cubeIndex++;
        if (cubeIndex == 5) { cubeIndex = 0; }       
        currentCube = cube;

        return cube;
    }

    Cube CreateCubeInRandomDirection()
    {
        int randomDirectionIndex = Random.Range(0, 2);
        Debug.Log("randomDirectionIndex " + randomDirectionIndex);
        if (randomDirectionIndex == 0)
        {
            Cube cube = CreateCubeAt(currentCube.transform.position.x + 1, currentCube.transform.position.z);
            return cube;
        }
        else if (randomDirectionIndex == 1)
        {
            Cube cube = CreateCubeAt(currentCube.transform.position.x, currentCube.transform.position.z + 1);
            return cube;
        }
        return null;
    }

    Cube RequestCubeFromPool()
    {
        foreach(var cube in cubePool)
        {
            if (cube.gameObject.activeInHierarchy == false)
            {
                cube.gameObject.SetActive(true);
                return cube;
            }
        }

        Cube newCube = Instantiate(cubePrefab);
        newCube.transform.parent = transform;
        newCube.gameObject.SetActive(true);
        cubePool.Add(newCube);

        return newCube;
    }
}
