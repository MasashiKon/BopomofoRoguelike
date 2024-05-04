using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;

public class ItemCreateManager : MonoBehaviour
{
    public PrefabManager prefabManager;
    public UIManager uiManager;
    public TurnManager turnManager;
    private bool[,] charTable = { { true, true, true, true, true, true, true, true, true, true, true, true },
        { true, true, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, false, false, true, false, false, true, true },
    { true, false, true, true, true, false, false, true, false, false, true, true }};
    private int[] coor = { 1, 11 };
    private int fontSizeDefault = 36;
    private int fontSizePressed = 25;
    private bool isSomeKeyPressed = false;
    private string memo = "選定 一聲 刪除";
    private bool isConverting = false;
    private TextMeshProUGUI input;

    // Start is called before the first frame update
    void Start()
    {
        prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        input = GameObject.Find("Zhuyin Input").GetComponent<TextMeshProUGUI>();
        coor[0] = 1;
        coor[1] = 11;
        foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
        {
            TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
            if (tmp.text == "ㄅ")
            {
                tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isSomeKeyPressed)
        {
            coor[0] = (coor[0] - 1 + 5) % 5;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 5) % 5;
            }
            ChangeSelectedChar();
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isSomeKeyPressed)
        {
            coor[0] = (coor[0] + 1) % 5;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] + 1) % 5;
            }
            ChangeSelectedChar();
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isSomeKeyPressed)
        {

            if (coor[0] == 0 && (coor[1] == 10 || coor[1] == 9 || coor[1] == 8 || coor[1] == 7 || coor[1] == 6))
            {
                coor[1] = 1;
            }
            else if (coor[0] == 0 && (coor[1] == 5 || coor[1] == 4 || coor[1] == 3 || coor[1] == 2 || coor[1] == 1 || coor[1] == 0))
            {
                coor[1] = 11;
            }
            else
            {
                coor[1] = (coor[1] - 1 + 12) % 12;
            }
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 5) % 5;
            }
            ChangeSelectedChar();
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isSomeKeyPressed)
        {

            if (coor[0] == 0 && coor[1] == 11)
            {
                coor[1] = 1;
            }
            else if (coor[0] == 0 && (coor[1] == 10 || coor[1] == 9 || coor[1] == 8 || coor[1] == 7 || coor[1] == 6))
            {
                coor[1] = 11;
            }
            else if (coor[0] == 0 && (coor[1] == 5 || coor[1] == 4 || coor[1] == 3 || coor[1] == 2 || coor[1] == 1 || coor[1] == 0))
            {
                coor[1] = 10;
            }
            else
            {
                coor[1] = (coor[1] + 1) % 12;
            }
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 5) % 5;
            }
            ChangeSelectedChar();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            isSomeKeyPressed = true;
            if (coor[0] == 0 && coor[1] == 11)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "決定")
                    {
                        tmp.fontSize = fontSizePressed;
                        if (input.text == "刀")
                        {
                            uiManager.items.Add(Instantiate(prefabManager.items[2], new Vector3(26, 26, 0), Quaternion.identity));
                            CloseManager();
                        }
                        else if (input.text == "盾")
                        {
                            uiManager.items.Add(Instantiate(prefabManager.items[3], new Vector3(26, 26, 0), Quaternion.identity));
                            CloseManager();
                        }
                        else if (input.text == "草")
                        {
                            uiManager.items.Add(Instantiate(prefabManager.items[1], new Vector3(26, 26, 0), Quaternion.identity));
                            CloseManager();
                        }
                    }
                }
            }
            else if (coor[0] == 0 && (coor[1] == 10 || coor[1] == 9 || coor[1] == 8 || coor[1] == 7 || coor[1] == 6))
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "一声")
                    {
                        tmp.fontSize = fontSizePressed;
                        if (input.text == "ㄉㄠ")
                        {
                            input.text = "刀";
                        }
                    }
                    else if (tmp.text == "変換")
                    {
                        tmp.fontSize = fontSizePressed;
                        if (input.text == "ㄉㄨㄣˋ")
                        {
                            input.text = "盾";
                        }
                        else if (input.text == "ㄘㄠˇ")
                        {
                            input.text = "草";

                        }

                    }
                }
            }
            else if (coor[0] == 0 && (coor[1] == 5 || coor[1] == 4 || coor[1] == 3 || coor[1] == 2 || coor[1] == 1 || coor[1] == 0))
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "削除")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                if (input.text.Length > 0)
                {
                    input.text = input.text.Remove(input.text.Length - 1);
                }
            }
            else if (coor[0] == 1 && coor[1] == 11)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄅ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄅ";
            }
            else if (coor[0] == 2 && coor[1] == 11)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄆ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄆ";
            }
            else if (coor[0] == 3 && coor[1] == 11)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄇ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄇ";
            }
            else if (coor[0] == 4 && coor[1] == 11)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄈ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄈ";
            }
            else if (coor[0] == 1 && coor[1] == 10)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄉ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄉ";
            }
            else if (coor[0] == 2 && coor[1] == 10)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄊ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄊ";
            }
            else if (coor[0] == 3 && coor[1] == 10)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄋ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄋ";
            }
            else if (coor[0] == 4 && coor[1] == 10)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄌ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄌ";
            }
            else if (coor[0] == 1 && coor[1] == 9)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄍ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄍ";
            }
            else if (coor[0] == 2 && coor[1] == 9)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄎ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄎ";
            }
            else if (coor[0] == 3 && coor[1] == 9)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄏ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄏ";
            }
            else if (coor[0] == 1 && coor[1] == 8)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄐ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄐ";
            }
            else if (coor[0] == 2 && coor[1] == 8)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄑ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄑ";
            }
            else if (coor[0] == 3 && coor[1] == 8)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄒ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄒ";
            }
            else if (coor[0] == 1 && coor[1] == 7)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄓ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄓ";
            }
            else if (coor[0] == 2 && coor[1] == 7)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄔ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄔ";
            }
            else if (coor[0] == 3 && coor[1] == 7)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄕ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄕ";
            }
            else if (coor[0] == 4 && coor[1] == 7)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄖ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄖ";
            }
            else if (coor[0] == 1 && coor[1] == 6)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄗ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄗ";
            }
            else if (coor[0] == 2 && coor[1] == 6)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄘ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄘ";
            }
            else if (coor[0] == 3 && coor[1] == 6)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄙ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄙ";
            }
            else if (coor[0] == 1 && coor[1] == 5)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄧ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄧ";
            }
            else if (coor[0] == 2 && coor[1] == 5)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄨ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄨ";
            }
            else if (coor[0] == 3 && coor[1] == 5)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄩ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄩ";
            }
            else if (coor[0] == 1 && coor[1] == 4)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄚ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄚ";
            }
            else if (coor[0] == 2 && coor[1] == 4)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄛ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄛ";
            }
            else if (coor[0] == 3 && coor[1] == 4)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄜ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄜ";
            }
            else if (coor[0] == 4 && coor[1] == 4)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄝ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄝ";
            }
            else if (coor[0] == 1 && coor[1] == 3)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄞ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄞ";
            }
            else if (coor[0] == 2 && coor[1] == 3)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄟ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄟ";
            }
            else if (coor[0] == 3 && coor[1] == 3)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄠ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄠ";
            }
            else if (coor[0] == 4 && coor[1] == 3)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄡ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄡ";
            }
            else if (coor[0] == 1 && coor[1] == 2)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄢ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄢ";
            }
            else if (coor[0] == 2 && coor[1] == 2)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄣ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄣ";
            }
            else if (coor[0] == 3 && coor[1] == 2)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄤ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄤ";
            }
            else if (coor[0] == 4 && coor[1] == 2)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄥ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄥ";
            }
            else if (coor[0] == 1 && coor[1] == 1)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ㄦ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ㄦ";
            }
            else if (coor[0] == 1 && coor[1] == 0)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ˊ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ˊ";
            }
            else if (coor[0] == 2 && coor[1] == 0)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ˇ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ˇ";
            }
            else if (coor[0] == 3 && coor[1] == 0)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "ˋ")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "ˋ";
            }
            else if (coor[0] == 4 && coor[1] == 0)
            {
                foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
                {
                    TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                    if (tmp.text == "˙")
                    {
                        tmp.fontSize = fontSizePressed;
                    }
                }
                input.text += "˙";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            isSomeKeyPressed = false;
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (coor[0] == 0 && (coor[1] == 10 || coor[1] == 9 || coor[1] == 8 || coor[1] == 7 || coor[1] == 6))
                {
                    if (tmp.text == "一声" && input.text.Length > 0)
                    {
                        tmp.text = "変換";
                    }
                }
                else if (coor[0] == 1 && coor[1] == 0 || coor[0] == 2 && coor[1] == 0 || coor[0] == 3 && coor[1] == 0 || coor[0] == 4 && coor[1] == 0)
                {
                    if (tmp.text == "一声" && input.text.Length > 0)
                    {
                        tmp.text = "変換";
                    }
                }
                if (tmp.text == "変換" && input.text.Length == 0)
                {
                    tmp.text = "一声";
                }
                tmp.fontSize = fontSizeDefault;
            }
        }
    }

    void ChangeSelectedChar()
    {
        if (coor[0] == 0 && coor[1] == 11)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "決定")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 0 && (coor[1] == 10 || coor[1] == 9 || coor[1] == 8 || coor[1] == 7 || coor[1] == 6))
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "一声" || tmp.text == "変換")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 0 && (coor[1] == 5 || coor[1] == 4 || coor[1] == 3 || coor[1] == 2 || coor[1] == 1 || coor[1] == 0))
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "削除")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 11)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄅ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 11)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄆ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 11)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄇ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 11)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄈ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 10)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄉ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 10)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄊ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 10)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄋ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 10)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄌ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 9)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄍ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 9)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄎ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 9)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄏ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 8)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄐ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 8)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄑ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 8)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄒ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 7)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄓ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 7)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄔ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 7)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄕ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 7)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄖ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 6)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄗ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 6)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄘ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 6)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄙ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 5)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄧ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 5)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄨ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 5)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄩ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 4)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄚ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 4)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄛ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 4)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄜ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 4)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄝ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 3)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄞ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 3)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄟ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 3)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄠ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 3)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄡ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 2)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄢ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 2)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄣ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 2)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄤ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 2)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄥ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 1)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ㄦ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 1 && coor[1] == 0)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ˊ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 2 && coor[1] == 0)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ˇ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 3 && coor[1] == 0)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "ˋ")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
        else if (coor[0] == 4 && coor[1] == 0)
        {
            foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
            {
                TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
                if (tmp.text == "˙")
                {
                    tmp.fontStyle = FontStyles.Bold | FontStyles.Italic;
                }
                else
                {
                    tmp.fontStyle = FontStyles.Normal;
                }
            }
        }
    }
    void CloseManager()
    {
        input.text = "";
        isSomeKeyPressed = false;
        foreach (GameObject bopomofo in GameObject.FindGameObjectsWithTag("Bopomofo"))
        {
            TextMeshProUGUI tmp = bopomofo.GetComponent<TextMeshProUGUI>();
            tmp.fontStyle = FontStyles.Normal;
            tmp.fontSize = fontSizeDefault;
            if (tmp.text == "変換")
            {
                tmp.text = "一声";
            }
        }
        gameObject.SetActive(false);
        uiManager.isPaused = false;
        turnManager.ProcessTurn();
    }
}
