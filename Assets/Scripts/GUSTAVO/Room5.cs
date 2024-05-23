using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5 : MonoBehaviour
{
    public static Action changeLights; 
    public static Action gustavo;
    [SerializeField] GameObject gustavoPrefab;

    private void OnEnable()
    {
        gustavo += SpawnGustavo;
    }

    private void OnDisable()
    {
        gustavo -= SpawnGustavo;
    }
    private void SpawnGustavo()
    {
        Instantiate(gustavoPrefab, transform.position, Quaternion.identity);
        changeLights?.Invoke();
    }
}