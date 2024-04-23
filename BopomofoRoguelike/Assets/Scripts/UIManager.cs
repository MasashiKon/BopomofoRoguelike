using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    [SerializeField] GameObject itemCreatePanel;
    public List<GameObject> items = new List<GameObject>();
    public bool isFreeze = false;
    public bool isPaused = false;
    public GameObject stairPanel;

    // Start is called before the first frame update
    void Start()
    {
        SceneReloader sceneReloader = GameObject.Find("Scene Reloader").GetComponent<SceneReloader>();
        PrefabManager prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();

        for (int i = 0; i < sceneReloader.items.Count; i++)
        {
            if (sceneReloader.items[i].name == "Item")
            {
                GameObject item = Instantiate(prefabManager.items[0]);
                items.Add(item);
            }
            else if (sceneReloader.items[i].name == "Herb")
            {
                GameObject item = Instantiate(prefabManager.items[1]);
                items.Add(item);
            }
            else if (sceneReloader.items[i].name == "Sword")
            {
                GameObject item = Instantiate(prefabManager.items[2]);
                items.Add(item);
                if (sceneReloader.items[i].isEquiped)
                {
                    item.GetComponent<Item>().EquipWithoutText(i);
                }
            }
            else if (sceneReloader.items[i].name == "Shield")
            {
                GameObject item = Instantiate(prefabManager.items[3]);
                items.Add(item);
                if (sceneReloader.items[i].isEquiped)
                {
                    item.GetComponent<Item>().EquipWithoutText(i);
                }
            }

        }

        while (sceneReloader.items.Count > 0)
        {
            sceneReloader.items.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFreeze) return;
        if (Input.GetKeyDown("tab"))
        {
            isPaused = !isPaused;
            if (!isPaused && GameObject.Find("Command Panel"))
            {
                CommandPanelManager commandPanelManager = GameObject.Find("Command Panel").GetComponent<CommandPanelManager>();
                commandPanelManager.DestroyAllCommands();
                commandPanelManager.isFocused = false;
            }
            if (itemCreatePanel.activeSelf)
            {
                isPaused = !isPaused;
                itemCreatePanel.SetActive(false);
            }
            menuPanel.SetActive(!menuPanel.activeSelf);
            if (isPaused)
            {
                menuPanel.GetComponent<MenuManager>().RerenderItems();
            }
        }
        else if (Input.GetKeyDown("r"))
        {
            isPaused = !isPaused;
            if (!isPaused && GameObject.Find("Command Panel"))
            {
                CommandPanelManager commandPanelManager = GameObject.Find("Command Panel").GetComponent<CommandPanelManager>();
                commandPanelManager.DestroyAllCommands();
                commandPanelManager.isFocused = false;
            }
            if (menuPanel.activeSelf)
            {
                isPaused = !isPaused;
                menuPanel.SetActive(false);
            }
            itemCreatePanel.SetActive(!itemCreatePanel.activeSelf);
        }
    }
}
