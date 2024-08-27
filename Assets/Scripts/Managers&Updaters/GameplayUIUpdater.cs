using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIUpdater : MonoBehaviour
{
    GameManager gmInstance;

    [SerializeField] GameObject healthHolder; 
    [SerializeField] Image[] healthIcons;
    [SerializeField] GameObject speedometer;
    [SerializeField] Image pointer;
    [SerializeField] GameObject raceData;
    [SerializeField] Text currentLap;
    [SerializeField] Text currentLapTime;
    [SerializeField] Text previousLapTime;
    [SerializeField] Text personalRecordTime;
    [SerializeField] Text countDown;

    int zeroPointRotation = 130;
    int capPointRotation = -130;
    [SerializeField] float rotateScale = 5;

    public void Start()
    {
        pointer.transform.rotation = Quaternion.Euler(0, 0, zeroPointRotation);
        gmInstance = GameManager.instance;
    }

    public void Update()
    {
        for (int i = 0; i < gmInstance.playerMaxHealth; i++)
        {
            if (i >= gmInstance.playerHealth) { healthIcons[i].enabled = false; }
            else { healthIcons[i].enabled = true; }
        }

        if(gmInstance.gameState == GameState.CountDown)
        {
            countDown.gameObject.SetActive(true);
            countDown.text = gmInstance.countDownInt.ToString();
            if(gmInstance.countDownInt == 0)
            {
                countDown.text = "GO";
            }
        }
        else
        {
            countDown.gameObject.SetActive(false);
        }

        pointer.transform.rotation = Quaternion.Euler(0, 0, zeroPointRotation - (gmInstance.playerSpeed * rotateScale));

        TimeData();
    }

    public void TimeData()
    {
        TimeSpan currentTimer = TimeSpan.FromSeconds(gmInstance.playerLapTime);
        TimeSpan previousTime = TimeSpan.FromSeconds(gmInstance.playerPrevLapTime);
        TimeSpan PersonalRecord = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("PersonalRecord"));
        currentLap.text = "Lap:" + gmInstance.playerLap;
        currentLapTime.text = "Laptime:" + currentTimer.Minutes.ToString("00") + ":" + currentTimer.Seconds.ToString("00") + ":" + currentTimer.Milliseconds.ToString("000");
        previousLapTime.text = "Previous Lap:" + previousTime.Minutes.ToString("00") + ":" + previousTime.Seconds.ToString("00") + ":" + previousTime.Milliseconds.ToString("000");
        personalRecordTime.text = "Personal Record: " + PersonalRecord.Minutes.ToString("00") + ":" + PersonalRecord.Seconds.ToString("00") + ":" + PersonalRecord.Milliseconds.ToString("000");
    }
}