using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public enum Difficulty
    {
        easy,
        normal,
        hard
    }

    public Difficulty currentDifficulty;

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

    private void Reset()
    {
        cubePool.Clear();
        SetStartingCubes();
    }

    private void SetStartingCubes()
    {
        cubePool = PregenerateCubePool(30);

        Cube startingPlatform = CreateCubeAt(-2, -1);
        startingPlatform.gameObject.SetActive(true);
        startingPlatform.transform.localScale = new Vector3(3, 3, 3);

        CreateCubeAt(0, 0);

        /*   for (int i = 0; i < 6; i++)
           {
               CreateFiveCubes();
           }*/
        GenerateFiveOfFive();
    }

    void Start()
    {

        SetStartingCubes();

        Player.onGameOver += Reset;
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




      //  int randomIndex = Random.Range(0, cubeArray.Length);
      //  cubeArray[randomIndex].ActivateCollectable();

        return cubeArray;
    }

    public void PutCristalsInSequence(int i)
    {
        int index = 0;
        Cube[] cubeArray = CreateFiveCubes();
        switch (i)
        {
            case 0: cubeArray[0].ActivateCollectable();
                index++;
                break;
            case 1: cubeArray[1].ActivateCollectable();
                index++;
                break;
            case 2: cubeArray[2].ActivateCollectable();
                index++;
                break;
            case 3: cubeArray[3].ActivateCollectable();
                index++;
                break;
            case 4: cubeArray[4].ActivateCollectable();
                index++;
                break;
        }
    }

    public void GenerateFiveOfFive()
    {
        for (int i = 0; i < 5; i++)
        {
            PutCristalsInSequence(i);
        }
    }


    Cube CreateCubeAt(float x, float z)
    {
        Cube cube = CreateCube(x, z);
 
        currentCube = cube;

        return cube;
    }

    public Cube CreateCube(float x, float z)
    {
        Cube cube = RequestCubeFromPool();
        cube.transform.position = new Vector3(x, 0f, z);
        cube.transform.parent = transform;
        cube.name = "(x" + x + ", z" + z + " )";
        cube.orderNumber = cubeIndex;
        cubeIndex++;
        if (cubeIndex == 5) { cubeIndex = 0; }

        return cube;
    }

    Cube CreateCubeInRandomDirection()
    {
        int randomDirectionIndex = Random.Range(0, 2);
        if (randomDirectionIndex == 0)
        {
            Cube cube = CreateCubeAt(currentCube.transform.position.x + 1, currentCube.transform.position.z);
            if (currentDifficulty == Difficulty.normal)
            {
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
            }
            
            if (currentDifficulty == Difficulty.hard)
            {
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 2);
            }
            return cube;
        }
        else if (randomDirectionIndex == 1)
        {
            Cube cube = CreateCubeAt(currentCube.transform.position.x, currentCube.transform.position.z + 1);
            if (currentDifficulty == Difficulty.normal)
            {
                CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
            }
            if (currentDifficulty == Difficulty.hard)
            {
                CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
                CreateCube(currentCube.transform.position.x - 2, currentCube.transform.position.z);
            }
            else
            {
                return cube;
            }
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
