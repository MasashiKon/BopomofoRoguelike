using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanelManager : MonoBehaviour
{
    public GameObject commandSlot;
    public GameObject item;
    public int? itemIndex;
    public UIManager uiManager;
    public bool isFocused = false;
    private PlayerController playerController;
    private GameObject menu;
    private List<GameObject> commandSlots = new List<GameObject>();
    private int commandIndex;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        menu = GameObject.Find("Menu Panel");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFocused) return;
        if (Input.GetKeyDown("return") && item != null && itemIndex != null)
        {
            if (commandSlots[commandIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == Item.GetCommandTranslation(Commands.Use, Language.Ja))
            {
                menu.GetComponent<MenuManager>().isFocused = true;
                GameObject.Find("UI Manager").GetComponent<UIManager>().isPaused = false;
                isFocused = false;
                item.GetComponent<Item>().Use(playerController, menu, (int)itemIndex);
            }
            else if (commandSlots[commandIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == Item.GetCommandTranslation(Commands.Dispose, Language.Ja))
            {
                item.GetComponent<Item>().Dispose(menu, (int)itemIndex);
                StartCoroutine(WaitOneFrame());
            }
            else if (commandSlots[commandIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == Item.GetCommandTranslation(Commands.Put, Language.Ja))
            {
                item.GetComponent<Item>().Put(menu, (int)itemIndex);
                StartCoroutine(WaitOneFrame());
            }
            else if (commandSlots[commandIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == Item.GetCommandTranslation(Commands.Throw, Language.Ja))
            {
                menu.GetComponent<MenuManager>().isFocused = true;
                GameObject.Find("UI Manager").GetComponent<UIManager>().isPaused = false;
                isFocused = false;
                item.GetComponent<Item>().Throw(menu, (int)itemIndex, playerController.transform.rotation.eulerAngles.z);

            }
            item = null;
            itemIndex = null;
            commandIndex = 0;
            commandSlots.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else if (Input.GetKeyDown("up"))
        {
            commandSlots[commandIndex].GetComponent<CommandSlotManager>().MouseExit();
            commandIndex = commandIndex - 1 >= 0 ? commandIndex - 1 : commandSlots.Count - 1;
            commandSlots[commandIndex].GetComponent<CommandSlotManager>().MouseOver();
        }
        else if (Input.GetKeyDown("down"))
        {
            commandSlots[commandIndex].GetComponent<CommandSlotManager>().MouseExit();
            commandIndex = (commandIndex + 1) % commandSlots.Count;
            commandSlots[commandIndex].GetComponent<CommandSlotManager>().MouseOver();
        }
        else if (Input.GetKeyDown("left"))
        {
            DestroyAllCommands();
            item = null;
            itemIndex = null;
            commandIndex = 0;
            commandSlots.Clear();
            isFocused = false;
            menu.GetComponent<MenuManager>().isFocused = true;
        }
    }

    public void GenerateCommands()
    {
        for (int i = 0; i < item.GetComponent<Item>().GetCommands().Length; i++)
        {
            GameObject itemInstence = Instantiate(commandSlot, transform.position, Quaternion.identity);
            if (item.GetComponent<Item>().GetCommands()[i] == Commands.Use)
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Item.GetCommandTranslation(Commands.Use, Language.Ja));
            }
            else if (item.GetComponent<Item>().GetCommands()[i] == Commands.Dispose)
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Item.GetCommandTranslation(Commands.Dispose, Language.Ja));
            }
            else if (item.GetComponent<Item>().GetCommands()[i] == Commands.Put)
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Item.GetCommandTranslation(Commands.Put, Language.Ja));
            }
            else if (item.GetComponent<Item>().GetCommands()[i] == Commands.Throw)
            {
                itemInstence.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Item.GetCommandTranslation(Commands.Throw, Language.Ja));
            }
            itemInstence.transform.SetParent(transform);
            RectTransform rectTransform = itemInstence.GetComponent<RectTransform>();
            rectTransform.offsetMax = new Vector2(0, i * -50);
            rectTransform.offsetMin = new Vector2(0, i * -50);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 50);
            commandSlots.Add(itemInstence);
        }
        commandSlots[0].GetComponent<CommandSlotManager>().MouseOver();
        commandIndex = 0;
        isFocused = true;
    }

    public void DestroyAllCommands()
    {
        item = null;
        itemIndex = null;
        commandIndex = 0;
        commandSlots.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();
        isFocused = false;
    }
}
