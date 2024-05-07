using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public static Action<Vector3> OnChanginRoom;
    float roundedX;
    float roundedZ;
    Vector3 correctPosition;


    private void OnEnable()
    {
        OnChanginRoom += TpOnPlayer;
    }

    private void OnDisable()
    {
        OnChanginRoom -= TpOnPlayer;
    }
    void StayOnGrid()
    {
        roundedX = Mathf.Round(transform.position.x);
        roundedZ = Mathf.Round(transform.position.z);
        correctPosition = new(roundedX, transform.position.y, roundedZ);
        transform.position = correctPosition;
    }

    private void Update()
    {
        //StayOnGrid();
    }

    void TpOnPlayer(Vector3 targetoPos)
    {
        transform.position = targetoPos;
    }
}
