﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public BoardManager boardManager;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        boardManager.InitGameBoard();
        boardManager.InitTileConnections();
    }
    
}
