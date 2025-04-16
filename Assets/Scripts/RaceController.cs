using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviourPunCallbacks
{
    public static bool RacePending => racePending;
    static bool racePending = false;

    [SerializeField] int laps = 1;
    [SerializeField] int countDownTimer = 3;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] GameObject finishPanel;

    [SerializeField] GameObject waitingText;
    [SerializeField] GameObject startButton;

    [SerializeField] AudioClip countDownSfx;
    [SerializeField] AudioClip startSfx;

    CheckpointController[] controllers;
    AudioSource audioSource;

    [SerializeField] GameObject carPrefab;
    [SerializeField] Transform[] spawnPos;

    int playerNumber;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        countDownText.gameObject.SetActive(false);
        finishPanel.SetActive(false);

        waitingText.SetActive(false);
        startButton.SetActive(false);

        //spawnowanie
        object[] instanceData = new object[4];
        instanceData[0] = PlayerPrefs.GetString("PlayerName");
        instanceData[1] = PlayerPrefs.GetFloat("R");
        instanceData[2] = PlayerPrefs.GetFloat("G");
        instanceData[3] = PlayerPrefs.GetFloat("B");

        playerNumber = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        Vector3 startPos = spawnPos[playerNumber].position;
        Quaternion startRot = spawnPos[playerNumber].rotation;
        GameObject playerCar = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0, instanceData);

        playerCar.GetComponent<CarAppearance>().SetLocalPlayer();
        playerCar.GetComponent<PlayerController>().enabled = true;

        if(PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            waitingText.SetActive(true);
        }
    }

    public void StartRace()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        photonView.RPC(nameof(StartRaceRPC), RpcTarget.All, null);
    }

    [PunRPC]
    public void StartRaceRPC()
    {
        controllers = FindObjectsOfType<CheckpointController>();
        InvokeRepeating(nameof(CountDown), 3, 1);

        startButton.SetActive(false);
        waitingText.SetActive(false);
    }

    public void RestartRace()
    {
        photonView.RPC(nameof(RestartRPC), RpcTarget.All, null);
    }
    [PunRPC]
    void RestartRPC()
    {
        countDownTimer = 3;

        Transform car = OnlinePlayer.LocalPlayerInstance.transform.GetChild(0);
        car.position = spawnPos[playerNumber].position;
        car.rotation = spawnPos[playerNumber].rotation;

        car.parent.GetComponent<DrivingScript>().Stop();

        foreach(var c in controllers)
        {
            c.Restart();
        }

        finishPanel.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            waitingText.SetActive(true);
        }
    }

    void HideCountdownText()
    {
        countDownText.gameObject.SetActive(false);
    }

    void CountDown()
    {
        countDownText.gameObject.SetActive(true);
        countDownText.text = countDownTimer.ToString();
        audioSource.PlayOneShot(countDownSfx);
        if (countDownTimer < 1)
        {
            countDownText.text = "Start!";
            audioSource.PlayOneShot(startSfx);
            Invoke(nameof(HideCountdownText), 1);
            racePending = true;
            CancelInvoke(nameof(CountDown));
            return;
        }
        countDownTimer--;
    }

    private void Update()
    {
        if (!racePending) return;

        int finishers = 0;
        foreach(var c in controllers)
        {
            if(c.Lap >= laps + 1)
                finishers++;
        }

        if (finishers >= controllers.Length)
        {
            finishPanel.SetActive(true);
            racePending = false;
        }
    }
}
