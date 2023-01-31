using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameObject[] datas;

    void Awake()
    {
        instance = this;

        datas = GameObject.FindGameObjectsWithTag("DATA");

        if (datas.Length >= 2)
            Destroy(datas[0]);

        DontDestroyOnLoad(transform.gameObject);
    }
}
