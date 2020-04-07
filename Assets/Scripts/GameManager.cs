using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public BoardManager boardManager;
    public static bool PaintMode;
    public GameObject paintObject;
    public bool powerupResolved;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PaintMode = false;
        }
    }

    public void ActivatePaintMode(GameObject objectToPaint)
    {
        paintObject = objectToPaint;
        PaintMode = true;
    }
}
