using System;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenUpdater : MonoBehaviour
{
    GameManager gmInstance;
    [SerializeField] GameObject finishScreen;
    [SerializeField] Text finishScreenText;

    // Start is called before the first frame update
    void Start()
    {
        gmInstance = GameManager.instance;
        DisplayLapTimes();
    }

    private void Update()
    {
        if (finishScreen.activeSelf && finishScreenText.text == "")
        {
            DisplayLapTimes();
        }
    }

    public void DisplayLapTimes()
    {
        TimeSpan[] allLapTimes = new TimeSpan[gmInstance.maxLaps];
        for (int i = 0; i < gmInstance.maxLaps; i++)
        { allLapTimes[i] = TimeSpan.FromSeconds(gmInstance.playerLapTimes[i]); }

        for (int i = 0; i < gmInstance.maxLaps; i++)
        {
            if (i != 0) { finishScreenText.text += "\n"; }
            finishScreenText.text += "Lap " + (i + 1) + ": " + allLapTimes[i].Minutes.ToString("00") + ":" + allLapTimes[i].Seconds.ToString("00") + ":" + allLapTimes[i].Milliseconds.ToString("000");
        }
    }
}
