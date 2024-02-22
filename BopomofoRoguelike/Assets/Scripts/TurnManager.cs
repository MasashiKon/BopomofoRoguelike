using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isReadyNextMove = false;
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
        if (isReadyNextMove) return;
        if (isCameraMoving)
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
        }

        if (!isCameraMoving)
        {
            isReadyNextMove = true;
        }

    }

    public void ProcessTurn()
    {
        isReadyNextMove = false;

        // Camera Move
        isCameraMoving = true;
        cameraCurrentPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        // Enemy Move
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TakeAction();
        }
    }
}
