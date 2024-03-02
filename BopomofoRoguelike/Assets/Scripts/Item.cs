using UnityEngine;

public class Item
{
    public string name;

    public Item(string nameOfItem)
    {
        name = nameOfItem;
    }

    public virtual Commands[] GetCommands()
    {
        return new Commands[] { Commands.Use, Commands.Dispose };
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
        if (menuManager.items.Count <= index)
        {
            menuManager.itemIndex = menuManager.items.Count - 1;
        }
        menuManager.RerenderItems();
    }

    public static string GetTranslation(Commands command, Language lang)
    {
        if (lang == Language.Ja)
        {
            switch (command)
            {
                case Commands.Use:
                    return "使う";
                case Commands.Dispose:
                    return "捨てる";
            }
        }
        return "";
    }
}
