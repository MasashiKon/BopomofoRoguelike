using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReloader : MonoBehaviour
{
    public List<ItemParameter> items = new List<ItemParameter>();
    public int? playerHP = null;
    public int floor = 1;
    private static SceneReloader instance;

    private void Awake()
    {
        if (GetInstance() == null)
        {
            // このオブジェクトをシーンの切り替え時に破棄されないようにする
            DontDestroyOnLoad(gameObject);
            // インスタンスを設定する
            instance = this;
        }
        else
        {
            // 既にインスタンスが存在する場合、新しいインスタンスを破棄する
            Destroy(gameObject);
        }
    }

    public static SceneReloader GetInstance()
    {
        return instance;
    }
}
