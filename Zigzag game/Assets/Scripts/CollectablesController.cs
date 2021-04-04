using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] int collectableValue = 1;
    int totalAmount;

    static CollectablesController instance;

    public static CollectablesController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CollectablesController>();
            }
            return instance;
        }
    }
    private void Start()
    {
        Player.onGameOver += Reset;
    }
    public void AddCollectable()
    {
        totalAmount += collectableValue;
        UIController.Instance.UpdateScore();
    }

    private void Reset()
    {
        totalAmount = 0;
        UIController.Instance.UpdateScore();
    }

    public int GetTotalScore()
    {
        return totalAmount;
    }
}
