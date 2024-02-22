using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<int> pos;
    private DungeonGenerator dungeonGenerator;
    private int[,] field;
    private TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        field = dungeonGenerator.field;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeAction()
    {
        List<int[]> availableCell = new List<int[]>();
        if (field[pos[0] + 1, pos[1]] != 0)
        {
            availableCell.Add(new int[] { pos[0] + 1, pos[1] });
        }
        if (field[pos[0] - 1, pos[1]] != 0)
        {
            availableCell.Add(new int[] { pos[0] - 1, pos[1] });
        }
        if (field[pos[0], pos[1] + 1] != 0)
        {
            availableCell.Add(new int[] { pos[0], pos[1] + 1 });
        }
        if (field[pos[0], pos[1] - 1] != 0)
        {
            availableCell.Add(new int[] { pos[0], pos[1] - 1 });
        }

        int randomIndex = Random.Range(0, availableCell.Count);
        turnManager.objectInfo[pos[0], pos[1]] = null;
        pos = new List<int> { availableCell[randomIndex][0], availableCell[randomIndex][1] };
        turnManager.objectInfo[pos[0], pos[1]] = gameObject;
        gameObject.transform.position = new Vector3(pos[1] - DungeonGenerator.dungeonSize / 2, pos[0] * -1 + DungeonGenerator.dungeonSize / 2, -1);

    }
}
