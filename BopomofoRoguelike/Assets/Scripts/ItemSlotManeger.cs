using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManeger : MonoBehaviour
{
    public bool isEquiped = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MouseOver()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("ItemSlot");
        foreach (GameObject item in items)
        {
            if (!isEquiped)
            {
                item.GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 100f / 255f);
            }
            else
            {
                item.GetComponent<Image>().color = new Color(255f / 255f, 200f / 255f, 255f / 255f, 100f / 255f);
            }

        }
        if (!isEquiped)
        {
            gameObject.GetComponent<Image>().color = new Color(255f / 255f, 200f / 255f, 200f / 255f, 220f / 255f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(255f / 255f, 200f / 255f, 200f / 255f, 220f / 255f);
        }
    }

    public void MouseExit()
    {
        if (!isEquiped)
        {
            gameObject.GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 100f / 255f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(255f / 255f, 200f / 255f, 200f / 255f, 100f / 255f);
        }
    }
}
