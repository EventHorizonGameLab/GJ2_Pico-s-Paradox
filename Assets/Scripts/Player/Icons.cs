using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icons : MonoBehaviour
{
    [SerializeField] GameObject icon;

    private void Start()
    {
        icon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out _))
            icon.SetActive(true);

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out _))
            icon.SetActive(false);
        
    }
    
}
