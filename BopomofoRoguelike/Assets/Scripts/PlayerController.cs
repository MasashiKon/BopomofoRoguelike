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
    public bool isPlayerUseItem = false;
    public int hp = 15;
    public Slider slider;
    public GameObject menuPanel;
    public GameObject stairPanel;
    public int[] playerPosition;
    public int playerAttack = 3;
    public Sword sword;
    public int playerDefence = 0;
    public Shield shield;
    public GameObject mainCamera;

    private TextMeshProUGUI textMessage;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private TurnManager turnManager;
    private UIManager uiManager;
    private Animator animator;
    private bool isCameraMoving = false;
    private Vector3 cameraPrevPos = new Vector3(0, 0, -10);
    private Vector3 cameraCurrentPos = new Vector3(0, 0, -10);
    private int cameraMoveInterpolationFramesCount = 40;
    private int cameraMoveElapsedFrames = 0;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        field = dungeonGenerator.field;

        int playerIndex = Random.Range(0, dungeonGenerator.availableCell.Count);
        playerPosition = dungeonGenerator.availableCell[playerIndex];

        turnManager.objectInfo[playerPosition[0], playerPosition[1]].Add(gameObject);
        gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
        isPlayerMove = true;
        slider.maxValue = hp;
        slider.value = hp;

        isCameraMoving = true;
        cameraCurrentPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -10);
        StartCoroutine(MoveCamera());
        turnManager.isReadyNextMove = true;
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
                if (field[playerPosition[0] - 1, playerPosition[1]] != 0 && (turnManager.objectInfo[playerPosition[0] - 1, playerPosition[1]] == null || !turnManager.objectInfo[playerPosition[0] - 1, playerPosition[1]].Exists(ob => ob.CompareTag("Enemy"))))
                {
                    for (int i = 0; i < turnManager.objectInfo[playerPosition[0], playerPosition[1]].Count; i++)
                    {
                        if (turnManager.objectInfo[playerPosition[0], playerPosition[1]][i] == gameObject)
                        {
                            turnManager.objectInfo[playerPosition[0], playerPosition[1]].RemoveAt(i);
                        }

                    }
                    playerPosition[0]--;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]].Add(gameObject);
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                isCameraMoving = true;
                cameraCurrentPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -10);
                StartCoroutine(MoveCamera());

                NextFloorOrStay();

            }
            else if (Input.GetKey("down"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                if (field[playerPosition[0] + 1, playerPosition[1]] != 0 && (turnManager.objectInfo[playerPosition[0] + 1, playerPosition[1]] == null || !turnManager.objectInfo[playerPosition[0] + 1, playerPosition[1]].Exists(ob => ob.CompareTag("Enemy"))))
                {
                    for (int i = 0; i < turnManager.objectInfo[playerPosition[0], playerPosition[1]].Count; i++)
                    {
                        if (turnManager.objectInfo[playerPosition[0], playerPosition[1]][i] == gameObject)
                        {
                            turnManager.objectInfo[playerPosition[0], playerPosition[1]].RemoveAt(i);
                        }

                    }
                    playerPosition[0]++;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]].Add(gameObject);
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                isCameraMoving = true;
                cameraCurrentPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -10);
                StartCoroutine(MoveCamera());

                NextFloorOrStay();

            }
            else if (Input.GetKey("left"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                if (field[playerPosition[0], playerPosition[1] - 1] != 0 && (turnManager.objectInfo[playerPosition[0], playerPosition[1] - 1] == null || !turnManager.objectInfo[playerPosition[0], playerPosition[1] - 1].Exists(ob => ob.CompareTag("Enemy"))))
                {
                    for (int i = 0; i < turnManager.objectInfo[playerPosition[0], playerPosition[1]].Count; i++)
                    {
                        if (turnManager.objectInfo[playerPosition[0], playerPosition[1]][i] == gameObject)
                        {
                            turnManager.objectInfo[playerPosition[0], playerPosition[1]].RemoveAt(i);
                        }

                    }
                    playerPosition[1]--;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]].Add(gameObject);
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                isCameraMoving = true;
                cameraCurrentPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -10);
                StartCoroutine(MoveCamera());

                NextFloorOrStay();

            }
            else if (Input.GetKey("right"))
            {
                turnManager.isReadyNextMove = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                if (field[playerPosition[0], playerPosition[1] + 1] != 0 && (turnManager.objectInfo[playerPosition[0], playerPosition[1] + 1] == null || !turnManager.objectInfo[playerPosition[0], playerPosition[1] + 1].Exists(ob => ob.CompareTag("Enemy"))))
                {
                    for (int i = 0; i < turnManager.objectInfo[playerPosition[0], playerPosition[1]].Count; i++)
                    {
                        if (turnManager.objectInfo[playerPosition[0], playerPosition[1]][i] == gameObject)
                        {
                            turnManager.objectInfo[playerPosition[0], playerPosition[1]].RemoveAt(i);
                        }

                    }
                    playerPosition[1]++;
                    turnManager.objectInfo[playerPosition[0], playerPosition[1]].Add(gameObject);
                    gameObject.transform.position = new Vector3(playerPosition[1] - DungeonGenerator.dungeonSize / 2, playerPosition[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);
                    isPlayerMove = true;

                }

                isCameraMoving = true;
                cameraCurrentPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -10);
                StartCoroutine(MoveCamera());

                NextFloorOrStay();

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
        int calcedDamage = damage - playerDefence - (shield?.GetSwordDefence() ?? 0) > 1 ? damage - playerDefence - (shield?.GetSwordDefence() ?? 0) : 1;
        hp -= calcedDamage > 1 ? calcedDamage : 1;
        textMessage.SetText($"{calcedDamage}のダメージ！");
        slider.value = hp;
    }

    public void IncreaseHP(int healAmount)
    {
        hp += healAmount;
        textMessage.SetText($"{healAmount}回復した！");
        slider.value = hp;
        StartCoroutine(WaitAndEraseText());
    }

    public void NextFloorOrStay()
    {
        if (turnManager.objectInfo[playerPosition[0], playerPosition[1]].Find(ob => ob.CompareTag("Stair")))
        {
            stairPanel.SetActive(true);
        }
        else
        {
            turnManager.ProcessTurn();
        }
    }

    IEnumerator WaitAndEraseText()
    {
        yield return new WaitForSeconds(0.5f);
        textMessage.SetText("");

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
}
