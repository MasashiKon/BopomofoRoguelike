using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public List<GameObject> items = new List<GameObject>();
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
            if (!isPaused && GameObject.Find("Command Panel"))
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
