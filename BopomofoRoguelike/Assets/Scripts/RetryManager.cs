using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{
    SceneReloader sceneReloader;
    // Start is called before the first frame update
    void Start()
    {
        sceneReloader = GameObject.Find("Scene Reloader").GetComponent<SceneReloader>();
        UIManager uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();

        uiManager.isPaused = true;
        uiManager.isFreeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        sceneReloader.playerHP = 15;
        sceneReloader.floor = 1;
        sceneReloader.items = new List<ItemParameter>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
