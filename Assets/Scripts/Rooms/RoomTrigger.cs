using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    Vector3 targetPos;

    private void Awake()
    {
        targetPos = transform.GetChild(0).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ITeleportable>(out _))
        {
            other.transform.position = targetPos;
            MovePoint.OnChanginRoom(targetPos);
        }
    }
}
