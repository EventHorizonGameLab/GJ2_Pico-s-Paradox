using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gustavo : MonoBehaviour
{
    [SerializeField] float movementCooldown;
    [SerializeField] float movementDistance;
    [SerializeField] float cooldownBetweenRooms;
    public static Action lose;
    [SerializeField] float gustavoSize;

    private void OnEnable()
    {
        StartCoroutine(Movement());

        //evento marco += changeroom;
    }

    private void OnDisable()
    {
        //evento marco -= changeroom;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            StopAllCoroutines();
            lose?.Invoke();
        }
    }
    IEnumerator Movement()
    {
        Vector3 pos = transform.position;
        yield return new WaitForSeconds(movementCooldown);
        pos.x += movementDistance;
        
        if (Physics.Raycast(transform.position + Vector3.left * gustavoSize, Vector3.left, out RaycastHit hit ,movementDistance))
        {
            pos.x = gustavoSize + (hit.collider.bounds.extents.x * 2) + hit.point.x; 
        }
        transform.position = pos;
        StartCoroutine(Movement());
    }
    
    void ChangeRoom(Vector3 newRoomPos)
    {
        StopAllCoroutines();
        StartCoroutine(WaitCooldown(newRoomPos));
        
    }

    IEnumerator WaitCooldown(Vector3 newRoomPos)
    {
        Vector3 pos;
        yield return new WaitForSeconds(cooldownBetweenRooms);
        pos = transform.position;
        pos.x = newRoomPos.x;
        transform.position = pos;
        StartCoroutine(Movement());
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.left * gustavoSize, Vector3.left * movementDistance);
    }
#endif
}