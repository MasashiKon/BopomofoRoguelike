using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CommandSlotManager : MonoBehaviour
{
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
        GameObject[] items = GameObject.FindGameObjectsWithTag("CommandSlot");
        foreach (GameObject item in items)
        {
            item.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
        }
        gameObject.GetComponent<Image>().color = new Color(255 / 255f, 200 / 255f, 200 / 255f, 220 / 255f);
    }

    public void MouseExit()
    {
        gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
    }
}
