using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
 /*   public enum Difficulty
    {
        easy,
        normal,
        hard
    }
    public  Difficulty currentDifficulty;*/

    public enum CollectableGeneration
    {
        random,
        inOrder
    }
    [SerializeField] CollectableGeneration collectablesGeneration;

    [SerializeField] Cube cubePrefab;
    
    public List<Cube> cubePool;
    public List<Cube> collectablesPool;
    int orderIndex = 0;
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
    //    SetStartingCubes();
        Player.onGameOver += ResetGame;
        UIController.onStartGame += SetStartingCubes;

    }

    private void SetStartingCubes()
    {
        cubePool = PregenerateCubePool(30);

        // Создать стартовую платформу
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Cube cube = CreateCube(x, z);
                currentCube = cube;
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

    void ActivateCollectables()
    {
        switch (collectablesGeneration)
        {
            case CollectableGeneration.random: ActivateCollectablesRandomly();
                break;
            case CollectableGeneration.inOrder: ActivateCollectablesInOrder();
                break;
        }
    }
    void ActivateCollectablesRandomly()
    {
        if (collectablesPool.Count == 5)
        {
            int randomIndex = Random.Range(0, collectablesPool.Count);
            collectablesPool[randomIndex].ActivateCollectable();
        }
        
        if (collectablesPool.Count > 5)
        {
            collectablesPool.Clear();
        }
    }

    void ActivateCollectablesInOrder()
    {
        if (collectablesPool.Count == 25)
        {
            foreach (Cube cube in collectablesPool)
            {
                if (collectablesPool.IndexOf(cube) % 6 == 0)
                {
                    collectablesPool[orderIndex].ActivateCollectable();
                }
            }
        }
        if (collectablesPool.Count > 25)
        {
            collectablesPool.Clear();
            orderIndex = 0;
        }
    }
 
 /*   Cube CreateCurrentCubeAt(float x, float z)
    {
        Cube cube = CreateCube(x, z);
 
        currentCube = cube;

        return cube;
    }*/

    public Cube CreateCube(float x, float z)
    {
        Cube cube = RequestCubeFromPool();
        cube.transform.position = new Vector3(x, 0f, z);
        cube.transform.parent = transform;
        cube.name = "(x" + x + ", z" + z + " )";
        collectablesPool.Add(cube);
//        Debug.Log(collectablesPool.Count);

        ActivateCollectables();

        return cube;
    }

    public Cube CreateCubeInRandomDirection()
    {
        int randomDirectionIndex = Random.Range(0, 2);
        if (randomDirectionIndex == 0)
        {
            Cube cube = CreateCube(currentCube.transform.position.x + 1, currentCube.transform.position.z);
            currentCube = cube;

            if (UIController.Instance.isNormal)
            {
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
            }
            
            if (UIController.Instance.isEasy)
            {
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
                CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 2);
            }
            return cube;
        }
        else if (randomDirectionIndex == 1)
        {
            Cube cube = CreateCube(currentCube.transform.position.x, currentCube.transform.position.z + 1);
            currentCube = cube;

            if (UIController.Instance.isNormal)
            {
                CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
            }
            if (UIController.Instance.isEasy)
            {
                CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
                CreateCube(currentCube.transform.position.x - 2, currentCube.transform.position.z);
            }
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

    private void ResetGame()
    {
        cubePool.Clear();
        collectablesPool.Clear();
        orderIndex = 0;
        SetStartingCubes();
    }
}
