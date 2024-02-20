using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private int[] playerPosition;
    private bool inputEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        field = dungeonGenerator.field;
        for (int i = 0; i < DungeonGenerator.dungeonSize; i++)
        {
            for (int j = 0; j < DungeonGenerator.dungeonSize; j++)
            {
                if (field[i, j] == 2)
                {
                    playerPosition = new int[] { i, j };
                }
            }
        }

        gameObject.transform.position = new Vector2(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKey("up"))
            {
                if (field[playerPosition[0] - 1, playerPosition[1]] != 0)
                {
                    playerPosition[0]--;
                    gameObject.transform.position = new Vector2(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2);
                }

            }
            else if (Input.GetKey("down"))
            {
                if (field[playerPosition[0] + 1, playerPosition[1]] != 0)
                {
                    playerPosition[0]++;
                    gameObject.transform.position = new Vector2(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2);
                }

            }
            else if (Input.GetKey("left"))
            {
                if (field[playerPosition[0], playerPosition[1] - 1] != 0)
                {
                    playerPosition[1]--;
                    gameObject.transform.position = new Vector2(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2);
                }

            }
            else if (Input.GetKey("right"))
            {
                if (field[playerPosition[0], playerPosition[1] + 1] != 0)
                {
                    playerPosition[1]++;
                    gameObject.transform.position = new Vector2(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2);
                }

            }
            DisableInputForSeconds(0.1f);
        }

    }

    // 入力を無効にするメソッド
    void DisableInputForSeconds(float seconds)
    {
        inputEnabled = false;
        StartCoroutine(EnableInputAfterDelay(seconds));
    }

    // 一定時間後に入力を再度有効にするコルーチン
    IEnumerator EnableInputAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        inputEnabled = true;
    }
}
