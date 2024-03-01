using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandPanelManager : MonoBehaviour
{
    public GameObject commandSlot;
    public Item item;
    public bool isFocused = false;
    private PlayerController playerController;
    private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        menu = GameObject.Find("Menu Panel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            isFocused = false;
            menu.GetComponent<MenuManager>().isFocused = true;
            GameObject.Find("UI Manager").GetComponent<UIManager>().isPaused = false;
            item.Use(playerController, menu);
        }
    }

    public void GenerateCommands()
    {
        GameObject itemInstence = Instantiate(commandSlot, transform.position, Quaternion.identity);
        itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Use");
        itemInstence.transform.SetParent(transform);
        RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
        Debug.Log(rectTransform.offsetMax.y);
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, -50);
        itemInstence.GetComponent<CommandSlotManager>().MouseOver();
        isFocused = true;
    }

    public void DestroyAllCommands()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
}
