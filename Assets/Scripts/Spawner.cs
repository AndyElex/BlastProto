using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public int[] spawnProbRegPiece;
    public int colourCount;
    private int _nextPieceId = 1;
    public GameObject[] pieces;


    private void Awake()
    {
        for (var i = 0; i < pieces.Length ; i++)
        {
            pieces[i].GetComponent<Piece>().isSpawnable = i < colourCount;
        }
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
        }
    }

}
