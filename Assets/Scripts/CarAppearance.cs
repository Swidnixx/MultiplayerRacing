using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAppearance : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] Color carColor;
    [SerializeField] TMP_Text nameText;
    [SerializeField] MeshRenderer carRenderer;

    // Do dostosowania przez prowadz¹cego do twojej koncepcji projektu :-)
    int carRego;
    bool regoSet = false;
    public CheckpointController checkPoint;

    public void SetNameAndColor(string name, Color color)
    {
        playerName = name;
        carColor = color;

        nameText.text = playerName;
        nameText.color = carColor;
        carRenderer.material.color = carColor;
    }

    public void SetLocalPlayer()
    {
        string name = PlayerPrefs.GetString("PlayerName");
        Color color = new Color(
            PlayerPrefs.GetFloat("R"),
            PlayerPrefs.GetFloat("G"),
            PlayerPrefs.GetFloat("B"));

        SetNameAndColor(name, color);

        FindObjectOfType<CameraController>().SetCamera(GetComponentInChildren<Rigidbody>().transform);
    }

    public int CarRego
    {
        get
        {
            return carRego;
        }
        set
        {
            carRego = value;
            Leaderboard.RegisterCar(carRego, playerName);
            regoSet = true;
        }
    }
    private void LateUpdate()
    {
        if (!regoSet)
        {

            return;
        }

        Leaderboard.SetPosition(carRego, checkPoint.lap, checkPoint.checkpoint);
    }
}
