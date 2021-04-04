using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public enum CollectableGeneration // правило, по которому расставляются кристаллы, выбирается в инспекторе
    {
        random,
        inOrder
    } 
    [SerializeField] CollectableGeneration collectablesGeneration;

    [SerializeField] Cube cubePrefab;
    
    public List<Cube> cubePool; // список кубов для повторного использования
    public List<Cube> collectablesPool; // список кубов для расстановки кристаллов
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
        Player.onGameOver += ResetGame;
        Player.onGameOver += SetStartingCubes;
        UIController.onStartGame += SetStartingCubes;
        cubePool = PregenerateCubePool(60);
    }

    private void SetStartingCubes()
    {
        // Создать стартовую платформу
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Cube cube = CreateCube(x, z);
                currentCube = cube;
                cube.gameObject.SetActive(true);
                collectablesPool.Clear();
            }
        }
        // создать начало дорожки
        for (int i = 0; i < 50; i++)
        {
           CreateCubeInRandomDirection();
        }
    }

    List<Cube> PregenerateCubePool(int amountOfCubes) // создать пул кубов, которые будут повторно использоваться в игре 
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

    Cube RequestCubeFromPool()
    {
        // вытащить куб из пула
        foreach (var cube in cubePool)
        {
            if (cube.gameObject.activeInHierarchy == false)
            {
                cube.gameObject.SetActive(true);
                return cube;
            }
        }

        // если свободного куба, создать новый
        Cube newCube = Instantiate(cubePrefab);
        newCube.transform.parent = transform;
        newCube.gameObject.SetActive(true);
        cubePool.Add(newCube);

        return newCube;
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
    void ActivateCollectablesRandomly() // кристаллы расставляются случайным образом
    {
        if (collectablesPool.Count == 5)
        {
            int randomIndex = Random.Range(0, collectablesPool.Count);
            collectablesPool[randomIndex].ActivateCollectable();
            collectablesPool.Clear();
        }
    }

    void ActivateCollectablesInOrder() // кристаллы расставляются по порядку
    {
        if (collectablesPool.Count == 25)
        {
            foreach (Cube cube in collectablesPool)
            {
                if (collectablesPool.IndexOf(cube) % 6 == 0)
                {
                    collectablesPool[collectablesPool.IndexOf(cube)].ActivateCollectable();
                }
            }
            collectablesPool.Clear();
        }
    }
 
    public Cube CreateCube(float x, float z)
    {
        Cube cube = RequestCubeFromPool();
        cube.transform.position = new Vector3(x, 0f, z);
        cube.transform.parent = transform;
        cube.name = "(x" + x + ", z" + z + " )";
        collectablesPool.Add(cube); 
         
        ActivateCollectables();

        return cube;
    }

    public Cube CreateCubeInRandomDirection() // создать дорожку в случайном направлении шириной в 1, 2 или 3 куба
    {
        int randomDirectionIndex = Random.Range(0, 2);
        if (randomDirectionIndex == 0)
        {
            Cube cube = CreateCube(currentCube.transform.position.x + 1, currentCube.transform.position.z);
            currentCube = cube;

            if (UIController.Instance.IsNormal())
            {
                Cube adjacentCube = CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
                adjacentCube.cubeType = Cube.CubeType.adjacent;
            }
            
            if (UIController.Instance.IsEasy())
            {
                Cube adjacentCube1 = CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 1);
                Cube adjacentCube2 = CreateCube(currentCube.transform.position.x, currentCube.transform.position.z - 2);
                adjacentCube1.cubeType = Cube.CubeType.adjacent;
                adjacentCube2.cubeType = Cube.CubeType.adjacent;
            }
            return cube;
        }
        else if (randomDirectionIndex == 1)
        {
            Cube cube = CreateCube(currentCube.transform.position.x, currentCube.transform.position.z + 1);
            currentCube = cube;

            if (UIController.Instance.IsNormal())
            {
                Cube adjacentCube3 = CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
                adjacentCube3.cubeType = Cube.CubeType.adjacent;
            }
            if (UIController.Instance.IsEasy())
            {
                Cube adjacentCube4 = CreateCube(currentCube.transform.position.x - 1, currentCube.transform.position.z);
                Cube adjacentCube5 = CreateCube(currentCube.transform.position.x - 2, currentCube.transform.position.z);
                adjacentCube4.cubeType = Cube.CubeType.adjacent;
                adjacentCube5.cubeType = Cube.CubeType.adjacent;
            }
            return cube;
        }
        return null;
    }

    private void ResetGame()
    {
        collectablesPool.Clear();

        foreach (Cube cube in cubePool)
        {
            cube.RecycleCube();
        }
    }
}
