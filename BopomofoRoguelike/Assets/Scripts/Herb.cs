using System;
using System.Collections;
using UnityEngine;

public class Herb : Item
{
    public override Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose, Commands.Put };
    }

    public override void Use(PlayerController player, GameObject menu, int index)
    {
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        uiManager.items.RemoveAt(index);
        MenuManager menuManager = GameObject.Find("Menu Panel").GetComponent<MenuManager>();
        menuManager.RerenderItems();
        menuManager.itemIndex = 0;
        player.isPlayerUseItem = true;
        player.IncreaseHP(5);
        menu.SetActive(false);
        TurnManager turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        turnManager.ProcessTurn();
    }

    public override void Put(GameObject menu, int index)
    {
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        uiManager.items.RemoveAt(index);
        MenuManager menuManager = GameObject.Find("Menu Panel").GetComponent<MenuManager>();
        menuManager.RerenderItems();
        PlayerController playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        TurnManager turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        GameObject item = Instantiate(gameObject, new Vector3(playerController.playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerController.playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1), Quaternion.identity);
        turnManager.objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Add(item);
        Debug.Log(menuManager.itemIndex);
        Debug.Log(uiManager.items.Count);
        if (menuManager.itemIndex == uiManager.items.Count)
        {
            menuManager.itemIndex = uiManager.items.Count - 1;
        }
    }

    public override string GetNameTranslation(Language lang)
    {
        switch (lang)
        {
            case Language.Ja:
                return "やくそう";
            default:
                return "";
        }
    }
}
