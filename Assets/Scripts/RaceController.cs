using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    public static bool RacePending => racePending;
    static bool racePending = false;

    [SerializeField] int laps = 2;
    [SerializeField] int countDownTimer = 3;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] GameObject finishPanel;

    [SerializeField] AudioClip countDownSfx;
    [SerializeField] AudioClip startSfx;

    CheckpointController[] controllers;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        countDownText.gameObject.SetActive(false);
        finishPanel.SetActive(false);

    }

    void StartRace()
    {
        controllers = FindObjectsOfType<CheckpointController>();
        InvokeRepeating(nameof(CountDown), 3, 1);
     
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
        int finishers = 0;
        foreach(var c in controllers)
        {
            if(c.Lap == laps + 1)
                finishers++;
        }

        if (finishers >= controllers.Length)
        {
            finishPanel.SetActive(true);
            racePending = false;
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
