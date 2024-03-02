using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isReadyNextMove = false;
    public bool isAttackPhase = false;
    public bool isPlayerMovePhase = false;
    public bool areEnemiesMove = false;
    public GameObject mainCamera;
    public GameObject player;
    public List<GameObject>[,] objectInfo = new List<GameObject>[DungeonGenerator.dungeonSize, DungeonGenerator.dungeonSize];
    public GameObject message;

    // Camera Move
    private bool isCameraMoving = false;
    private Vector3 cameraPrevPos = new Vector3(0, 0, -10);
    private Vector3 cameraCurrentPos = new Vector3(0, 0, -10);
    private int cameraMoveInterpolationFramesCount = 60;
    private int cameraMoveElapsedFrames = 0;
    private int enemyInt = 0;
    private UIManager uiManager;

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

        // Camera Move
        if (playerController.isPlayerMove)
        {
            isPlayerMovePhase = true;
            if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Exists(ob => ob.CompareTag("Item")))
            {
                for (int i = 0; i < objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].Count; i++)
                {
                    if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]][i].CompareTag("Item"))
                    {
                        Destroy(objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]][i]);
                        objectInfo[playerController.playerPosition[0], playerController.playerPosition[1]].RemoveAt(i);
                        uiManager.items.Add(new Herb("やくそう"));
                        StartCoroutine(DisplayItemGet("やくそう", textMessage));
                    }
                }
            }
            else
            {
                isPlayerMovePhase = false;
            }

            isCameraMoving = true;
            cameraCurrentPos = new Vector3(player.transform.position.x, player.transform.position.y - 1, -10);
            StartCoroutine(MoveCamera());
        }

        // Enemy Move
        if (playerController.isPlayerAttack || playerController.isPlayerMove || playerController.isPlayerUseItem)
        {
            StartCoroutine(ProcessEnemies());
        }

        playerController.isPlayerAttack = false;
        playerController.isPlayerMove = false;
        playerController.isPlayerUseItem = false;

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
                        int damage = Random.Range(3, 6);
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(damage);

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
                        int damage = Random.Range(3, 6);
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(damage);

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
                        int damage = Random.Range(3, 6);
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(damage);

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
                        int damage = Random.Range(3, 6);
                        textMessage.SetText("プレイヤーの攻撃！");
                        yield return new WaitForSeconds(0.3f);

                        enemy.GetComponent<EnemyController>().DecreaceHP(damage);

                        yield return new WaitForSeconds(0.5f);

                        textMessage.SetText("");
                        break;
                    }
                }
            }
        }
        isAttackPhase = false;
    }

    IEnumerator MoveCamera()
    {
        while (isCameraMoving)
        {
            float interpolationRatio = (float)cameraMoveElapsedFrames / cameraMoveInterpolationFramesCount;

            Vector3 interpolatedPosition = Vector3.Lerp(cameraPrevPos, cameraCurrentPos, interpolationRatio);
            mainCamera.transform.position = interpolatedPosition;

            cameraMoveElapsedFrames = (cameraMoveElapsedFrames + 1) % (cameraMoveInterpolationFramesCount + 1);
            if (cameraMoveElapsedFrames == 0)
            {
                isCameraMoving = false;
                cameraPrevPos = cameraCurrentPos;
            }

            yield return null;
        }

    }


    IEnumerator DetectTurnEnd()
    {
        yield return new WaitForSeconds(0.15f);

        while (!isReadyNextMove)
        {
            if (!isCameraMoving && !isAttackPhase && !areEnemiesMove)
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
        while (isAttackPhase || isPlayerMovePhase) yield return null;
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
}
