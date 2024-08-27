using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUpdater : MonoBehaviour
{
    GameManager gmInstance;
    [SerializeField] GameState levelState;
    public int totalLaps;

    public void Start()
    {
        gmInstance = GameManager.instance;
        gmInstance.maxLaps = totalLaps;
        gmInstance.currentScene = SceneManager.GetActiveScene();
        gmInstance.gameState = levelState;
        if (gmInstance.gameState == GameState.Racing) { gmInstance.gameState = GameState.CountDown; }
        if (gmInstance.gameState == GameState.CountDown) { StartCoroutine(gmInstance.CountDown()); }
    }

    public void SwitchToScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }

    public void QuitGame()
    {
        Debug.Log("Game has been ended.");
        Application.Quit();
    }
}
