using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject itemSlot;
    public GameObject uiManager;
    public GameObject scrollPort;
    public GameObject commandPanel;
    public GameObject commandSlot;
    public bool isFocused = true;
    private List<Item> items;
    private GameObject currentItem;
    private int itemIndex = 0;
    private RectTransform content;
    private PlayerController playerController;
    private CommandPanelManager commandPanelManager;

    // Start is called before the first frame update
    void Start()
    {
        items = uiManager.GetComponent<UIManager>().items;
        content = scrollPort.GetComponent<RectTransform>();
        commandPanelManager = commandPanel.GetComponent<CommandPanelManager>();
        playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, items.Count * 50);
        for (int i = 0; i < items.Count; i++)
        {
            GameObject itemInstence = Instantiate(itemSlot, gameObject.transform.position, Quaternion.identity);
            itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(items[i].name);
            itemInstence.transform.SetParent(scrollPort.transform);
            RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 50 * -i);
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        }

        GameObject.FindGameObjectsWithTag("ItemSlot")[0].GetComponent<ItemSlotManeger>().MouseOver();

        isFocused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFocused) return;
        if (Input.GetKeyDown("down") || Input.GetKeyDown("f"))
        {
            if (GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex])
            {
                GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseExit();
            }

            itemIndex = (itemIndex + 1) % items.Count;
            GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseOver();

            items = uiManager.GetComponent<UIManager>().items;
            RectTransform menuRectTransform = gameObject.GetComponent<RectTransform>();
            float viewPortHeight = FindObjectOfType<Canvas>().GetComponent<RectTransform>().sizeDelta.y - Mathf.Abs(menuRectTransform.offsetMax.y) - menuRectTransform.offsetMin.y;
            float scrollHeight = content.sizeDelta.y - GameObject.Find("Item Viewport").GetComponent<RectTransform>().sizeDelta.y;
            float scrollRoom = scrollHeight - viewPortHeight;
            float slotUnit = 50 / scrollRoom;
            float topOfViewPort = (1f - GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition) * scrollRoom;
            float bottomOfViewPort = (1f - GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition) * scrollRoom + viewPortHeight;
            if (!(itemIndex * 50 > topOfViewPort && itemIndex * 50 + 50 < bottomOfViewPort))
            {
                GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 1f - slotUnit * (itemIndex + 1) + viewPortHeight / scrollRoom;
            }
        }
        else if (Input.GetKeyDown("up") || Input.GetKeyDown("r"))
        {
            if (GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex])
            {
                GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseExit();
            }

            itemIndex = itemIndex - 1 >= 0 ? (itemIndex - 1) % items.Count : items.Count - 1;
            GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseOver();
            items = uiManager.GetComponent<UIManager>().items;
            RectTransform menuRectTransform = gameObject.GetComponent<RectTransform>();
            float viewPortHeight = FindObjectOfType<Canvas>().GetComponent<RectTransform>().sizeDelta.y - Mathf.Abs(menuRectTransform.offsetMax.y) - menuRectTransform.offsetMin.y;
            float scrollHeight = content.sizeDelta.y - GameObject.Find("Item Viewport").GetComponent<RectTransform>().sizeDelta.y;
            float scrollRoom = scrollHeight - viewPortHeight;
            float slotUnit = 50 / scrollRoom;
            float topOfViewPort = (1f - GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition) * scrollRoom;
            float bottomOfViewPort = (1f - GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition) * scrollRoom + viewPortHeight;
            if (!(itemIndex * 50 > topOfViewPort && itemIndex * 50 + 50 < bottomOfViewPort))
            {
                GameObject.Find("Item Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 1f - slotUnit * itemIndex;
            }
        }
        else if (Input.GetKeyDown("return"))
        {
            //GameObject itemInstence = Instantiate(commandSlot, commandPanel.transform.position, Quaternion.identity);
            //itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Use");
            //itemInstence.transform.SetParent(commandPanel.transform);
            //RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            //Debug.Log(rectTransform.offsetMax.y);
            //rectTransform.offsetMax = new Vector2(0, 0);
            //rectTransform.offsetMin = new Vector2(0, -50);
            //items[itemIndex].Use(playerController, gameObject);
            commandPanelManager.GenerateCommands();
            commandPanelManager.item = items[itemIndex];
            isFocused = false;
        }
    }
}
