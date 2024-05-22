using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerControllerV2>(out _))
            BGM.OnGustavo?.Invoke();
    }
}

