using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript driveScript;

    private void Start()
    {
        driveScript = GetComponent<DrivingScript>();
    }

    private void Update()
    {
        if (!RaceController.racePending) return;

        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");
        driveScript.Drive(accel, steer, brake);
    }
}
