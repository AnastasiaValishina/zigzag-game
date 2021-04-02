using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] Cube cubePrefab;
    
    public List<Cube> cubePool;
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

        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                CreateCubeAt(x, z);
            }
        }
        for (int i = 0; i < 20; i++)
        {
            CreateCubeInRandomDirection();
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


    void CreateCubeAt(float x, float z)
    {
        Cube cube = RequestCubeFromPool();
        cube.transform.position = new Vector3(x, 0f, z);
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
            case 0: CreateCubeAt(currentCube.transform.position.x + 1, currentCube.transform.position.z);
                break;
            case 1: CreateCubeAt(currentCube.transform.position.x, currentCube.transform.position.z + 1);
                break;
        }
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
        cubePool.Add(newCube);

        return newCube;
    }
}
