using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : EntityBehavior
{
    public List<KeyCode> inputKeyList;

    protected override void Awake()
    {
        base.Awake();

        inputKeyList.Add(KeyCode.W);
        inputKeyList.Add(KeyCode.A);
        inputKeyList.Add(KeyCode.S);
        inputKeyList.Add(KeyCode.D);
        inputKeyList.Add(KeyCode.UpArrow);
        inputKeyList.Add(KeyCode.LeftArrow);
        inputKeyList.Add(KeyCode.DownArrow);
        inputKeyList.Add(KeyCode.RightArrow);
    }

    public override void LapComplete()
    {
        if (currentLapTimer < PlayerPrefs.GetFloat("PersonalRecord", 600))
        {
            PlayerPrefs.SetFloat("PersonalRecord", currentLapTimer);
            PlayerPrefs.Save();
        }
        base.LapComplete();
    }

    protected override void Update()
    {
        base.Update();
        GameManager gmInstance = GameManager.instance;

        for (int i = 0; i < inputKeyList.Count; i++)
        {
            if (Input.GetKeyUp(inputKeyList[i]))
            { mScript.receivedEndInput = mScript.movementInput; }
        }

        //Feeds the Input Axes of "Horizontal" & "Vertical" to the MovementScript attached to the player.
        //Shouldn't cause any problems, as the Player should always have a MovementScript.
        if(gmInstance.gameState == GameState.Racing || gmInstance.gameState == GameState.MainMenu)
        {
            mScript.movementInput.x = Input.GetAxisRaw("Horizontal");
            mScript.movementInput.y = Input.GetAxisRaw("Vertical");
        }
        

        gmInstance.playerMaxHealth = hScript.maxHealth;
        gmInstance.playerHealth = hScript.currentHealth;
        gmInstance.playerLap = currentLap;
        gmInstance.playerSpeed = mScript.currentSpeed;
        gmInstance.playerLapTime = currentLapTimer;
        gmInstance.playerPrevLapTime = previousLapTime;
        gmInstance.playerLapRecord = personalRecord;
    }
}
