using UnityEngine;

public class Item
{
    public string name;
    public string[] commandList;

    public Item(string nameOfItem)
    {
        name = nameOfItem;
    }

    public virtual void Use(PlayerController player, GameObject gameObject)
    {
        Debug.Log("The item used");
    }
}
