using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider Wh => wh;
    public bool IsFront => isFront;

    [SerializeField] bool isFront;
    [SerializeField] MeshRenderer model;
    [SerializeField] WheelCollider wh;

    private void Update()
    {
        wh.GetWorldPose(out Vector3 pos, out Quaternion rot);
        model.transform.position = pos;
        model.transform.rotation = rot;
    }
}
