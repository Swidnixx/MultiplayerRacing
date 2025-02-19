using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CarStabilizer : MonoBehaviour
{
    public Transform centerOfMass;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }
}
