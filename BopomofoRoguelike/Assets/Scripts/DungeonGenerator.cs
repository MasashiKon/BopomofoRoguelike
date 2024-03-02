using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    static public int dungeonSize = 51;

    public GameObject wall;
    public GameObject corridor;
    public GameObject start;
    public GameObject player;
    public GameObject enemy;
    public GameObject item;
    public int[,] field = new int[dungeonSize, dungeonSize];

    private TurnManager turnManager;
    private List<int[]> route = new List<int[]>();
    private List<int[]> availableCell = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();

        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                field[i, j] = 0;
            }
        }

        int[] startPoint = { Random.Range(0, dungeonSize - 2) + 1, Random.Range(0, dungeonSize - 2) + 1, };
        route.Add(startPoint);
        field[startPoint[0], startPoint[1]] = 2;

        bool diggingComplete = false;

        while (!diggingComplete)
        {
            List<int[]> directions = new List<int[]> { new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, };
            int progress = route.Count;
            do
            {
                int index = Random.Range(0, directions.Count);
                int[] direction = directions[index];
                directions.RemoveAt(index);
                int[] currentLoc = route[route.Count - 1];
                if (currentLoc[0] + direction[0] * 2 > 0 && currentLoc[0] + direction[0] * 2 < dungeonSize - 1 && currentLoc[1] + direction[1] * 2 > 0 && currentLoc[1] + direction[1] * 2 < dungeonSize - 1 && field[currentLoc[0] + direction[0] * 2, currentLoc[1] + direction[1] * 2] == 0)
                {
                    field[currentLoc[0] + direction[0], currentLoc[1] + direction[1]] = 1;
                    currentLoc = new int[] { currentLoc[0] + direction[0], currentLoc[1] + direction[1] };
                    route.Add(currentLoc);
                    availableCell.Add(currentLoc);
                    field[currentLoc[0] + direction[0], currentLoc[1] + direction[1]] = 1;
                    currentLoc = new int[] { currentLoc[0] + direction[0], currentLoc[1] + direction[1] };
                    route.Add(currentLoc);
                    availableCell.Add(currentLoc);
                    break;
                }
                else if (directions.Count == 0)
                {
                    break;
                }
            } while (true);

            if (progress == route.Count)
            {
                do
                {
                    route.RemoveAt(route.Count - 1);
                    if (route.Count < 1) break;
                    route.RemoveAt(route.Count - 1);
                    if (route.Count < 1) break;
                    int[] backTrace = { route[route.Count - 1][0], route[route.Count - 1][1] };
                    if (backTrace[0] - 2 > 0 && field[backTrace[0] - 2, backTrace[1]] == 0)
                    {
                        break;
                    }
                    else if (backTrace[0] + 2 < dungeonSize - 1 && field[backTrace[0] + 2, backTrace[1]] == 0)
                    {
                        break;
                    }
                    else if (backTrace[1] - 2 > 0 && field[backTrace[0], backTrace[1] - 2] == 0)
                    {
                        break;
                    }
                    else if (backTrace[1] + 2 < dungeonSize - 1 && field[backTrace[0], backTrace[1] + 2] == 0)
                    {
                        break;
                    }
                } while (route.Count > 0);

                if (route.Count == 0)
                {
                    diggingComplete = true;
                }
            };
        }



        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                if (field[i, j] == 0)
                {
                    Instantiate(wall, new Vector3(j - dungeonSize / 2, i * -1 + dungeonSize / 2, 0), Quaternion.identity);
                }
                else if (field[i, j] == 1)
                {
                    Instantiate(corridor, new Vector3(j - dungeonSize / 2, i * -1 + dungeonSize / 2, 0), Quaternion.identity);
                }
                else if (field[i, j] == 2)
                {
                    Instantiate(start, new Vector3(j - dungeonSize / 2, i * -1 + dungeonSize / 2, 0), Quaternion.identity);
                }

            }
        }

        for (int i = 0; i < 50; i++)
        {
            int randomIndex = Random.Range(0, availableCell.Count());
            GameObject itemInstance = Instantiate(item, new Vector3(availableCell[randomIndex][1] - dungeonSize / 2, availableCell[randomIndex][0] * -1 + dungeonSize / 2, -1), Quaternion.identity);
            itemInstance.GetComponent<ItemController>().pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
            turnManager.objectInfo[availableCell[randomIndex][0], availableCell[randomIndex][1]].Add(itemInstance);
        }

        for (int i = 0; i < 50; i++)
        {
            int randomIndex = Random.Range(0, availableCell.Count());
            GameObject enemyInstance = Instantiate(enemy, new Vector3(availableCell[randomIndex][1] - dungeonSize / 2, availableCell[randomIndex][0] * -1 + dungeonSize / 2, -1), Quaternion.identity);
            enemyInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256 / 255f), Random.Range(0, 156 / 155f) + 100);
            enemyInstance.GetComponent<EnemyController>().pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
            turnManager.objectInfo[availableCell[randomIndex][0], availableCell[randomIndex][1]].Add(enemyInstance);


            availableCell.RemoveAt(randomIndex);
        }

        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
