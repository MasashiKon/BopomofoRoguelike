using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairManager : MonoBehaviour
{
    public bool isFreeze = false;
    private PlayerController playerController;
    private TurnManager turnManager;
    private SceneReloader sceneReloader;
    private UIManager uiManager;
    private int commandIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        sceneReloader = GameObject.Find("Scene Reloader").GetComponent<SceneReloader>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        uiManager.isFreeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            ChangeColorDefault();
            commandIndex = Mathf.Abs(commandIndex - 1);
            ChangeColorSelected();
        }
        else if (Input.GetKeyDown("down"))
        {
            ChangeColorDefault();
            commandIndex = (commandIndex + 1) % 2;
            ChangeColorSelected();
        }
        else if (Input.GetKeyDown("return"))
        {
            if (commandIndex == 0)
            {
                sceneReloader.playerHP = playerController.hp;
                sceneReloader.floor++;
                for (int i = 0; i < uiManager.items.Count; i++)
                {
                    Item item = uiManager.items[i].GetComponent<Item>();
                    sceneReloader.items.Add(new ItemParameter(item.name.Replace("(Clone)", ""), item.isEquiped));
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (commandIndex == 1)
            {
                uiManager.isFreeze = false;
                turnManager.ProcessTurn();
                gameObject.SetActive(false);
            }
        }
    }

    private void ChangeColorSelected()
    {
        gameObject.transform.GetChild(commandIndex).transform.GetComponent<Image>().color = new Color(255 / 255f, 200 / 255f, 200 / 255f, 220 / 255f);
    }
    private void ChangeColorDefault()
    {
        gameObject.transform.GetChild(commandIndex).transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
    }
}
