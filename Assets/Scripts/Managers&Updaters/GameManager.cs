using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    MainMenu,
    CountDown,
    Racing,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Scene currentScene;
    public GameState gameState;

    public int[] entityMaxHealth;
    public int[] entityHealth;
    public float[] entitySpeed;
    public int[] entityLap;
    public float[] entityLapTime;
    public float[] entityPrevLapTime;

    public int playerMaxHealth;
    public int playerHealth;
    public float playerSpeed;
    public int playerLap;
    public float playerLapTime;
    public float playerPrevLapTime;
    public float playerLapRecord;
    public float[] playerLapTimes;

    public int countDownInt = 0;
    public int maxLaps;

    public List<EntityBehavior> entities = new List<EntityBehavior>();

    private void Awake()
    {
        if (this != instance && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void CheckRanking()
    {

    }

    public IEnumerator CountDown()
    {
        countDownInt = 3;
        yield return new WaitForSeconds(1f);
        countDownInt = 2;
        yield return new WaitForSeconds(1f);
        countDownInt = 1;
        yield return new WaitForSeconds(1f);
        countDownInt = 0;
        yield return new WaitForSeconds(0.5f);
        gameState = GameState.Racing;
    }
}
