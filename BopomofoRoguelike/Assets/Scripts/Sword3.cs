using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Sword3 : Sword
{
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
        copiedItem.transform.localPosition = new Vector3(copiedItem.transform.localPosition.x + 0.4f, copiedItem.transform.localPosition.y, copiedItem.transform.localPosition.z);
        uiManager.isPaused = false;
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (playerController.sword)
        {
            playerController.sword.isEquiped = false;
        }
        playerController.sword = gameObject.GetComponent<Sword3>();
        playerController.isPlayerUseItem = true;
        menu.SetActive(false);
        StartCoroutine(RenderTextAndProcessTurn(GetNameTranslation(Language.Ja) + "を装備した"));
    }

    public override void EquipWithoutText(int index)
    {
        isEquiped = true;
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        GameObject player = GameObject.Find("Player");
        GameObject copiedItem = Instantiate(uiManager.items[index], player.transform.position, player.transform.rotation);
        copiedItem.transform.SetParent(player.transform);
        copiedItem.GetComponent<SpriteRenderer>().sortingOrder = 4;
        copiedItem.transform.localPosition = new Vector3(copiedItem.transform.localPosition.x + 0.4f, copiedItem.transform.localPosition.y, copiedItem.transform.localPosition.z);
        uiManager.isPaused = false;
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.sword = gameObject.GetComponent<Sword3>();
    }

    public override void Off(GameObject menu, int index)
    {
        isEquiped = false;
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Sword3>())
            {
                Destroy(player.transform.GetChild(i).gameObject);
            }
        }
        uiManager.isPaused = false;
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.sword = null;
        playerController.isPlayerUseItem = true;
        menu.SetActive(false);
        StartCoroutine(RenderTextAndProcessTurn(GetNameTranslation(Language.Ja) + "を外した"));
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
                return "鋭い剣";
            default:
                return "";
        }
    }

    public override int GetSwordAttack()
    {
        return attack;
    }

    IEnumerator RenderTextAndProcessTurn(string text)
    {
        TextMeshProUGUI textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        textMessage.SetText(text);

        yield return new WaitForSeconds(0.5f);

        textMessage.SetText("");

        GameObject.Find("Turn Manager").GetComponent<TurnManager>().ProcessTurn();
    }
}
