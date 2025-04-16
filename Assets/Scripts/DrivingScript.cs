using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    [SerializeField] Wheel[] wheels;
    [SerializeField] float maxThrustTorque = 500;
    [SerializeField] float maxSteerAngle = 30;
    [SerializeField] float maxBrakeTorque = 750;

    Rigidbody rb; 
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void Drive(float accel, float steer, float brake)
    {
        float thrustTorque = accel * maxThrustTorque;
        steer *= maxSteerAngle;
        brake *= maxBrakeTorque;

        foreach (var w in wheels)
        {
            if (w.IsFront)
            {
                w.Wh.steerAngle = steer; 
            }

            w.Wh.motorTorque = thrustTorque;
            w.Wh.brakeTorque = brake;
        }
    }

    internal void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Drive(0, 0, 1);
    }
}
