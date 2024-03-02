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
        left
    }
    public List<int> pos;
    public EnemyState state = EnemyState.waiting;

    private PlayerController player;
    private TextMeshProUGUI textMessage;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private TurnManager turnManager;
    private int hp = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        field = dungeonGenerator.field;
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
                }

            }

        }

        if (isPlayerAdjacent)
        {
            textMessage.SetText("モンスターの攻撃！");
            gameObject.transform.GetChild(0).GetComponent<Animator>().Play("PlayerAttack");
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<PlayerController>().DecreaseHP(Random.Range(1, 4));

            yield return new WaitForSeconds(0.5f);

            textMessage.SetText("");

        }
        else
        {
            // Move
            List<int[]> availableCell = new List<int[]>();
            if (field[pos[0] + 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] + 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                availableCell.Add(new int[] { pos[0] + 1, pos[1] });
            }
            if (field[pos[0] - 1, pos[1]] != 0 && !turnManager.objectInfo[pos[0] - 1, pos[1]].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                availableCell.Add(new int[] { pos[0] - 1, pos[1] });
            }
            if (field[pos[0], pos[1] + 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] + 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                availableCell.Add(new int[] { pos[0], pos[1] + 1 });
            }
            if (field[pos[0], pos[1] - 1] != 0 && !turnManager.objectInfo[pos[0], pos[1] - 1].Exists(ob => ob.CompareTag("Player") || ob.CompareTag("Enemy")))
            {
                availableCell.Add(new int[] { pos[0], pos[1] - 1 });
            }

            if (availableCell.Count != 0)
            {
                int randomIndex = Random.Range(0, availableCell.Count + 1);
                if (randomIndex != availableCell.Count)
                {
                    for (int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
                    {
                        if (turnManager.objectInfo[pos[0], pos[1]][i] == gameObject)
                        {
                            turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                        }

                    }
                    pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
                    turnManager.objectInfo[pos[0], pos[1]].Add(gameObject);
                    gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                }
            }
        }

        state = EnemyState.finishTurn;
    }

    public void DecreaceHP(int damage)
    {
        hp -= damage;
        textMessage.SetText($"{damage}のダメージ！");
        if (hp <= 0)
        {
            for(int i = 0; i < turnManager.objectInfo[pos[0], pos[1]].Count; i++)
            {
                if(turnManager.objectInfo[pos[0], pos[1]][i].CompareTag("Enemy"))
                {
                    turnManager.objectInfo[pos[0], pos[1]].RemoveAt(i);
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
