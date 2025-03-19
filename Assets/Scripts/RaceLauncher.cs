using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceLauncher : MonoBehaviour
{
    public void JoinRace()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
