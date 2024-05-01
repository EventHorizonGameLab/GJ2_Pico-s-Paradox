using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPoint : MonoBehaviour , IHolder
{
    

    //Ref
    [SerializeField] Transform playerTransform;


    private void Update()
    {
        if (GameManager.IsHoldingAnObject) // Mantiene l'holdpoint sull'oggetto, impedendogli di muoversi
            return;
        if (InputManager.IsMoving(out Vector3 direction))
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
            


