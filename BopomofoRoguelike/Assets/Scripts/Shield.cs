using System;
using System.Collections;
using UnityEngine;

public class Shield : Item
{
    public override void Use(PlayerController player, GameObject menu, int index)
    {

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
}