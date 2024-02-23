using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isReadyNextMove = false;
    public bool isAttackPhase = false;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject[,] objectInfo = new GameObject[DungeonGenerator.dungeonSize, DungeonGenerator.dungeonSize];

    // Camera Move
    private bool isCameraMoving = false;
    private Vector3 cameraPrevPos = new Vector3(0, 0, -10);
    private Vector3 cameraCurrentPos = new Vector3(0, 0, -10);
    private int cameraMoveInterpolationFramesCount = 60;
    private int cameraMoveElapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DungeonGenerator.dungeonSize; i++)
        {
            for (int j = 0; j < DungeonGenerator.dungeonSize; j++)
            {
                objectInfo[i, j] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {  

    }

    public void ProcessTurn()
    {
        isReadyNextMove = false;
        PlayerController playerController = player.GetComponent<PlayerController>();
        // Battle Phase
        if (playerController.isPlayerAttack)
        {
            if (player.transform.eulerAngles.z == 0)
            {
                if (objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]] && objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]].CompareTag("Enemy"))
                {
                    Destroy(objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]]);
                    objectInfo[playerController.playerPosition[0] - 1, playerController.playerPosition[1]] = null;
                }
            }
            else if (player.transform.eulerAngles.z == 90)
            {
                if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1] && objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1].CompareTag("Enemy"))
                {
                    Destroy(objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1]);
                    objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] - 1] = null;
                }

            }
            else if (player.transform.eulerAngles.z == 180)
            {

                if (objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]] && objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]].CompareTag("Enemy"))
                {
                    Destroy(objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]]);
                    objectInfo[playerController.playerPosition[0] + 1, playerController.playerPosition[1]] = null;
                }

            }
            else if (player.transform.eulerAngles.z == 270)
            {
                if (objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1] && objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1].CompareTag("Enemy"))
                {
                    Destroy(objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1]);
                    objectInfo[playerController.playerPosition[0], playerController.playerPosition[1] + 1] = null;
                }
            }
        }
        isAttackPhase = false;

        // Camera Move
        if (playerController.isPlayerMove)
        {
            isCameraMoving = true;
            cameraCurrentPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            StartCoroutine(MoveCamera());
        }

        // Enemy Move
        if (playerController.isPlayerAttack || playerController.isPlayerMove)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().TakeAction();
            }
        }

        playerController.isPlayerAttack = false;
        playerController.isPlayerMove = false;

        StartCoroutine(DetectTurnEnd());
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
            if (!isCameraMoving && !isAttackPhase)
            {
                isReadyNextMove = true;
            } else
            {
                yield return null;
            }
        }

    }
}
