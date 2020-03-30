using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public int[] spawnProbRegPiece;
    public int colourCount;
    private int _nextPieceId = 1;
    public GameObject[] pieces;
    public GameObject rocket;


    private void Awake()
    {
        for (var i = 0; i < pieces.Length ; i++)
        {
            pieces[i].GetComponent<Piece>().isSpawnable = i < colourCount;
        }
    }

    private void Update()
    {
        
    }

    public void SpawnPiece(int typeId, int tileId)
    {
        var bm = GameManager.Instance.boardManager;
        var tile = bm.tileDict[tileId];

        if (pieces[typeId].GetComponent<Piece>().isSpawnable)
        {
            var newObj = Instantiate(pieces[typeId], tile.transform.position, Quaternion.identity);
            newObj.transform.SetParent(tile.transform);
            
            var newPiece = newObj.GetComponent<Piece>();
            newPiece.pieceId = _nextPieceId;
            newPiece.currentTileId = tileId;
            _nextPieceId++;

            bm.tileDict[tileId].occupantId = newPiece.pieceId;
            bm.pieceDict.Add(newPiece.pieceId, newPiece);
            
            bm.UpdateBoardState();
        }
    }

    public void PaintPiece(int tileId)
    {
        var bm = GameManager.Instance.boardManager;
        var currentTile = bm.tileDict[tileId];
        var currentPiece = bm.pieceDict[currentTile.occupantId];

        bm.pieceDict.Remove(currentPiece.pieceId);
        Destroy(currentPiece.transform.gameObject);

        var newPiece = Instantiate(GameManager.Instance.paintObject, currentTile.transform.position, Quaternion.identity).GetComponent<Piece>();
        newPiece.transform.SetParent(currentTile.transform);
        
        newPiece.pieceId = _nextPieceId;
        _nextPieceId++;
        newPiece.currentTileId = tileId;
        bm.pieceDict.Add(newPiece.pieceId, newPiece);

        bm.tileDict[tileId].occupantId = newPiece.pieceId;
        
        bm.UpdateBoardState();
    }

    public IEnumerator SpawnPowerUp(int matchSize, int tileId)
    {
        var bm = GameManager.Instance.boardManager;
        
        switch(matchSize)
        {
            case 5:
            case 6:
                yield return StartCoroutine(SpawnRocket(tileId));
                break;
            case 7:
            case 8:
                //make bomb
                break;
        }

        if (matchSize >= 9)
        {
            //make a swirl
        }
        
        bm.UpdateBoardState();
        
    }

    private IEnumerator SpawnRocket(int tileId)
    {
        var bm = GameManager.Instance.boardManager;
        var tile = bm.tileDict[tileId];
        
        var newObj = Instantiate(rocket, tile.transform.position, Quaternion.identity);
        newObj.transform.SetParent(tile.transform);
            
        var newPiece = newObj.GetComponent<Piece>();
        newPiece.pieceId = _nextPieceId;
        newPiece.currentTileId = tileId;
        _nextPieceId++;

        bm.tileDict[tileId].occupantId = newPiece.pieceId;
        bm.pieceDict.Add(newPiece.pieceId, newPiece);
        
        bm.UpdateBoardState();

        yield return null;
    }

}
