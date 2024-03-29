using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairManager : MonoBehaviour
{
    private PlayerController playerController;
    private TurnManager turnManager;
    private int commandIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player Wrapper").GetComponent<PlayerController>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        ChangeColorSelected();
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

            }
            else if (commandIndex == 1)
            {
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
