using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupportScript : MonoBehaviour
{
    CheckpointController ckpController;
    Rigidbody rb;

    float notOk;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ckpController = GetComponent<CheckpointController>();
    }

    bool CheckIfNotOk()
    {
        return 
            transform.up.y < 0.38765f
            || 
            rb.velocity.magnitude < 0.0012f;
    }

    private void Update()
    {
        if (!RaceController.RacePending) return;

        if (CheckIfNotOk())
            notOk += Time.deltaTime;
        else
            notOk = 0;
        if (notOk > 3.5f)
            TurnCarBack();
    }

    void TurnCarBack()
    {
        //transform.position += Vector3.up;
        transform.position = ckpController.LastCheckpoint.position;
        //transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        transform.rotation = ckpController.LastCheckpoint.rotation;
        rb.angularVelocity = Vector3.zero;
    }
}
