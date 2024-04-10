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
            else if (currentDirection == Direction.aboveRight && field[pos[0] - 1, pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
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
            else if (currentDirection == Direction.belowRight && field[pos[0] + 1, pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
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
            else if (currentDirection == Direction.belowLeft && field[pos[0] + 1, pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
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
            else if (currentDirection == Direction.aboveLeft && field[pos[0] - 1, pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
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
