using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] int totalAmount;
    [SerializeField] int collectableValue = 1;
    void Start()
    {
        
    }

    public void AddCollectable()
    {
        totalAmount += collectableValue; 
    }
}
