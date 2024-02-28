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
    private string[] items;
    private GameObject currentItem;
    private int itemIndex = 0;
    private RectTransform content;

    // Start is called before the first frame update
    void Start()
    {
        items = uiManager.GetComponent<UIManager>().items;
        content = scrollPort.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, items.Length * 50);
        for (int i = 0; i < items.Length; i++)
        {
            GameObject itemInstence = Instantiate(itemSlot, gameObject.transform.position, Quaternion.identity);
            itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(items[i]);
            itemInstence.transform.SetParent(scrollPort.transform);
            RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 50 * -i);
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        }

        GameObject.FindGameObjectsWithTag("ItemSlot")[0].GetComponent<ItemSlotManeger>().MouseOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("f"))
        {
            if (GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex])
            {
                GameObject.FindGameObjectsWithTag("ItemSlot")[itemIndex].GetComponent<ItemSlotManeger>().MouseExit();
            }

            itemIndex = (itemIndex + 1) % items.Length;
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

            itemIndex = itemIndex - 1 >= 0 ? (itemIndex - 1) % items.Length : items.Length - 1;
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
    }
}
