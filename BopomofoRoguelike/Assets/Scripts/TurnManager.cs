using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isReadyNextMove = false;
    public bool isAttackPhase = false;
    public bool itItemProcessPhase = false;
    public bool isPlayerMovePhase = false;
    public bool areEnemiesMove = false;
    public bool isPlayerThrowItem = false;
    public int[] thrownItemPosition = new int[0];
    public GameObject mainCamera;
    public GameObject player;
    public List<GameObject>[,] objectInfo = new List<GameObject>[DungeonGenerator.dungeonSize, DungeonGenerator.dungeonSize];
    public GameObject message;
    public GameObject[] prefabItems;

    // Camera Move
    private bool isCameraMoving = false;
    private Vector3 cameraPrevPos = new Vector3(0, 0, -10);
    private Vector3 cameraCurrentPos = new Vector3(0, 0, -10);
    private int cameraMoveInterpolationFramesCount = 60;
    private int cameraMoveElapsedFrames = 0;
    private int enemyInt = 0;
    private UIManager uiManager;
    private PrefabManager prefabManager;
    private DungeonGenerator dungeonGenerator;
    private GameObject flyingItem;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DungeonGenerator.dungeonSize; i++)
        {
            for (int j = 0; j < DungeonGenerator.dungeonSize; j++)
            {
                objectInfo[i, j] = new List<GameObject> { };
            }
        }
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProcessTurn()
    {
        isReadyNextMove = false;
        PlayerController playerController = player.GetComponent<PlayerController>();
        TextMeshProUGUI textMessage = message.GetComponent<TextMeshProUGUI>();

        // Battle Phase
        if (playerController.isPlayerAttack)
        {
            StartCoroutine(ProcessBattle(playerController, textMessage));
        }
        else if (isPlayerThrowItem)
        {
            itItemProcessPhase = true;
            flyingItem = objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Find(ob => ob.CompareTag("Item"));
            StartCoroutine(FlyItem());
        }

        if (playerController.isPlayerMove && !isPlayerThrowItem)
        {
            isPlayerMovePhase = true;
            if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Exists(ob => ob.CompareTag("Item")))
            {
                for (int i = 0; i < objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Count; i++)
                {
                    if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]][i].CompareTag("Item"))
                    {
                        GameObject targetItem = null;
                        for (int j = 0; j < prefabManager.items.Length; j++)
                        {
                            if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]][i].name == prefabManager.items[j].name + "(Clone)")
                            {
                                targetItem = Instantiate(prefabManager.items[j]);
                            }
                        }

                        Destroy(objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]][i]);
                        objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].RemoveAt(i);
                        uiManager.items.Add(targetItem);
                        StartCoroutine(DisplayItemGet(targetItem.GetComponent<Item>().GetNameTranslation(Language.Ja), textMessage));
                    }
                }
            }
            else
            {
                isPlayerMovePhase = false;
            }
        }

        // Enemy Move
        if (playerController.isPlayerAttack || playerController.isPlayerMove || playerController.isPlayerUseItem)
        {
            StartCoroutine(ProcessEnemies());
        }

        playerController.isPlayerAttack = false;
        playerController.isPlayerMove = false;
        playerController.isPlayerUseItem = false;
        isPlayerThrowItem = false;

        StartCoroutine(DetectTurnEnd());
    }

    IEnumerator ProcessBattle(PlayerController playerController, TextMeshProUGUI textMessage)
    {
        if (player.transform.eulerAngles.z == 0)
        {
            if (objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]].Count != 0)
            {
                foreach (GameObject enemy in objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]])
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        int damage = Random.Range(playerController.playerAttack, playerController.playerAttack + 3) + (playerController.sword?.GetSwordAttack() ?? 0);
                        int calcDamage = damage - enemy.GetComponent<EnemyController>().enemyDefence > 1 ? damage - enemy.GetComponent<EnemyController>().enemyDefence : 1;
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(calcDamage);

                        yield return new WaitForSeconds(0.5f);

                        textMessage.SetText("");
                        break;
                    }
                }
            }
        }
        else if (player.transform.eulerAngles.z == 90)
        {
            if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1].Count != 0)
            {
                foreach (GameObject enemy in objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1])
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        int damage = Random.Range(playerController.playerAttack, playerController.playerAttack + 3) + (playerController.sword?.GetSwordAttack() ?? 0);
                        int calcDamage = damage - enemy.GetComponent<EnemyController>().enemyDefence > 1 ? damage - enemy.GetComponent<EnemyController>().enemyDefence : 1;
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(calcDamage);

                        yield return new WaitForSeconds(0.5f);

                        textMessage.SetText("");
                        break;
                    }
                }
            }
        }
        else if (player.transform.eulerAngles.z == 180)
        {
            if (objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]].Count != 0)
            {
                foreach (GameObject enemy in objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]])
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        int damage = Random.Range(playerController.playerAttack, playerController.playerAttack + 3) + (playerController.sword?.GetSwordAttack() ?? 0);
                        int calcDamage = damage - enemy.GetComponent<EnemyController>().enemyDefence > 1 ? damage - enemy.GetComponent<EnemyController>().enemyDefence : 1;
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(calcDamage);

                        yield return new WaitForSeconds(0.5f);

                        textMessage.SetText("");
                        break;
                    }
                }
            }
        }
        else if (player.transform.eulerAngles.z == 270)
        {
            if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1].Count != 0)
            {
                foreach (GameObject enemy in objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1])
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        int damage = Random.Range(playerController.playerAttack, playerController.playerAttack + 3) + (playerController.sword?.GetSwordAttack() ?? 0);
                        int calcDamage = damage - enemy.GetComponent<EnemyController>().enemyDefence > 1 ? damage - enemy.GetComponent<EnemyController>().enemyDefence : 1;
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(calcDamage);

                        yield return new WaitForSeconds(0.5f);

                        textMessage.SetText("");
                        break;
                    }
                }
            }
        }
        isAttackPhase = false;
    }

    IEnumerator DetectTurnEnd()
    {
        yield return new WaitForSeconds(0.15f);

        while (!isReadyNextMove)
        {
            if (!isCameraMoving && !isAttackPhase && !areEnemiesMove && !itItemProcessPhase)
            {
                enemyInt = 0;
                isReadyNextMove = true;
            }
            else
            {
                yield return null;
            }
        }

    }

    IEnumerator ProcessEnemies()
    {
        while (isAttackPhase || isPlayerMovePhase || itItemProcessPhase) yield return null;
        areEnemiesMove = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        while (enemyInt < enemies.Length)
        {
            EnemyController enemyController = enemies[enemyInt].GetComponent<EnemyController>();
            if (enemyController.state == EnemyController.EnemyState.takingAction)
            {
                yield return null;
            }
            else if (enemyController.state == EnemyController.EnemyState.finishTurn)
            {
                enemyInt++;
            }
            else if (enemyController.state == EnemyController.EnemyState.waiting)
            {
                StartCoroutine(enemies[enemyInt].GetComponent<EnemyController>().TakeAction());
            }
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().state = EnemyController.EnemyState.waiting;
        }

        areEnemiesMove = false;
    }

    IEnumerator DisplayItemGet(string item, TextMeshProUGUI textMessage)
    {
        textMessage.SetText($"{item}を拾った");

        yield return new WaitForSeconds(0.5f);

        textMessage.SetText("");
        isPlayerMovePhase = false;
    }


    IEnumerator FlyItem()
    {
        float direction = player.transform.rotation.eulerAngles.z;
        float frameItem = 0.1f;
        bool throwLoop = true;

        if (direction == 0)
        {
            while (throwLoop && dungeonGenerator.field[thrownItemPosition[0] - 1, thrownItemPosition[1]] != 0)
            {
                objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                objectInfo[thrownItemPosition[0] - 1, thrownItemPosition[1]].Add(flyingItem);
                thrownItemPosition = new int[] { thrownItemPosition[0] - 1, thrownItemPosition[1] };
                flyingItem.transform.position = new Vector3(thrownItemPosition[1] - DungeonGenerator.dungeonSize / 2, thrownItemPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                for (int i = 0; i < objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Count; i++)
                {
                    if (objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i].CompareTag("Enemy"))
                    {
                        flyingItem.GetComponent<Item>().Collision(objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i]);
                        objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                        throwLoop = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(frameItem);
            }
            thrownItemPosition = new int[0];
            flyingItem = null;
        }
        else if (direction == 270)
        {
            while (throwLoop && dungeonGenerator.field[thrownItemPosition[0], thrownItemPosition[1] + 1] != 0)
            {
                objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                objectInfo[thrownItemPosition[0], thrownItemPosition[1] + 1].Add(flyingItem);
                thrownItemPosition = new int[] { thrownItemPosition[0], thrownItemPosition[1] + 1 };
                flyingItem.transform.position = new Vector3(thrownItemPosition[1] - DungeonGenerator.dungeonSize / 2, thrownItemPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                for (int i = 0; i < objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Count; i++)
                {
                    if (objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i].CompareTag("Enemy"))
                    {
                        flyingItem.GetComponent<Item>().Collision(objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i]);
                        objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                        throwLoop = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(frameItem);
            }
            thrownItemPosition = new int[0];
            flyingItem = null;
        }
        else if (direction == 180)
        {
            while (throwLoop && dungeonGenerator.field[thrownItemPosition[0] + 1, thrownItemPosition[1]] != 0)
            {
                objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                objectInfo[thrownItemPosition[0] + 1, thrownItemPosition[1]].Add(flyingItem);
                thrownItemPosition = new int[] { thrownItemPosition[0] + 1, thrownItemPosition[1] };
                flyingItem.transform.position = new Vector3(thrownItemPosition[1] - DungeonGenerator.dungeonSize / 2, thrownItemPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                for (int i = 0; i < objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Count; i++)
                {
                    if (objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i].CompareTag("Enemy"))
                    {
                        flyingItem.GetComponent<Item>().Collision(objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i]);
                        objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                        throwLoop = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(frameItem);
            }
            thrownItemPosition = new int[0];
            flyingItem = null;
        }
        else if (direction == 90)
        {
            while (throwLoop && dungeonGenerator.field[thrownItemPosition[0], thrownItemPosition[1] - 1] != 0)
            {
                objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                objectInfo[thrownItemPosition[0], thrownItemPosition[1] - 1].Add(flyingItem);
                thrownItemPosition = new int[] { thrownItemPosition[0], thrownItemPosition[1] - 1 };
                flyingItem.transform.position = new Vector3(thrownItemPosition[1] - DungeonGenerator.dungeonSize / 2, thrownItemPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                for (int i = 0; i < objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Count; i++)
                {
                    if (objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i].CompareTag("Enemy"))
                    {
                        flyingItem.GetComponent<Item>().Collision(objectInfo[thrownItemPosition[0], thrownItemPosition[1]][i]);
                        objectInfo[thrownItemPosition[0], thrownItemPosition[1]].Remove(flyingItem);
                        throwLoop = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(frameItem);
            }
            thrownItemPosition = new int[0];
            flyingItem = null;
        }

        itItemProcessPhase = false;
        thrownItemPosition = new int[0];
    }
}
