using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    GameManager gmInstance;
    UIManager uiInstance;

    GameObject player;
    [SerializeField] GameObject[] assignedMenus;
    [SerializeField] GameObject[] menuArray;
    [SerializeField] GameObject currentMenu;

    private void Awake()
    {
        gmInstance = GameManager.instance;
        uiInstance = UIManager.instance;

        menuArray = new GameObject[assignedMenus.Length];
        for (int i = 0; i < menuArray.Length; i++)
        {
            menuArray[assignedMenus[i].GetComponent<MenuID>().menuIdentity] = assignedMenus[i];
        }

        uiInstance.UIMenus = menuArray;
        uiInstance.MenuActive = new bool[menuArray.Length];
        uiInstance.uiScript = this;
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        GameState gState = gmInstance.gameState;

        //Keeps the MenuActive bool[] up to date with the current UI activity; specifically if the menu 'i' is enabled or disabled.
        for (int i = 0; i < menuArray.Length; i++) { uiInstance.MenuActive[i] = menuArray[i].activeSelf; }

        //Toggles the Pause Menu if you are in a race.
        if (Input.GetKeyDown(KeyCode.Escape) && (currentMenu == null || currentMenu == menuArray[1]) && gState == GameState.Racing)
        { ToggleMenuUI(1); }

        //Toggles the Main Menu when in the Main Menu scene.
        if (Input.GetKeyDown(KeyCode.Escape) && gState == GameState.MainMenu){ ToggleMenuUI(1); }

        //Pauses time if the Pause Menu is active.
        if (gState == GameState.Racing && uiInstance.MenuActive[1])
        { Time.timeScale = 0; }
        else { Time.timeScale = 1; }

        //Assures that the Gameplay UI is on during gameplay. Does not turn it off if the driverState variable changes.
        if (MenuInactiveCheck() == menuArray.Length) 
        { 
            player.GetComponent<CarHealth>().driverState = DriverState.driving;
            menuArray[0].SetActive(true);
        }

        //Turns on the FinishScreen if the player has completed the last lap.
        if (gmInstance.playerLap > gmInstance.maxLaps && !uiInstance.MenuActive[4] && gState == GameState.Racing) { ToggleMenuUI(4); }
    }

    /// <summary>
    /// Checks if the Menus that are in the scene are active or not. Is used to determine some of the game logic, to prevent menus from activating when they shouldn't.
    /// </summary>
    /// <returns></returns>
    public int MenuInactiveCheck()
    {
        int inactiveCheckInt = 0;
        for (int i = 0; i < menuArray.Length; i++)
        {
            if (!uiInstance.MenuActive[i]) { inactiveCheckInt++; }
        }
        return inactiveCheckInt;
    }

    /// <summary>
    /// Toggles a menu on or off depending on if the targeted menu is active or not. Deactivates all other menus through "OnlyThisMenuOn()"
    /// </summary>
    /// <param name="targetMenu">The menu being toggled. Correlates directly to MenuID's "menuIdentity" integer.</param>
    public void ToggleMenuUI(int targetMenu)
    {
        switch (uiInstance.MenuActive[targetMenu])
        {
            case true: 
                menuArray[targetMenu].SetActive(false);
                currentMenu = null;
                break;
            case false: 
                menuArray[targetMenu].SetActive(true);
                currentMenu = menuArray[targetMenu];
                break;
        }
        OnlyThisMenuOn(targetMenu);

        if (targetMenu > 1)
        { player.GetComponent<CarHealth>().driverState = DriverState.stunned; }
        else
        { player.GetComponent<CarHealth>().driverState = DriverState.driving; }
    }

    /// <summary>
    /// Turns off all menus excluding the targetMenu.
    /// </summary>
    /// <param name="targetMenu">The menu intended to remain on while all others are turned off.</param>
    public void OnlyThisMenuOn(int targetMenu)
    {
        for (int i = 0; i < menuArray.Length; i++)
        {
            if (menuArray[i] != menuArray[targetMenu])
            { 
                menuArray[i].SetActive(false);
            }
        }
    }
}
