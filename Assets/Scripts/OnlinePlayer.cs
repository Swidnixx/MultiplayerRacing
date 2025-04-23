using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    public int Number { get; private set; }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            GetComponent<CarAppearance>().SetLocalPlayer();
        }
        else
        {
            string name = (string)photonView.InstantiationData[0];
            Color color = new Color(
                (float)photonView.InstantiationData[1],
                (float)photonView.InstantiationData[2],
                (float)photonView.InstantiationData[3]
                );
            GetComponent<CarAppearance>().SetNameAndColor(name, color);
        }

        Number = (int)photonView.InstantiationData[4];
        RaceController rc = FindObjectOfType<RaceController>();
        rc.Register(photonView.Owner, this);

        GetComponent<CarAppearance>().CarRego = Number;
    }
}
