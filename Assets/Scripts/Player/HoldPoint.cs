using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class HoldPoint : MonoBehaviour , IHolder
{


    //Ref
    [SerializeField] PlayerController Player;


    private void Update()
    {
        if (GameManager.IsHoldingAnObject && GameManager.PlayerIsOnGrid) // Blocca l'holdpoint e blocca l'asse di tenuta
        {
            Player.CheckAxisToHoldingObject(transform.localPosition);
            return;
            
        }
        else
        {
            Player.CheckAxisToHoldingObject(Vector3.zero);
        }
        
       
            
        if (InputManager.IsMoving(out Vector3 direction) && GameManager.PlayerIsOnGrid)
        {
            Vector3 offset = new(0.5f, 0.5f, 0.5f);

            if (direction.x > 0)
            {
                //Right
                transform.localPosition = new Vector3(offset.x, offset.y, 0);
            }
            else if (direction.x < 0)
            {
                //Left
                transform.localPosition = new Vector3(-offset.x, offset.y, 0);
            }

            if (direction.z > 0)
            {
                //Forward
                transform.localPosition = new Vector3(0, offset.y, offset.z);
            }
            else if (direction.z < 0)
            {
                //Backword
                transform.localPosition = new Vector3(0, offset.y, -offset.z);
            }
        }
    }
}
            


