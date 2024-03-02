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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            isPaused = !isPaused;
            if (!isPaused)
            {
                CommandPanelManager commandPanelManager = GameObject.Find("Command Panel").GetComponent<CommandPanelManager>();
                commandPanelManager.DestroyAllCommands();
                commandPanelManager.isFocused = false;
            }
            menuPanel.SetActive(!menuPanel.activeSelf);
            if (isPaused)
            {
                menuPanel.GetComponent<MenuManager>().RerenderItems();
            }
        }
    }
}
