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

    private void Start()
    {
        nameText.text = playerName;
        nameText.color = carColor;
        carRenderer.material.color = carColor;
    }
}
