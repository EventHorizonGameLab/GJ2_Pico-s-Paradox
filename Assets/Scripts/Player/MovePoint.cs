using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{

    float roundedX;
    float roundedZ;
    Vector3 correctPosition;

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
}
