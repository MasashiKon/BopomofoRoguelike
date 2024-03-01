using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerReady = false;
    public bool isPlayerAttack = false;
    public bool isPlayerMove = false;
    public int hp = 10;
    public Slider slider;
    public GameObject menuPanel;
    public int[] playerPosition;
    private TextMeshProUGUI textMessage;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private TurnManager turnManager;
    private UIManager uiManager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        animator = transform.GetChild(0).GetComponent<Animator>();

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

        turnManager.objectInfo[playerPosition[0], playerPosition[1]] = gameObject;
        gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
        isPlayerMove = true;
        slider.maxValue = hp;
        slider.value = hp;

        turnManager.ProcessTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.isReadyNextMove && !uiManager.isPaused)
        {
            if (Input.GetKey("up"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (field[playerPosition[0] - 1, playerPosition[1]] != 0 && turnManager.objectInfo[playerPosition[0] - 1, playerPosition[1]] == null)
                {
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = null;
                    playerPosition[0]--;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = gameObject;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                turnManager.ProcessTurn();

            }
            else if (Input.GetKey("down"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                if (field[playerPosition[0] + 1, playerPosition[1]] != 0 && turnManager.objectInfo[playerPosition[0] + 1, playerPosition[1]] == null)
                {
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = null;
                    playerPosition[0]++;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = gameObject;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                turnManager.ProcessTurn();

            }
            else if (Input.GetKey("left"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                if (field[playerPosition[0], playerPosition[1] - 1] != 0 && turnManager.objectInfo[playerPosition[0], playerPosition[1] - 1] == null)
                {
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = null;
                    playerPosition[1]--;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = gameObject;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                turnManager.ProcessTurn();

            }
            else if (Input.GetKey("right"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                if (field[playerPosition[0], playerPosition[1] + 1] != 0 && turnManager.objectInfo[playerPosition[0], playerPosition[1] + 1] == null)
                {
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = null;
                    playerPosition[1]++;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]] = gameObject;
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                turnManager.ProcessTurn();

            }
            else if (Input.GetKey("space"))
            {
                turnManager.isReadyNextMove = false;
                isPlayerAttack = true;
                turnManager.isAttackPhase = true;
                animator.Play("PlayerAttack");
            }
        }
    }

    public void DecreaseHP(int damage)
    {
        hp -= damage;
        textMessage.SetText($"{damage}のダメージ！");
        slider.value = hp;
    }

    public void IncreaseHP(int healAmount)
    {
        hp += healAmount;
        textMessage.SetText($"{healAmount}回復した！");
        slider.value = hp;
        StartCoroutine(WaitAndEraseText());
    }

    IEnumerator WaitAndEraseText()
    {
        yield return new WaitForSeconds(0.5f);
        textMessage.SetText("");
    }
}
