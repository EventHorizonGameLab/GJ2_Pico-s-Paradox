using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public static Action win;

    private void OnTriggerEnter(Collider other)
    {
        win?.Invoke();
    }
}
