using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    static public int dungeonSize = 51;

    public GameObject wall;
    public GameObject corridor;
    public GameObject room;
    public GameObject player;
    public GameObject enemy;
    public int[,] field = new int[dungeonSize, dungeonSize];

    private TurnManager turnManager;
    private PrefabManager prefabManager;
    private List<int[]> route = new List<int[]>();
    private List<int[]> availableCell = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();

        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                field[i, j] = 0;
            }
        }

        DivideArea(0, 0, dungeonSize, dungeonSize);

        //int[] startPoint = { Random.Range(0, dungeonSize - 2) + 1, Random.Range(0, dungeonSize - 2) + 1, };
        //route.Add(startPoint);
        //field[startPoint[0], startPoint[1]] = 2;

        //bool diggingComplete = false;

        //while (!diggingComplete)
        //{
        //    List<int[]> directions = new List<int[]> { new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, };
        //    int progress = route.Count;
        //    do
        //    {
        //        int index = Random.Range(0, directions.Count);
        //        int[] direction = directions[index];
        //        directions.RemoveAt(index);
        //        int[] currentLoc = route[route.Count - 1];
        //        if (currentLoc[0] + direction[0] * 2 > 0 && currentLoc[0] + direction[0] * 2 < dungeonSize - 1 && currentLoc[1] + direction[1] * 2 > 0 && currentLoc[1] + direction[1] * 2 < dungeonSize - 1 && field[currentLoc[0] + direction[0] * 2, currentLoc[1] + direction[1] * 2] == 0)
        //        {
        //            field[currentLoc[0] + direction[0], currentLoc[1] + direction[1]] = 1;
        //            currentLoc = new int[] { currentLoc[0] + direction[0], currentLoc[1] + direction[1] };
        //            route.Add(currentLoc);
        //            availableCell.Add(currentLoc);
        //            field[currentLoc[0] + direction[0], currentLoc[1] + direction[1]] = 1;
        //            currentLoc = new int[] { currentLoc[0] + direction[0], currentLoc[1] + direction[1] };
        //            route.Add(currentLoc);
        //            availableCell.Add(currentLoc);
        //            break;
        //        }
        //        else if (directions.Count == 0)
        //        {
        //            break;
        //        }
        //    } while (true);

        //    if (progress == route.Count)
        //    {
        //        do
        //        {
        //            route.RemoveAt(route.Count - 1);
        //            if (route.Count < 1) break;
        //            route.RemoveAt(route.Count - 1);
        //            if (route.Count < 1) break;
        //            int[] backTrace = { route[route.Count - 1][0], route[route.Count - 1][1] };
        //            if (backTrace[0] - 2 > 0 && field[backTrace[0] - 2, backTrace[1]] == 0)
        //            {
        //                break;
        //            }
        //            else if (backTrace[0] + 2 < dungeonSize - 1 && field[backTrace[0] + 2, backTrace[1]] == 0)
        //            {
        //                break;
        //            }
        //            else if (backTrace[1] - 2 > 0 && field[backTrace[0], backTrace[1] - 2] == 0)
        //            {
        //                break;
        //            }
        //            else if (backTrace[1] + 2 < dungeonSize - 1 && field[backTrace[0], backTrace[1] + 2] == 0)
        //            {
        //                break;
        //            }
        //        } while (route.Count > 0);

        //        if (route.Count == 0)
        //        {
        //            diggingComplete = true;
        //        }
        //    };
        //}



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
                    Instantiate(room, new Vector3(j - dungeonSize / 2, i * -1 + dungeonSize / 2, 0), Quaternion.identity);
                }

            }
        }

        //for (int i = 0; i < 150; i++)
        //{
        //    int randomIndex = Random.Range(0, availableCell.Count());
        //    int randomItemIndex = Random.Range(0, prefabManager.items.Length);
        //    GameObject itemInstance = Instantiate(prefabManager.items[randomItemIndex], new Vector3(availableCell[randomIndex][1] - dungeonSize / 2, availableCell[randomIndex][0] * -1 + dungeonSize / 2, -1), prefabManager.items[randomItemIndex].transform.rotation);
        //    itemInstance.GetComponent<ItemController>().pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
        //    turnManager.objectInfo[availableCell[randomIndex][0], availableCell[randomIndex][1]].Add(itemInstance);
        //}

        //for (int i = 0; i < 50; i++)
        //{
        //    int randomIndex = Random.Range(0, availableCell.Count());
        //    GameObject enemyInstance = Instantiate(enemy, new Vector3(availableCell[randomIndex][1] - dungeonSize / 2, availableCell[randomIndex][0] * -1 + dungeonSize / 2, -1), Quaternion.identity);
        //    enemyInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256 / 255f), Random.Range(0, 156 / 155f) + 100);
        //    enemyInstance.GetComponent<EnemyController>().pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
        //    turnManager.objectInfo[availableCell[randomIndex][0], availableCell[randomIndex][1]].Add(enemyInstance);


        //    availableCell.RemoveAt(randomIndex);
        //}

        //player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int[] DivideArea(int startX, int startY, int endX, int endY)
    {
        int[] CreateBox()
        {
            int box1pointX1;
            int box1pointX2;
            do
            {
                box1pointX1 = Random.Range(startX + 1, startX + 4);
                box1pointX2 = Random.Range(endX - 4, endX - 1);
            } while (box1pointX1 == box1pointX2);

            int box1pointY1;
            int box1pointY2;
            do
            {
                box1pointY1 = Random.Range(startY + 1, startY + 4);
                box1pointY2 = Random.Range(endY - 4, endY - 1);
            } while (box1pointY1 == box1pointY2);

            if (box1pointY2 - box1pointY1 > 0)
            {
                if (box1pointX2 - box1pointX1 > 0)
                {
                    for (int i = box1pointY1; i <= box1pointY2; i++)
                    {
                        for (int j = box1pointX1; j <= box1pointX2; j++)
                        {
                            field[i, j] = 2;
                        }
                    }
                    return new int[] { box1pointX1, box1pointY1, box1pointX2, box1pointY2 };
                }
                else
                {
                    for (int i = box1pointY1; i <= box1pointY2; i++)
                    {
                        for (int j = box1pointX2; j <= box1pointX1; j++)
                        {
                            field[i, j] = 2;
                        }
                    }
                    return new int[] { box1pointX2, box1pointY1, box1pointX1, box1pointY2 };
                }
            }
            else
            {
                if (box1pointX2 - box1pointX1 > 0)
                {
                    for (int i = box1pointY2; i <= box1pointY1; i++)
                    {
                        for (int j = box1pointX1; j <= box1pointX2; j++)
                        {
                            field[i, j] = 2;
                        }
                    }
                    return new int[] { box1pointX1, box1pointY2, box1pointX2, box1pointY1 };
                }
                else
                {
                    for (int i = box1pointY2; i <= box1pointY1; i++)
                    {
                        for (int j = box1pointX2; j <= box1pointX1; j++)
                        {
                            field[i, j] = 2;
                        }
                    }
                    return new int[] { box1pointX2, box1pointY2, box1pointX1, box1pointY1 };
                }
            }
        }

        if (endX - startX < 8 || endY - startY < 8)
        {
            return CreateBox();
        }

        if (endX - startX < 20 || endY - startY < 20)
        {
            int makeRoomOrDivideMore = Random.Range(0, 3);

            if (makeRoomOrDivideMore == 0)
            {
                return CreateBox();
            }
            else
            {
                int horOrVer = Random.Range(0, 2);

                if (horOrVer == 0)
                {
                    int divisionLine = Random.Range((endY - startY) / 2 + startY - 2, (endY - startY) / 2 + startY + 3);

                    if (divisionLine - startY < 8 || endY - divisionLine < 8)
                    {
                        return CreateBox();
                    }
                    else
                    {
                        int[] box1 = DivideArea(startX, startY, endX, divisionLine - 1);
                        int[] box2 = DivideArea(startX, divisionLine + 1, endX, endY);

                        int minX = box2[0] - box1[0] > 0 ? box1[0] : box2[0];
                        int maxX = box2[2] - box1[2] > 0 ? box2[2] : box1[2];

                        int alleyPoint1 = Random.Range(box1[0], box1[2]);
                        int box1Int = divisionLine - 1;
                        List<int> availableLines1 = new List<int> { };

                        for (int i = 0; i < box1[2] - box1[0]; i++)
                        {
                            availableLines1.Add(box1[0] + i);
                        }

                        while (field[box1Int, alleyPoint1] == 0)
                        {
                            if (field[box1Int, alleyPoint1 - 1] != 0 || field[box1Int, alleyPoint1 + 1] != 0)
                            {
                                availableLines1.Remove(alleyPoint1);
                                alleyPoint1 = availableLines1[Random.Range(0, availableLines1.Count)];
                                box1Int = divisionLine - 1;
                                continue;
                            }
                            box1Int--;
                        }

                        box1Int = divisionLine - 1;

                        while (field[box1Int, alleyPoint1] == 0)
                        {
                            field[box1Int, alleyPoint1] = 1;
                            box1Int--;
                        }

                        int alleyPoint2 = Random.Range(box2[0], box2[2]);
                        int box2Int = divisionLine;
                        List<int> availableLines2 = new List<int> { };

                        for (int i = 0; i < box2[2] - box2[0]; i++)
                        {
                            availableLines2.Add(box2[0] + i);
                        }

                        while (field[box2Int, alleyPoint2] == 0)
                        {
                            if (field[box2Int, alleyPoint2 - 1] != 0 || field[box2Int, alleyPoint2 + 1] != 0)
                            {
                                availableLines2.Remove(alleyPoint2);
                                alleyPoint2 = availableLines2[Random.Range(0, availableLines2.Count)];
                                box2Int = divisionLine;
                                continue;
                            }
                            box2Int++;
                        }

                        box2Int = divisionLine;

                        while (field[box2Int, alleyPoint2] == 0)
                        {
                            field[box2Int, alleyPoint2] = 1;
                            box2Int++;
                        }

                        if (alleyPoint2 - alleyPoint1 > 0)
                        {
                            for (int i = alleyPoint1; i <= alleyPoint2; i++)
                            {
                                field[divisionLine, i] = 1;
                            }
                        }
                        else if (alleyPoint2 - alleyPoint1 < 0)
                        {
                            for (int i = alleyPoint2; i <= alleyPoint1; i++)
                            {
                                field[divisionLine, i] = 1;
                            }
                        }

                        return new int[] { minX, box1[1], maxX, box2[3] };
                    }
                }
                else
                {
                    int divisionLine = Random.Range((endX - startX) / 2 + startX - 2, (endX - startX) / 2 + startX + 3);

                    if (divisionLine - startX < 8 || endX - divisionLine < 8)
                    {
                        return CreateBox();
                    }
                    else
                    {
                        int[] box1 = DivideArea(startX, startY, divisionLine - 1, endY);
                        int[] box2 = DivideArea(divisionLine + 1, startY, endX, endY);

                        int minY = box2[1] - box1[1] > 0 ? box1[1] : box2[1];
                        int maxY = box2[3] - box1[3] > 0 ? box2[3] : box1[3];

                        int alleyPoint1 = Random.Range(box1[1], box1[3]);
                        int box1Int = divisionLine - 1;
                        List<int> availableLines1 = new List<int> { };

                        for (int i = 0; i < box1[3] - box1[1]; i++)
                        {
                            availableLines1.Add(box1[1] + i);
                        }

                        while (field[alleyPoint1, box1Int] == 0)
                        {
                            if (field[alleyPoint1 - 1, box1Int] != 0 || field[alleyPoint1 + 1, box1Int] != 0)
                            {
                                availableLines1.Remove(alleyPoint1);
                                alleyPoint1 = availableLines1[Random.Range(0, availableLines1.Count)];
                                box1Int = divisionLine - 1;
                                continue;
                            }
                            box1Int--;
                        }

                        box1Int = divisionLine - 1;

                        while (field[alleyPoint1, box1Int] == 0)
                        {
                            field[alleyPoint1, box1Int] = 1;
                            box1Int--;
                        }

                        int alleyPoint2 = Random.Range(box2[1], box2[3]);
                        int box2Int = divisionLine;
                        List<int> availableLines2 = new List<int> { };

                        for (int i = 0; i < box2[3] - box2[1]; i++)
                        {
                            availableLines2.Add(box2[1] + i);
                        }

                        while (field[alleyPoint2, box2Int] == 0)
                        {
                            if (field[alleyPoint2 - 1, box2Int] != 0 || field[alleyPoint2 + 1, box2Int] != 0)
                            {
                                availableLines2.Remove(alleyPoint2);
                                alleyPoint2 = availableLines2[Random.Range(0, availableLines2.Count)];
                                box2Int = divisionLine;
                                continue;
                            }
                            box2Int++;
                        }

                        box2Int = divisionLine;

                        while (field[alleyPoint2, box2Int] == 0)
                        {
                            field[alleyPoint2, box2Int] = 1;
                            box2Int++;
                        }

                        if (alleyPoint2 - alleyPoint1 > 0)
                        {
                            for (int i = alleyPoint1; i <= alleyPoint2; i++)
                            {
                                field[i, divisionLine] = 1;
                            }
                        }
                        else if (alleyPoint2 - alleyPoint1 < 0)
                        {
                            for (int i = alleyPoint2; i <= alleyPoint1; i++)
                            {
                                field[i, divisionLine] = 1;
                            }
                        }


                        return new int[] { box1[0], minY, box2[2], maxY };
                    }
                }

            }

        }
        else
        {
            int horOrVer = Random.Range(0, 2);

            if (horOrVer == 0)
            {
                int divisionLine = Random.Range((endY - startY) / 2 + startY - 2, (endY - startY) / 2 + startY + 3);
                if (divisionLine - startY < 8 || endY - divisionLine < 8)
                {
                    return CreateBox();
                }
                else
                {
                    int[] box1 = DivideArea(startX, startY, endX, divisionLine - 1);
                    int[] box2 = DivideArea(startX, divisionLine + 1, endX, endY);

                    int minX = box2[0] - box1[0] > 0 ? box1[0] : box2[0];
                    int maxX = box2[2] - box1[2] > 0 ? box2[2] : box1[2];

                    int alleyPoint1 = Random.Range(box1[0], box1[2]);
                    int box1Int = divisionLine - 1;
                    List<int> availableLines1 = new List<int> { };

                    for (int i = 0; i < box1[2] - box1[0]; i++)
                    {
                        availableLines1.Add(box1[0] + i);
                    }

                    while (field[box1Int, alleyPoint1] == 0)
                    {
                        if (field[box1Int, alleyPoint1 - 1] != 0 || field[box1Int, alleyPoint1 + 1] != 0)
                        {
                            availableLines1.Remove(alleyPoint1);
                            alleyPoint1 = availableLines1[Random.Range(0, availableLines1.Count)];
                            box1Int = divisionLine - 1;
                            continue;
                        }
                        box1Int--;
                    }

                    box1Int = divisionLine - 1;

                    while (field[box1Int, alleyPoint1] == 0)
                    {
                        field[box1Int, alleyPoint1] = 1;
                        box1Int--;
                    }

                    int alleyPoint2 = Random.Range(box2[0], box2[2]);
                    int box2Int = divisionLine;
                    List<int> availableLines2 = new List<int> { };

                    for (int i = 0; i < box2[2] - box2[0]; i++)
                    {
                        availableLines2.Add(box2[0] + i);
                    }

                    while (field[box2Int, alleyPoint2] == 0)
                    {
                        if (field[box2Int, alleyPoint2 - 1] != 0 || field[box2Int, alleyPoint2 + 1] != 0)
                        {
                            availableLines2.Remove(alleyPoint2);
                            alleyPoint2 = availableLines2[Random.Range(0, availableLines2.Count)];
                            box2Int = divisionLine;
                            continue;
                        }
                        box2Int++;
                    }

                    box2Int = divisionLine;

                    while (field[box2Int, alleyPoint2] == 0)
                    {
                        field[box2Int, alleyPoint2] = 1;
                        box2Int++;
                    }

                    if (alleyPoint2 - alleyPoint1 > 0)
                    {
                        for (int i = alleyPoint1; i <= alleyPoint2; i++)
                        {
                            field[divisionLine, i] = 1;
                        }
                    }
                    else if (alleyPoint2 - alleyPoint1 < 0)
                    {
                        for (int i = alleyPoint2; i <= alleyPoint1; i++)
                        {
                            field[divisionLine, i] = 1;
                        }
                    }

                    return new int[] { minX, box1[1], maxX, box2[3] };
                }
            }
            else
            {
                int divisionLine = Random.Range((endX - startX) / 2 + startX - 2, (endX - startX) / 2 + startX + 3);
                if (divisionLine - startX < 8 || endX - divisionLine < 8)
                {
                    return CreateBox();
                }
                else
                {
                    int[] box1 = DivideArea(startX, startY, divisionLine - 1, endY);
                    int[] box2 = DivideArea(divisionLine + 1, startY, endX, endY);

                    int minY = box2[1] - box1[1] > 0 ? box1[1] : box2[1];
                    int maxY = box2[3] - box1[3] > 0 ? box2[3] : box1[3];

                    int alleyPoint1 = Random.Range(box1[1], box1[3]);
                    int box1Int = divisionLine - 1;
                    List<int> availableLines1 = new List<int> { };

                    for (int i = 0; i < box1[3] - box1[1]; i++)
                    {
                        availableLines1.Add(box1[1] + i);
                    }

                    while (field[alleyPoint1, box1Int] == 0)
                    {
                        if (field[alleyPoint1 - 1, box1Int] != 0 || field[alleyPoint1 + 1, box1Int] != 0)
                        {
                            availableLines1.Remove(alleyPoint1);
                            alleyPoint1 = availableLines1[Random.Range(0, availableLines1.Count)];
                            box1Int = divisionLine - 1;
                            continue;
                        }
                        box1Int--;
                    }

                    box1Int = divisionLine - 1;

                    while (field[alleyPoint1, box1Int] == 0)
                    {
                        field[alleyPoint1, box1Int] = 1;
                        box1Int--;
                    }

                    int alleyPoint2 = Random.Range(box2[1], box2[3]);
                    int box2Int = divisionLine;
                    List<int> availableLines2 = new List<int> { };

                    for (int i = 0; i < box2[3] - box2[1]; i++)
                    {
                        availableLines2.Add(box2[1] + i);
                    }

                    while (field[alleyPoint2, box2Int] == 0)
                    {
                        if (field[alleyPoint2 - 1, box2Int] != 0 || field[alleyPoint2 + 1, box2Int] != 0)
                        {
                            availableLines2.Remove(alleyPoint2);
                            alleyPoint2 = availableLines2[Random.Range(0, availableLines2.Count)];
                            box2Int = divisionLine;
                            continue;
                        }
                        box2Int++;
                    }

                    box2Int = divisionLine;

                    while (field[alleyPoint2, box2Int] == 0)
                    {
                        field[alleyPoint2, box2Int] = 1;
                        box2Int++;
                    }

                    if (alleyPoint2 - alleyPoint1 > 0)
                    {
                        for (int i = alleyPoint1; i <= alleyPoint2; i++)
                        {
                            field[i, divisionLine] = 1;
                        }
                    }
                    else if (alleyPoint2 - alleyPoint1 < 0)
                    {
                        for (int i = alleyPoint2; i <= alleyPoint1; i++)
                        {
                            field[i, divisionLine] = 1;
                        }
                    }

                    return new int[] { box1[0], minY, box2[2], maxY };
                }
            }
        }
    }
}
