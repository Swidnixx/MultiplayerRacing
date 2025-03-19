using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField playerName;
    [SerializeField] Slider r, g, b;
    [SerializeField] MeshRenderer auto;
    Color color;

    private void Start()
    {
        playerName.text = PlayerPrefs.GetString("PlayerName");

        r.value = PlayerPrefs.GetFloat("R");
        g.value = PlayerPrefs.GetFloat("G");
        b.value = PlayerPrefs.GetFloat("B");
    }

    private void Update()
    {
        color = new Color(r.value, g.value, b.value);
        auto.material.color = color;

        PlayerPrefs.SetFloat("R", r.value);
        PlayerPrefs.SetFloat("G", g.value);
        PlayerPrefs.SetFloat("B", b.value);
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
}
