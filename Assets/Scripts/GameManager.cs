using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public BoardManager boardManager;

    public bool boardResolving;
    
    
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
    
    void Update()
    {
        
    }
}
