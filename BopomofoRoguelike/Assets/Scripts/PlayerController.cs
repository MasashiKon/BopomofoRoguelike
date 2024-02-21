using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerReady = false;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private int[] playerPosition;
    private TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();

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

        gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);

        turnManager.ProcessTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.isReadyNextMove)
        {
            if (Input.GetKey("up"))
            {
                if (field[playerPosition[0] - 1, playerPosition[1]] != 0)
                {
                    playerPosition[0]--;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    turnManager.ProcessTurn();
                }

            }
            else if (Input.GetKey("down"))
            {
                if (field[playerPosition[0] + 1, playerPosition[1]] != 0)
                {
                    playerPosition[0]++;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    turnManager.ProcessTurn();
                }

            }
            else if (Input.GetKey("left"))
            {
                if (field[playerPosition[0], playerPosition[1] - 1] != 0)
                {
                    playerPosition[1]--;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    turnManager.ProcessTurn();
                }

            }
            else if (Input.GetKey("right"))
            {
                if (field[playerPosition[0], playerPosition[1] + 1] != 0)
                {
                    playerPosition[1]++;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    turnManager.ProcessTurn();
                }

            }
        }

    }
}
