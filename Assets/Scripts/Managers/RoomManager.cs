using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //Events
    //public static Action<int,Transform> OnChangingRoom;

    //var
    [Header("Spawn Points")]
    [SerializeField] private Transform secondRoomEnter;
    [SerializeField] private Transform firstRoomReturn;
    [SerializeField] private Transform thirdRoomEnter;
    [SerializeField] private Transform secondRoomReturn;
    [SerializeField] private Transform fourthRoomEnter;
    [SerializeField] private Transform thirdRoomReturn;
    [SerializeField] private Transform fifthRoomEnter;
    [SerializeField] private Transform fourthRoomReturn;
    [Header("MovePoint")]
    [SerializeField] private Transform movePoint;
    
    Dictionary<int, Transform> SpawnPoints;

    enum WichRoomToGo
    {
        TWOtoONE = 1, ONEtoTWO = 2, THREEtoTWO=3, TWOtoTHREE=4, FOURtoTHREE=5, THREEtoFOUR=6, FIVEtoFOUR=7, FOURtoFIVE=8 
    }

    private void Awake()
    {
        InitializeMapLogic();
    }

    private void OnEnable()
    {
        //OnChangingRoom += Teleport;
    }

    void InitializeMapLogic()
    {
        SpawnPoints = new Dictionary<int, Transform>()
        {
            { (int)WichRoomToGo.TWOtoONE, firstRoomReturn },
            { (int)WichRoomToGo.ONEtoTWO, secondRoomEnter },
            { (int)WichRoomToGo.THREEtoTWO, secondRoomReturn },
            { (int)WichRoomToGo.TWOtoTHREE, thirdRoomEnter },
            { (int)WichRoomToGo.FOURtoTHREE, thirdRoomReturn },
            { (int)WichRoomToGo.THREEtoFOUR, fourthRoomEnter },
            { (int)WichRoomToGo.FIVEtoFOUR, fourthRoomReturn },
            { (int)WichRoomToGo.FOURtoFIVE, fifthRoomEnter }
        };
    }

    protected void Teleport(int index, Transform objToTeleport)
    {
        if(SpawnPoints.ContainsKey(index))
        {
            Vector3 targetPosition = SpawnPoints[index].position;
            movePoint.position = targetPosition;
            objToTeleport.position = targetPosition;
        }

    }

        
    


}
