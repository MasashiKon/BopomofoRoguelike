using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        waiting,
        takingAction,
        finishTurn
    }

    enum Direction
    {
        above,
        right,
        below,
        left,
        aboveRight,
        aboveLeft,
        belowRight,
        belowLeft,
    }
    public List<int> pos;
    public EnemyState state = EnemyState.waiting;
    public int enemyDefence = 1;

    private PlayerController player;
    private TextMeshProUGUI textMessage;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private TurnManager turnManager;
    private int hp = 10;
    private Direction currentDirection;
    private int howManyTurnsInARoom = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        field = dungeonGenerator.field;
        currentDirection = (Direction)Enum.GetValues(typeof(Direction)).GetValue(UnityEngine.Random.Range(0, 8));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator TakeAction()
    {
        state = EnemyState.takingAction;

        // Attack
        bool isPlayerAdjacent = false;
        foreach (GameObject anyOb in turnManager.objectInfo[pos[0] + 1, pos[1]])
        {
            if (anyOb.CompareTag("Player"))
            {
                isPlayerAdjacent = true;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        if (!isPlayerAdjacent)
        {
            foreach (GameObject anyOb in turnManager.objectInfo[pos[0] - 1, pos[1]])
            {
                if (anyOb.CompareTag("Player"))
                {
                    isPlayerAdjacent = true;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (!isPlayerAdjacent)
            {
                foreach (GameObject anyOb in turnManager.objectInfo[pos[0], pos[1] + 1])
                {
                    if (anyOb.CompareTag("Player"))
                    {
                        isPlayerAdjacent = true;
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                    }
                }
                if (!isPlayerAdjacent)
                {
                    foreach (GameObject anyOb in turnManager.objectInfo[pos[0], pos[1] - 1])
                    {
                        if (anyOb.CompareTag("Player"))
                        {
                            isPlayerAdjacent = true;
                            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                    if (!isPlayerAdjacent)
                    {
                        foreach (GameObject anyOb in turnManager.objectInfo[pos[0] - 1, pos[1] + 1])
                        {
                            if (anyOb.CompareTag("Player"))
                            {
                                isPlayerAdjacent = true;
                                gameObject.transform.rotation = Quaternion.Euler(0, 0, 315);
                            }
                        }
                        if (!isPlayerAdjacent)
                        {
                            foreach (GameObject anyOb in turnManager.objectInfo[pos[0] + 1, pos[1] + 1])
                            {
                                if (anyOb.CompareTag("Player"))
                                {
                                    isPlayerAdjacent = true;
                                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 225);
                                }
                            }
                            if (!isPlayerAdjacent)
                            {
                                foreach (GameObject anyOb in turnManager.objectInfo[pos[0] + 1, pos[1] - 1])
                                {
                                    if (anyOb.CompareTag("Player"))
                                    {
                                        isPlayerAdjacent = true;
                                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 135);
                                    }
                                }
                                if (!isPlayerAdjacent)
                                {
                                    foreach (GameObject anyOb in turnManager.objectInfo[pos[0] - 1, pos[1] - 1])
                                    {
                                        if (anyOb.CompareTag("Player"))
                                        {
                                            isPlayerAdjacent = true;
                                            gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        if (isPlayerAdjacent)
        {
            textMessage.SetText("モンスターの攻撃！");
            gameObject.transform.GetChild(0).GetComponent<Animator>().Play("PlayerAttack");
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<PlayerController>().DecreaseHP(UnityEngine.Random.Range(3, 6));

            yield return new WaitForSeconds(0.5f);

            textMessage.SetText("");

        }
        else
        {
            // Move
            if (field[pos[0], pos[1]] == 2)
            {
                howManyTurnsInARoom++;
            }
            else
            {
                howManyTurnsInARoom = 0;
            }
            if (howManyTurnsInARoom > 50)
            {
                int rangeOfSearching = 1;
                bool exitFound = false;
                List<int> exit = new List<int>();
                List<int> avobeOmitList = new List<int> { };
                List<int> belowOmitList = new List<int> { };
                List<int> rightOmitList = new List<int> { };
                List<int> leftOmitList = new List<int> { };
                bool IsAExitAjacentCells(int i, int j)
                {
                    if (field[i, j + 1] == 1)
                    {
                        return true;
                    }
                    else if (field[i - 1, j + 1] == 1)
                    {
                        return true;
                    }
                    else if (field[i - 1, j] == 1)
                    {
                        return true;
                    }
                    else if (field[i - 1, j - 1] == 1)
                    {
                        return true;
                    }
                    else if (field[i, j - 1] == 1)
                    {
                        return true;
                    }
                    else if (field[i + 1, j - 1] == 1)
                    {
                        return true;
                    }
                    else if (field[i + 1, j] == 1)
                    {
                        return true;
                    }
                    else if (field[i + 1, j + 1] == 1)
                    {
                        return true;
                    }
                    return false;
                }
                List<int> ReturnExit(int i, int j)
                {
                    if (field[i, j + 1] == 1)
                    {
                        return new List<int> { i, j + 1 };
                    }
                    else if (field[i - 1, j + 1] == 1)
                    {
                        return new List<int> { i - 1, j + 1 };
                    }
                    else if (field[i - 1, j] == 1)
                    {
                        return new List<int> { i - 1, j };
                    }
                    else if (field[i - 1, j - 1] == 1)
                    {
                        return new List<int> { i - 1, j - 1 };
                    }
                    else if (field[i, j - 1] == 1)
                    {
                        return new List<int> { i, j - 1 };
                    }
                    else if (field[i + 1, j - 1] == 1)
                    {
                        return new List<int> { i + 1, j - 1 };
                    }
                    else if (field[i + 1, j] == 1)
                    {
                        return new List<int> { i + 1, j };
                    }
                    else if (field[i + 1, j + 1] == 1)
                    {
                        return new List<int> { i + 1, j + 1 };
                    }
                    return null;
                }
                bool ExitBelongsToOtherRooms(int i, int j)
                {
                    if (i - pos[0] > 0 && j - pos[1] > 0)
                    {
                        List<int> testPath1 = new List<int> { };
                        List<int> testPath2 = new List<int> { };
                        for (int k = i; k >= pos[0]; k--)
                        {
                            testPath1.Add(field[k, j]);
                        }
                        for (int k = j; k >= pos[1]; k--)
                        {
                            testPath1.Add(field[pos[0], k]);
                        }
                        if (!testPath1.Exists(x => x != 2)) return true;
                        for (int k = j; k >= pos[1]; k--)
                        {
                            testPath2.Add(field[i, k]);
                        }
                        for (int k = i; k >= pos[0]; k--)
                        {
                            testPath2.Add(field[k, pos[1]]);
                        }
                        if (!testPath2.Exists(x => x != 2)) return true;
                    }
                    else if (i - pos[0] < 0 && j - pos[1] > 0)
                    {
                        List<int> testPath1 = new List<int> { };
                        List<int> testPath2 = new List<int> { };
                        for (int k = i; k <= pos[0]; k++)
                        {
                            testPath1.Add(field[k, j]);
                        }
                        for (int k = j; k >= pos[1]; k--)
                        {
                            testPath1.Add(field[pos[0], k]);
                        }
                        if (!testPath1.Exists(x => x != 2)) return true;
                        for (int k = j; k >= pos[1]; k--)
                        {
                            testPath2.Add(field[i, k]);
                        }
                        for (int k = i; k <= pos[0]; k++)
                        {
                            testPath2.Add(field[k, pos[1]]);
                        }
                        if (!testPath2.Exists(x => x != 2)) return true;
                    }
                    else if (i - pos[0] < 0 && j - pos[1] < 0)
                    {
                        List<int> testPath1 = new List<int> { };
                        List<int> testPath2 = new List<int> { };
                        for (int k = i; k <= pos[0]; k++)
                        {
                            testPath1.Add(field[k, j]);
                        }
                        for (int k = j; k <= pos[1]; k++)
                        {
                            testPath1.Add(field[pos[0], k]);
                        }
                        if (!testPath1.Exists(x => x != 2)) return true;
                        for (int k = j; k <= pos[1]; k++)
                        {
                            testPath2.Add(field[i, k]);
                        }
                        for (int k = i; k <= pos[0]; k++)
                        {
                            testPath2.Add(field[k, pos[1]]);
                        }
                        if (!testPath2.Exists(x => x != 2)) return true;
                    }
                    else if (i - pos[0] > 0 && j - pos[1] < 0)
                    {
                        List<int> testPath1 = new List<int> { };
                        List<int> testPath2 = new List<int> { };
                        for (int k = i; k >= pos[0]; k--)
                        {
                            testPath1.Add(field[k, j]);
                        }
                        for (int k = j; k <= pos[1]; k++)
                        {
                            testPath1.Add(field[pos[0], k]);
                        }
                        if (!testPath1.Exists(x => x != 2)) return true;
                        for (int k = j; k <= pos[1]; k++)
                        {
                            testPath2.Add(field[i, k]);
                        }
                        for (int k = i; k >= pos[0]; k--)
                        {
                            testPath2.Add(field[k, pos[1]]);
                        }
                        if (!testPath2.Exists(x => x != 2)) return true;
                    }
                    return false;
                }
                while (!exitFound)
                {
                    for (int i = -rangeOfSearching; i <= rangeOfSearching; i++)
                    {
                        if (pos[1] - rangeOfSearching > 0 && pos[1] - rangeOfSearching < DungeonGenerator.dungeonSize && pos[0] + i > 0 && pos[0] + i < DungeonGenerator.dungeonSize)
                        {
                            if (i == -rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + i, pos[1] - j] != 2)
                                    {
                                        leftOmitList.Add(i);
                                    }
                                }
                            }
                            else if (i == rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + i, pos[1] - j] != 2)
                                    {
                                        leftOmitList.Add(i);
                                    }
                                }
                            }
                            if (field[pos[0] + i, pos[1] - rangeOfSearching] != 2)
                            {
                                leftOmitList.Add(i);
                            }
                            else if (!leftOmitList.Contains(i) && IsAExitAjacentCells(pos[0] + i, pos[1] - rangeOfSearching))
                            {
                                exit = ReturnExit(pos[0] + i, pos[1] - rangeOfSearching);
                                if (ExitBelongsToOtherRooms(exit[0], exit[1])) continue;
                                exitFound = true;
                                break;
                            }
                        }
                    }
                    if (exitFound) break;
                    for (int i = -rangeOfSearching; i <= rangeOfSearching; i++)
                    {
                        if (pos[1] + rangeOfSearching > 0 && pos[1] + rangeOfSearching < DungeonGenerator.dungeonSize && pos[0] + i > 0 && pos[0] + i < DungeonGenerator.dungeonSize)
                        {
                            if (i == -rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + i, pos[1] + j] != 2)
                                    {
                                        rightOmitList.Add(i);
                                    }
                                }
                            }
                            else if (i == rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + i, pos[1] + j] != 2)
                                    {
                                        rightOmitList.Add(i);
                                    }
                                }
                            }
                            if (field[pos[0] + i, pos[1] + rangeOfSearching] != 2)
                            {
                                rightOmitList.Add(i);
                            }
                            else if (!rightOmitList.Contains(i) && IsAExitAjacentCells(pos[0] + i, pos[1] + rangeOfSearching))
                            {
                                exit = ReturnExit(pos[0] + i, pos[1] + rangeOfSearching);
                                if (ExitBelongsToOtherRooms(exit[0], exit[1])) continue;
                                exitFound = true;
                                break;
                            }
                        }
                    }
                    if (exitFound) break;
                    for (int i = -rangeOfSearching; i <= rangeOfSearching; i++)
                    {
                        if (pos[0] - rangeOfSearching > 0 && pos[0] - rangeOfSearching < DungeonGenerator.dungeonSize && pos[1] + i > 0 && pos[1] + i < DungeonGenerator.dungeonSize)
                        {
                            if (i == -rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] - j, pos[1] + i] != 2)
                                    {
                                        avobeOmitList.Add(i);
                                    }
                                }
                            }
                            else if (i == rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] - j, pos[1] + i] != 2)
                                    {
                                        avobeOmitList.Add(i);
                                    }
                                }
                            }
                            if (field[pos[0] - rangeOfSearching, pos[1] + i] != 2)
                            {
                                avobeOmitList.Add(i);
                            }
                            else if (!avobeOmitList.Contains(i) && IsAExitAjacentCells(pos[0] - rangeOfSearching, pos[1] + i))
                            {
                                exit = ReturnExit(pos[0] - rangeOfSearching, pos[1] + i);
                                if (ExitBelongsToOtherRooms(exit[0], exit[1])) continue;
                                exitFound = true;
                                break;
                            }
                        }
                    }
                    if (exitFound) break;
                    for (int i = -rangeOfSearching; i <= rangeOfSearching; i++)
                    {
                        if (pos[0] + rangeOfSearching > 0 && pos[0] + rangeOfSearching < DungeonGenerator.dungeonSize && pos[1] + i > 0 && pos[1] + i < DungeonGenerator.dungeonSize)
                        {
                            if (i == -rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + j, pos[1] + i] != 2)
                                    {
                                        belowOmitList.Add(i);
                                    }
                                }
                            }
                            else if (i == rangeOfSearching)
                            {
                                for (int j = 1; j < rangeOfSearching; j++)
                                {
                                    if (field[pos[0] + j, pos[1] + i] != 2)
                                    {
                                        belowOmitList.Add(i);
                                    }
                                }
                            }
                            if (field[pos[0] + rangeOfSearching, pos[1] + i] != 2)
                            {
                                belowOmitList.Add(i);
                            }
                            else if (!belowOmitList.Contains(i) && IsAExitAjacentCells(pos[0] + rangeOfSearching, pos[1] + i))
                            {
                                exit = ReturnExit(pos[0] + rangeOfSearching, pos[1] + i);
                                if (ExitBelongsToOtherRooms(exit[0], exit[1])) continue;
                                exitFound = true;
                                break;
                            }
                        }
                    }
                    if (exitFound) break;
                    rangeOfSearching++;
                    if (rangeOfSearching > DungeonGenerator.dungeonSize)
                    {
                        exitFound = true;
                    }
                }
                if (exit.Count > 0)
                {
                    int dx = exit[1] - pos[1];
                    int dy = -(exit[0] - pos[0]);

                    double angleRadians = Math.Atan2(dy, dx);
                    double angleDegrees = angleRadians * (180 / Math.PI);
                    if (angleDegrees < 0)
                    {
                        angleDegrees += 360;
                    }
                    if (angleDegrees >= 337.5 || angleDegrees < 22.5)
                    {
                        currentDirection = Direction.right;
                    }
                    else if (angleDegrees >= 22.5 && angleDegrees < 67.5)
                    {
                        currentDirection = Direction.aboveRight;
                    }
                    else if (angleDegrees >= 67.5 && angleDegrees < 112.5)
                    {
                        currentDirection = Direction.above;
                    }
                    else if (angleDegrees >= 112.5 && angleDegrees < 157.5)
                    {
                        currentDirection = Direction.aboveLeft;
                    }
                    else if (angleDegrees >= 157.5 && angleDegrees < 202.5)
                    {
                        currentDirection = Direction.left;
                    }
                    else if (angleDegrees >= 202.5 && angleDegrees < 247.5)
                    {
                        currentDirection = Direction.belowLeft;
                    }
                    else if (angleDegrees >= 247.5 && angleDegrees < 292.5)
                    {
                        currentDirection = Direction.below;
                    }
                    else if (angleDegrees >= 292.5 && angleDegrees < 337.5)
                    {
                        currentDirection = Direction.belowRight;
                    }
                }
                if (exit.Count > 0 && rangeOfSearching == 1)
                {
                    if (currentDirection == Direction.aboveRight)
                    {
                        if (field[pos[0] - 1, pos[1]] == 0)
                        {
                            currentDirection = Direction.right;
                        }
                        else if (field[pos[0], pos[1] + 1] == 0)
                        {
                            currentDirection = Direction.above;
                        }
                    }
                    else if (currentDirection == Direction.aboveLeft)
                    {
                        if (field[pos[0] - 1, pos[1]] == 0)
                        {
                            currentDirection = Direction.left;
                        }
                        else if (field[pos[0], pos[1] - 1] == 0)
                        {
                            currentDirection = Direction.above;
                        }
                    }
                    else if (currentDirection == Direction.belowLeft)
                    {
                        if (field[pos[0] + 1, pos[1]] == 0)
                        {
                            currentDirection = Direction.left;
                        }
                        else if (field[pos[0], pos[1] - 1] == 0)
                        {
                            currentDirection = Direction.below;
                        }
                    }
                    else if (currentDirection == Direction.belowRight)
                    {
                        if (field[pos[0] + 1, pos[1]] == 0)
                        {
                            currentDirection = Direction.right;
                        }
                        else if (field[pos[0], pos[1] + 1] == 0)
                        {
                            currentDirection = Direction.below;
                        }
                    }
                }
            }
            if (currentDirection == Direction.above && field[pos[0] - 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] - 1, pos[1] };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.aboveRight && field[pos[0] - 1, pos[1] + 1] != 0 && field[pos[0] - 1, pos[1]] != 0 && field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] - 1, pos[1] + 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.right && field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0], pos[1] + 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.belowRight && field[pos[0] + 1, pos[1] + 1] != 0 && field[pos[0] + 1, pos[1]] != 0 && field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] + 1, pos[1] + 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.below && field[pos[0] + 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] + 1, pos[1] };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.belowLeft && field[pos[0] + 1, pos[1] - 1] != 0 && field[pos[0] + 1, pos[1]] != 0 && field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] + 1, pos[1] - 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.left && field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0], pos[1] - 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else if (currentDirection == Direction.aboveLeft && field[pos[0] - 1, pos[1] - 1] != 0 && field[pos[0] - 1, pos[1]] != 0 && field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                {
                    if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                    {
                        turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    }
                }

                pos = new List<int> { pos[0] - 1, pos[1] - 1 };
                turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
            }
            else
            {
                List<int[]> availableCell = new List<int[]>();
                if (field[pos[0] + 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.above)
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] });
                    }
                }
                if (field[pos[0] - 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.below)
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] });
                    }
                }
                if (field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.left)
                    {
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] + 1 });
                    }
                }
                if (field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.right)
                    {
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0], pos[1] - 1 });
                    }
                }
                if (field[pos[0] - 1, pos[1] + 1] != 0 && field[pos[0] - 1, pos[1]] != 0 && field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.belowLeft)
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] + 1 });
                    }
                }
                if (field[pos[0] + 1, pos[1] + 1] != 0 && field[pos[0] + 1, pos[1]] != 0 && field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.aboveLeft)
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] + 1 });
                    }
                }
                if (field[pos[0] + 1, pos[1] - 1] != 0 && field[pos[0] + 1, pos[1]] != 0 && field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.aboveRight)
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] + 1, pos[1] - 1 });
                    }
                }
                if (field[pos[0] - 1, pos[1] - 1] != 0 && field[pos[0] - 1, pos[1]] != 0 && field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
                {
                    if (currentDirection == Direction.belowRight)
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                    }
                    else
                    {
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                        availableCell.Add(new int[] { pos[0] - 1, pos[1] - 1 });
                    }
                }

                if (availableCell.Count != 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, availableCell.Count + 1);
                    if (randomIndex != availableCell.Count)
                    {
                        for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                        {
                            if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                            {
                                turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                            }

                        }
                        if (pos[0] - availableCell[randomIndex][0] == 1 && pos[1] - availableCell[randomIndex][1] == 0)
                        {
                            currentDirection = Direction.above;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == 1 && pos[1] - availableCell[randomIndex][1] == -1)
                        {
                            currentDirection = Direction.aboveRight;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == 0 && pos[1] - availableCell[randomIndex][1] == -1)
                        {
                            currentDirection = Direction.right;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == -1 && pos[1] - availableCell[randomIndex][1] == -1)
                        {
                            currentDirection = Direction.belowRight;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == -1 && pos[1] - availableCell[randomIndex][1] == 0)
                        {
                            currentDirection = Direction.below;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == -1 && pos[1] - availableCell[randomIndex][1] == 1)
                        {
                            currentDirection = Direction.belowLeft;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == 0 && pos[1] - availableCell[randomIndex][1] == 1)
                        {
                            currentDirection = Direction.left;
                        }
                        else if (pos[0] - availableCell[randomIndex][0] == 0 && pos[1] - availableCell[randomIndex][1] == 1)
                        {
                            currentDirection = Direction.aboveLeft;
                        }

                        pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
                        turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                        gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    }
                }
            }
        }

        state = EnemyState.finishTurn;
    }

    public void IncreaceHP(int amountOfHeal)
    {
        hp += amountOfHeal;
        textMessage.SetText($"モンスターは{amountOfHeal}回復した！");
    }

    public void DecreaceHP(int damage)
    {
        hp -= damage;
        textMessage.SetText($"モンスターに{damage}のダメージ！");
        if (hp <= 0)
        {
            for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
            {
                if (turnManager.objectInfo[pos[0], pos[1]][i].CompareTag("Enemy"))
                {
                    turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
