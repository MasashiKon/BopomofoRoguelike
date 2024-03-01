using System.Collections;
using UnityEngine;

public class Herb : Item
{
    public Herb(string nameOfItem) : base(nameOfItem)
    {

    }

    public override void Use(PlayerController player, GameObject menu)
    {
        player.IncreaseHP(5);
        menu.SetActive(false);
    }
}
