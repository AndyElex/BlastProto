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

    public IEnumerator SpawnPowerUp(int matchSize, int tileId)
    {
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

        yield return null;
    }

}
