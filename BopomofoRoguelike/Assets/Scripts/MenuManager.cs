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
    public List<GameObject> items;
    private GameObject currentItem;
    public int itemIndex = 0;
    private RectTransform content;
    private PlayerController playerController;
    private CommandPanelManager commandPanelManager;
    private bool firstActive = true;

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
            itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(items[i].GetComponent<Item>().GetNameTranslation(Language.Ja));
            itemInstence.transform.SetParent(scrollPort.transform);
            RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 50 * -i);
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        }

        if (GameObject.FindGameObjectsWithTag("ItemSlot").Length != 0)
        {
            GameObject.FindGameObjectsWithTag("ItemSlot")[0].GetComponent<ItemSlotManeger>().MouseOver();
        }

        isFocused = true;
        firstActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFocused || items.Count <= 0) return;
        if (Input.GetKeyDown("down") || Input.GetKeyDown("f"))
        {
            if (GameObject.FindGameObjectsWithTag("ItemSlot").Length != 0)
            {
                GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseExit();
            }

            itemIndex = (itemIndex + 1) % items.Count;
            GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseOver();

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
        else if ((Input.GetKeyDown("return") || Input.GetKeyDown("right")) && !commandPanelManager.isFocused)
        {
            commandPanelManager.DestroyAllCommands();
            commandPanelManager.item = items[itemIndex];
            commandPanelManager.itemIndex = itemIndex;
            commandPanelManager.GenerateCommands();
            isFocused = false;
        }
    }

    public void RerenderItems()
    {
        if (firstActive) return;
        for (int i = 0; i < scrollPort.transform.childCount; i++)
        {
            Destroy(scrollPort.transform.GetChild(i).gameObject);
        }

        content.sizeDelta = new Vector2(content.sizeDelta.x, items.Count * 50);
        if (items.Count <= 0) return;
        for (int i = 0; i < items.Count; i++)
        {
            GameObject itemInstence = Instantiate(itemSlot, gameObject.transform.position, Quaternion.identity);
            Item targetItem = items[i].GetComponent<Item>(); 
            if (!targetItem.isEquiped)
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(targetItem.GetNameTranslation(Language.Ja));
            }
            else
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(targetItem.GetNameTranslation(Language.Ja) + " " + targetItem.GetEquipTranslation(Language.Ja));
            }
            itemInstence.transform.SetParent(scrollPort.transform);
            RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 50 * -i);
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        }

        StartCoroutine(WaitAndChangeColor());

        isFocused = true;
    }

    IEnumerator WaitAndChangeColor()
    {
        yield return new WaitForEndOfFrame();
        if (GameObject.FindGameObjectsWithTag("ItemSlot").Length != 0)
        {
            GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseOver();
        }
    }
}
