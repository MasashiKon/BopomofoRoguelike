using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public List<Item> items = new List<Item>();
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        items.Add(new Herb("Herb"));
        for (int i = 0; i < 19; i++)
        {
            items.Add(new Item("Sample Item"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            isPaused = true;
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
