using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose, Commands.Put };
    }

    public virtual void Use(PlayerController player, GameObject gameObject, int index)
    {
        Debug.Log("The item used");
        player.isPlayerUseItem = true;
    }

    public virtual void Dispose(GameObject menu, int index)
    {
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        uiManager.items.RemoveAt(index);
        MenuManager menuManager = GameObject.Find("Menu Panel").GetComponent<MenuManager>();
        menuManager.RerenderItems();
        if (uiManager.items.Count != 0 && menuManager.itemIndex == uiManager.items.Count)
        {
            menuManager.itemIndex = uiManager.items.Count - 1;
        }
    }

    public virtual void Put(GameObject menu, int index)
    {
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        uiManager.items.RemoveAt(index);
        MenuManager menuManager = GameObject.Find("Menu Panel").GetComponent<MenuManager>();
        menuManager.RerenderItems();
        PlayerController playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        TurnManager turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        GameObject item = Instantiate(gameObject, new Vector3(playerController.playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerController.playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1), Quaternion.identity);
        turnManager.objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Add(item);
        if (uiManager.items.Count != 0 && menuManager.itemIndex == uiManager.items.Count)
        {
            menuManager.itemIndex = uiManager.items.Count - 1;
        }
    }

    public virtual string GetNameTranslation(Language lang)
    {
        switch (lang)
        {
            case Language.Ja:
                return "アイテム";
            default:
                return "";
        }
    }


    public static string GetCommandTranslation(Commands command, Language lang)
    {
        if (lang == Language.Ja)
        {
            switch (command)
            {
                case Commands.Use:
                    return "使う";
                case Commands.Dispose:
                    return "捨てる";
                case Commands.Put:
                    return "置く";
            }
        }
        return "";
    }
}
