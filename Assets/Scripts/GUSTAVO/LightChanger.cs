using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    Light myLight;
    [SerializeField] Color myColor;

    private void Awake()
    {
        myLight = GetComponent<Light>();
    }
    private void OnEnable()
    {
        Room5.changeLights += ChangeLight;
    }

    private void OnDisable()
    {
        Room5.changeLights -= ChangeLight;
    }

    private void ChangeLight()
    {
        myLight.color = myColor;
    }
}
