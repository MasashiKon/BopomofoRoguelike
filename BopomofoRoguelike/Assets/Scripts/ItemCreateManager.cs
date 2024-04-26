using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCreateManager : MonoBehaviour
{
    public PrefabManager prefabManager;
    public UIManager uiManager;
    private bool[,] charTable = { { true, true, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, true, true, true, true, true, true, true },
    { true, false, true, true, true, false, false, true, false, false, true, true },
    { true, false, true, true, true, false, false, true, false, false, true, true }};
    private int[] coor = { 0, 11 };

    // Start is called before the first frame update
    void Start()
    {
        prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            coor[0] = (coor[0] - 1 + 4) % 4;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 4) % 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            coor[0] = (coor[0] + 1) % 4;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] + 1) % 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            coor[1] = (coor[1] - 1 + 12) % 12;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 4) % 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            coor[1] = (coor[1] + 1) % 12;
            while (!charTable[coor[0], coor[1]])
            {
                coor[0] = (coor[0] - 1 + 4) % 4;
            }
        }

        if (coor[0] == 0 && coor[1] == 11)
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
        else if (coor[0] == 1 && coor[1] == 11)
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
        else if (coor[0] == 2 && coor[1] == 11)
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
        else if (coor[0] == 3 && coor[1] == 11)
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
        else if (coor[0] == 0 && coor[1] == 10)
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
        else if (coor[0] == 1 && coor[1] == 10)
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
        else if (coor[0] == 2 && coor[1] == 10)
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
        else if (coor[0] == 3 && coor[1] == 10)
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
        else if (coor[0] == 0 && coor[1] == 9)
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
        else if (coor[0] == 1 && coor[1] == 9)
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
        else if (coor[0] == 2 && coor[1] == 9)
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
        else if (coor[0] == 0 && coor[1] == 8)
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
        else if (coor[0] == 1 && coor[1] == 8)
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
        else if (coor[0] == 2 && coor[1] == 8)
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
        else if (coor[0] == 0 && coor[1] == 7)
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
        else if (coor[0] == 1 && coor[1] == 7)
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
        else if (coor[0] == 2 && coor[1] == 7)
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
        else if (coor[0] == 3 && coor[1] == 7)
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
        else if (coor[0] == 0 && coor[1] == 6)
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
        else if (coor[0] == 1 && coor[1] == 6)
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
        else if (coor[0] == 2 && coor[1] == 6)
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
        else if (coor[0] == 0 && coor[1] == 5)
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
        else if (coor[0] == 1 && coor[1] == 5)
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
        else if (coor[0] == 2 && coor[1] == 5)
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
        else if (coor[0] == 0 && coor[1] == 4)
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
        else if (coor[0] == 1 && coor[1] == 4)
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
        else if (coor[0] == 2 && coor[1] == 4)
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
        else if (coor[0] == 3 && coor[1] == 4)
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
        else if (coor[0] == 0 && coor[1] == 3)
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
        else if (coor[0] == 1 && coor[1] == 3)
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
        else if (coor[0] == 2 && coor[1] == 3)
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
        else if (coor[0] == 3 && coor[1] == 3)
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
        else if (coor[0] == 0 && coor[1] == 2)
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
        else if (coor[0] == 1 && coor[1] == 2)
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
        else if (coor[0] == 2 && coor[1] == 2)
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
        else if (coor[0] == 3 && coor[1] == 2)
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
        else if (coor[0] == 0 && coor[1] == 1)
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
        else if (coor[0] == 0 && coor[1] == 0)
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
        else if (coor[0] == 1 && coor[1] == 0)
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
        else if (coor[0] == 2 && coor[1] == 0)
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
        else if (coor[0] == 3 && coor[1] == 0)
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
}
