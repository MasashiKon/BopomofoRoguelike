using System;
using System.Collections;
using UnityEngine;

public class Herb : Item
{

    public override Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose, Commands.Put, Commands.Throw };
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

    public override void Collision(GameObject objectGotHit)
    {
        if (objectGotHit.CompareTag("Enemy"))
        {
            objectGotHit.GetComponent<EnemyController>().IncreaceHP(5);
            Destroy(gameObject);
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
