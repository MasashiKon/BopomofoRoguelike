using System;
using System.Collections;
using UnityEngine;

public class Herb : Item
{
    public Herb(string nameOfItem) : base(nameOfItem)
    {

    }

    public override Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose };
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
}
