using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreateManager : MonoBehaviour
{
    public PrefabManager prefabManager;
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            uiManager.items.Add(prefabManager.items[Random.Range(0, prefabManager.items.Length)]);
        }
    }
}
