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
                    Instantiate(start, new Vector3(j - dungeonSize / 2, i * -1 + dungeonSize / 2, 0), Quaternion.identity);
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

    private void DivideArea(int startX, int startY, int endX, int endY)
    {
        if ((endX - startX < 8 && endY - startY < 16) || (endX - startX < 16 && endY - startY < 8))
        {
            int box1pointX1;
            int box1pointX2;
            do
            {
                box1pointX1 = Random.Range(startX + 1, endX - 1);
                box1pointX2 = Random.Range(startX + 1, endX - 1);
            } while (box1pointX1 == box1pointX2);

            int box1pointY1;
            int box1pointY2;
            do
            {
                box1pointY1 = Random.Range(startY + 1, endY - 1);
                box1pointY2 = Random.Range(startY + 1, endY - 1);
            } while (box1pointY1 == box1pointY2);

            if (box1pointY2 - box1pointY1 > 0)
            {
                if (box1pointX2 - box1pointX1 > 0)
                {
                    for (int i = box1pointY1; i <= box1pointY2; i++)
                    {
                        for (int j = box1pointX1; j <= box1pointX2; j++)
                        {
                            field[i, j] = 1;
                        }
                    }
                }
                else
                {
                    for (int i = box1pointY1; i <= box1pointY2; i++)
                    {
                        for (int j = box1pointX2; j <= box1pointX1; j++)
                        {
                            field[i, j] = 1;
                        }
                    }
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
                            field[i, j] = 1;
                        }
                    }
                }
                else
                {
                    for (int i = box1pointY2; i <= box1pointY1; i++)
                    {
                        for (int j = box1pointX2; j <= box1pointX1; j++)
                        {
                            field[i, j] = 1;
                        }
                    }
                }
            }

            return;
        }

        if (endX - startX < 16 && endY - startY >= 16)
        {
            int divisionLine = Random.Range((endY - startY) / 2 + startY - 2, (endY - startY) / 2 + startY + 3);

            if (divisionLine - startY < 8 || endY - divisionLine < 8)
            {
                int box1pointX1;
                int box1pointX2;
                do
                {
                    box1pointX1 = Random.Range(startX + 1, endX - 1);
                    box1pointX2 = Random.Range(startX + 1, endX - 1);
                } while (box1pointX1 == box1pointX2);

                int box1pointY1;
                int box1pointY2;
                do
                {
                    box1pointY1 = Random.Range(startY + 1, endY - 1);
                    box1pointY2 = Random.Range(startY + 1, endY - 1);
                } while (box1pointY1 == box1pointY2);

                if (box1pointY2 - box1pointY1 > 0)
                {
                    if (box1pointX2 - box1pointX1 > 0)
                    {
                        for (int i = box1pointY1; i <= box1pointY2; i++)
                        {
                            for (int j = box1pointX1; j <= box1pointX2; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = box1pointY1; i <= box1pointY2; i++)
                        {
                            for (int j = box1pointX2; j <= box1pointX1; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
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
                                field[i, j] = 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = box1pointY2; i <= box1pointY1; i++)
                        {
                            for (int j = box1pointX2; j <= box1pointX1; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
                    }
                }
            }
            else
            {
                DivideArea(startX, startY, endX, divisionLine - 1);
                DivideArea(startX, divisionLine, endX, endY);
            }
        }
        else if (endX - startX >= 16 && endY - startY < 16)
        {
            int divisionLine = Random.Range((endX - startX) / 2 + startX - 2, (endX - startX) / 2 + startX + 3);

            if (divisionLine - startX < 8 || endX - divisionLine < 8)
            {
                int box1pointX1;
                int box1pointX2;
                do
                {
                    box1pointX1 = Random.Range(startX + 1, endX - 1);
                    box1pointX2 = Random.Range(startX + 1, endX - 1);
                } while (box1pointX1 == box1pointX2);

                int box1pointY1;
                int box1pointY2;
                do
                {
                    box1pointY1 = Random.Range(startY + 1, endY - 1);
                    box1pointY2 = Random.Range(startY + 1, endY - 1);
                } while (box1pointY1 == box1pointY2);

                if (box1pointY2 - box1pointY1 > 0)
                {
                    if (box1pointX2 - box1pointX1 > 0)
                    {
                        for (int i = box1pointY1; i <= box1pointY2; i++)
                        {
                            for (int j = box1pointX1; j <= box1pointX2; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = box1pointY1; i <= box1pointY2; i++)
                        {
                            for (int j = box1pointX2; j <= box1pointX1; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
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
                                field[i, j] = 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = box1pointY2; i <= box1pointY1; i++)
                        {
                            for (int j = box1pointX2; j <= box1pointX1; j++)
                            {
                                field[i, j] = 1;
                            }
                        }
                    }
                }
            }
            else
            {
                DivideArea(startX, startY, divisionLine - 1, endY);
                DivideArea(divisionLine, startY, endX, endY);
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
                    int box1pointX1;
                    int box1pointX2;
                    do
                    {
                        box1pointX1 = Random.Range(startX + 1, endX - 1);
                        box1pointX2 = Random.Range(startX + 1, endX - 1);
                    } while (box1pointX1 == box1pointX2);

                    int box1pointY1;
                    int box1pointY2;
                    do
                    {
                        box1pointY1 = Random.Range(startY + 1, endY - 1);
                        box1pointY2 = Random.Range(startY + 1, endY - 1);
                    } while (box1pointY1 == box1pointY2);

                    if (box1pointY2 - box1pointY1 > 0)
                    {
                        if (box1pointX2 - box1pointX1 > 0)
                        {
                            for (int i = box1pointY1; i <= box1pointY2; i++)
                            {
                                for (int j = box1pointX1; j <= box1pointX2; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = box1pointY1; i <= box1pointY2; i++)
                            {
                                for (int j = box1pointX2; j <= box1pointX1; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
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
                                    field[i, j] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = box1pointY2; i <= box1pointY1; i++)
                            {
                                for (int j = box1pointX2; j <= box1pointX1; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    DivideArea(startX, startY, endX, divisionLine - 1);
                    DivideArea(startX, divisionLine, endX, endY);
                }

                //// box1
                //int box1pointX1;
                //int box1pointX2;

                //do
                //{
                //    box1pointX1 = Random.Range(startX + 1, endX - 1);
                //    box1pointX2 = Random.Range(startX + 1, endX - 1);
                //} while (box1pointX1 == box1pointX2);

                //int box1pointY1;
                //int box1pointY2;

                //do
                //{
                //    box1pointY1 = Random.Range(startY + 1, divisionLine - 2);
                //    box1pointY2 = Random.Range(startY + 1, divisionLine - 2);
                //} while (box1pointY1 == box1pointY2);

                //if (box1pointY2 - box1pointY1 > 0)
                //{
                //    if (box1pointX2 - box1pointX1 > 0)
                //    {
                //        for (int i = box1pointY1; i <= box1pointY2; i++)
                //        {
                //            for (int j = box1pointX1; j <= box1pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box1pointY1; i <= box1pointY2; i++)
                //        {
                //            for (int j = box1pointX2; j <= box1pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    if (box1pointX2 - box1pointX1 > 0)
                //    {
                //        for (int i = box1pointY2; i <= box1pointY1; i++)
                //        {
                //            for (int j = box1pointX1; j <= box1pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box1pointY2; i <= box1pointY1; i++)
                //        {
                //            for (int j = box1pointX2; j <= box1pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}

                //// box2
                //int box2pointX1;
                //int box2pointX2;

                //do
                //{
                //    box2pointX1 = Random.Range(startX + 1, endX - 1);
                //    box2pointX2 = Random.Range(startX + 1, endX - 1);
                //} while (box2pointX1 == box2pointX2);

                //int box2pointY1;
                //int box2pointY2;

                //do
                //{
                //    box2pointY1 = Random.Range(divisionLine + 1, endY - 1);
                //    box2pointY2 = Random.Range(divisionLine + 1, endY - 1);
                //} while (box2pointY1 == box2pointY2);

                //if (box2pointY2 - box2pointY1 > 0)
                //{
                //    if (box2pointX2 - box2pointX1 > 0)
                //    {
                //        for (int i = box2pointY1; i <= box2pointY2; i++)
                //        {
                //            for (int j = box2pointX1; j <= box2pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box2pointY1; i <= box2pointY2; i++)
                //        {
                //            for (int j = box2pointX2; j <= box2pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    if (box2pointX2 - box2pointX1 > 0)
                //    {
                //        for (int i = box2pointY2; i <= box2pointY1; i++)
                //        {
                //            for (int j = box2pointX1; j <= box2pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box2pointY2; i <= box2pointY1; i++)
                //        {
                //            for (int j = box2pointX2; j <= box2pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
            }
            else
            {
                int divisionLine = Random.Range((endX - startX) / 2 + startX - 2, (endX - startX) / 2 + startX + 3);
                if (divisionLine - startX < 8 || endX - divisionLine < 8)
                {
                    int box1pointX1;
                    int box1pointX2;
                    do
                    {
                        box1pointX1 = Random.Range(startX + 1, endX - 1);
                        box1pointX2 = Random.Range(startX + 1, endX - 1);
                    } while (box1pointX1 == box1pointX2);

                    int box1pointY1;
                    int box1pointY2;
                    do
                    {
                        box1pointY1 = Random.Range(startY + 1, endY - 1);
                        box1pointY2 = Random.Range(startY + 1, endY - 1);
                    } while (box1pointY1 == box1pointY2);

                    if (box1pointY2 - box1pointY1 > 0)
                    {
                        if (box1pointX2 - box1pointX1 > 0)
                        {
                            for (int i = box1pointY1; i <= box1pointY2; i++)
                            {
                                for (int j = box1pointX1; j <= box1pointX2; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = box1pointY1; i <= box1pointY2; i++)
                            {
                                for (int j = box1pointX2; j <= box1pointX1; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
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
                                    field[i, j] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = box1pointY2; i <= box1pointY1; i++)
                            {
                                for (int j = box1pointX2; j <= box1pointX1; j++)
                                {
                                    field[i, j] = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    DivideArea(startX, startY, divisionLine - 1, endY);
                    DivideArea(divisionLine, startY, endX, endY);
                }

                //// box1
                //int box1pointX1;
                //int box1pointX2;

                //do
                //{
                //    box1pointX1 = Random.Range(startX + 1, divisionLine - 2);
                //    box1pointX2 = Random.Range(startX + 1, divisionLine - 2);
                //} while (box1pointX1 == box1pointX2);

                //int box1pointY1;
                //int box1pointY2;

                //do
                //{
                //    box1pointY1 = Random.Range(startY + 1, endY - 1);
                //    box1pointY2 = Random.Range(startY + 1, endY - 1);
                //} while (box1pointY1 == box1pointY2);

                //if (box1pointY2 - box1pointY1 > 0)
                //{
                //    if (box1pointX2 - box1pointX1 > 0)
                //    {
                //        for (int i = box1pointY1; i <= box1pointY2; i++)
                //        {
                //            for (int j = box1pointX1; j <= box1pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box1pointY1; i <= box1pointY2; i++)
                //        {
                //            for (int j = box1pointX2; j <= box1pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    if (box1pointX2 - box1pointX1 > 0)
                //    {
                //        for (int i = box1pointY2; i <= box1pointY1; i++)
                //        {
                //            for (int j = box1pointX1; j <= box1pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box1pointY2; i <= box1pointY1; i++)
                //        {
                //            for (int j = box1pointX2; j <= box1pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}

                //// box2
                //int box2pointX1;
                //int box2pointX2;

                //do
                //{
                //    box2pointX1 = Random.Range(divisionLine + 1, endX - 1);
                //    box2pointX2 = Random.Range(divisionLine + 1, endX - 1);
                //} while (box2pointX1 == box2pointX2);

                //int box2pointY1;
                //int box2pointY2;

                //do
                //{
                //    box2pointY1 = Random.Range(startY + 1, endY - 1);
                //    box2pointY2 = Random.Range(startY + 1, endY - 1);
                //} while (box2pointY1 == box2pointY2);

                //if (box2pointY2 - box2pointY1 > 0)
                //{
                //    if (box2pointX2 - box2pointX1 > 0)
                //    {
                //        for (int i = box2pointY1; i <= box2pointY2; i++)
                //        {
                //            for (int j = box2pointX1; j <= box2pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box2pointY1; i <= box2pointY2; i++)
                //        {
                //            for (int j = box2pointX2; j <= box2pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    if (box2pointX2 - box2pointX1 > 0)
                //    {
                //        for (int i = box2pointY2; i <= box2pointY1; i++)
                //        {
                //            for (int j = box2pointX1; j <= box2pointX2; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = box2pointY2; i <= box2pointY1; i++)
                //        {
                //            for (int j = box2pointX2; j <= box2pointX1; j++)
                //            {
                //                field[i, j] = 1;
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
