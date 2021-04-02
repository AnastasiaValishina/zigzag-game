using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] int totalAmount;
    [SerializeField] int collectableValue = 1;

    private void Start()
    {
        Player.onGameOver += Reset;
    }
    public void AddCollectable()
    {
        totalAmount += collectableValue; 
    }

    private void Reset()
    {
        totalAmount = 0;
    }
}
