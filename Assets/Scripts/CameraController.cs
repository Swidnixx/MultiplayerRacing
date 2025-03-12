using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera[] cams;
    int activeCamera = 0;

    private void Start()
    {
        cams = GetComponentsInChildren<CinemachineVirtualCamera>(true);
        foreach(var c in cams)
        {
            c.gameObject.SetActive(true);
            c.enabled = false;
        }
        cams[activeCamera].enabled = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            cams[activeCamera].enabled = false;
            activeCamera++;
            activeCamera %= cams.Length;
            cams[activeCamera].enabled = true;
        }
    }
}
