using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IconCaller : MonoBehaviour, IInteractable // qusto script è fatto in emergenza perchè le cose son state fatte alla come viene, sorry Robert
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void Interact()
    {
        // no need
    }
}
