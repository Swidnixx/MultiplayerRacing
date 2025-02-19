using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupportScript : MonoBehaviour
{
    Rigidbody rb;

    float notOk;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.up.y < 0.38765f)
            notOk += Time.deltaTime;
        else
            notOk = 0;
        if (notOk > 3.5f)
            TurnCarBack();
    }

    void TurnCarBack()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        rb.angularVelocity = Vector3.zero;
    }
}
