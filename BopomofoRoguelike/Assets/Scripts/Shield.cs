using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public int defence = 5;

    public override Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose, Commands.Put, Commands.Throw, Commands.Equip, Commands.Off };
    }

    public override void Use(PlayerController player, GameObject menu, int index)
    {

    }

    public override void Equip(GameObject menu, int index)
    {
        isEquiped = true;
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        GameObject player = GameObject.Find("Player");
        GameObject copiedItem = Instantiate(uiManager.items[index], player.transform.position, player.transform.rotation);
        copiedItem.transform.SetParent(player.transform);
        copiedItem.GetComponent<SpriteRenderer>().sortingOrder = 4;
        copiedItem.transform.localPosition = new Vector3(copiedItem.transform.localPosition.x - 0.2f, copiedItem.transform.localPosition.y + 0.4f, copiedItem.transform.localPosition.z);
        uiManager.isPaused = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().shield = gameObject.GetComponent<Shield>();
        menu.SetActive(false);
    }

    public override void Off(GameObject menu, int index)
    {
        isEquiped = false;
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Shield>())
            {
                Destroy(player.transform.GetChild(i).gameObject);
            }
        }
        uiManager.isPaused = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().sword = gameObject.GetComponent<Sword>();
        menu.SetActive(false);
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
                return "盾";
            default:
                return "";
        }
    }

    public int GetSwordDefence()
    {
        return defence;
    }
}