using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public GameObject gameBoard;
    public Spawner spawner;
    private int _nextOpenTileId = 1;
    public Dictionary<int,Tile> tileDict = new Dictionary<int, Tile>();
    public Dictionary<int, Piece> pieceDict = new Dictionary<int, Piece>();
    private List<int> toSearch = new List<int>();
    private List<int> searching = new List<int>();
    private List<int> searched = new List<int>();
    public List<Connection> connections = new List<Connection>();

    public void InitGameBoard()
    {
        foreach (Transform child in gameBoard.transform)
        {
            child.GetComponent<Tile>().tileId = _nextOpenTileId;
            _nextOpenTileId++;
            
            tileDict.Add(child.GetComponent<Tile>().tileId, child.GetComponent<Tile>());
        }

        for (var i = 0; i < gameBoard.transform.childCount; i++)
        {
            spawner.SpawnPiece(Random.Range(0,spawner.colourCount),i+1);
            
        }
    }

    public void UpdateTileOccupants()
    {
        foreach (var piece in pieceDict.Values)
        {
            tileDict[piece.currentTileId].occupantId = piece.pieceId;
        }
    }

    public void InitTileConnections()
    {
        foreach (var tile in tileDict.Values)
        {
            var remainder = tile.tileId % 9;
            var quotient = Math.Floor(Decimal.ToDouble(tile.tileId / 9));

            tile.neighborTileIds[3] = tile.tileId - 1;
            tile.neighborTileIds[1] = tile.tileId + 1;
            tile.neighborTileIds[0] = tile.tileId + 9;
            tile.neighborTileIds[2] = tile.tileId - 9;
            
            if (remainder == 1)
                tile.neighborTileIds[3] = 0;
            if (remainder == 0)
                tile.neighborTileIds[1] = 0;
            if (quotient > 7)
                tile.neighborTileIds[0] = 0;
            if (quotient < 1)
                tile.neighborTileIds[2] = 0;
            
        }
        UpdateTileOccupants();
        UpdateNeighborPieces();
        connections.Clear();
        UpdatePieceConnections(1, true);
    }

    public void UpdateNeighborPieces()
    {
        foreach (var piece in pieceDict.Values)
        {
            Array.Clear(piece.neighborPieceIds, 0, piece.neighborPieceIds.Length);
            
            var tile = tileDict[piece.currentTileId];
            
            for (var i = 0; i < tile.neighborTileIds.Length; i++)
            {
                if (tile.neighborTileIds[i] > 0)
                {
                    piece.neighborPieceIds[i] = tileDict[tile.neighborTileIds[i]].occupantId;
                }
            }
        }
    }

    public void UpdatePieceConnections(int startPieceId, bool firstInstance)
    {
        if (firstInstance)
        {
            searched.Clear();
            searching.Clear();
            toSearch.Clear();

            foreach (var piece in pieceDict.Values)
            {
                toSearch.Add(piece.pieceId);
            }
        }

        for(var i=0; i < toSearch.Count; i++)
        {
            var id = toSearch[i];
            var piece = pieceDict[id];
            
            if (searching.Contains(id))
            {
                break;
            }
            
            searching.Add(id);
            toSearch.Remove(id);

            foreach (var nPieceId in piece.neighborPieceIds)
            {
                if (nPieceId > 0)
                {
                    var nPiece = pieceDict[nPieceId];
                                    
                    if (nPiece.isMatchable && nPiece.colourCode == piece.colourCode)
                    {
                        var tempList = new List<int>() {id, nPieceId};
                        var connectionExists = false;
                        
                        foreach (var connection in connections.Where(connection => tempList.Intersect(connection.Match).Any()))
                        {
                            connection.Match = connection.Match.Union(tempList).ToList();
                            connectionExists = true;
                        }

                        if (!connectionExists)
                        {
                            var nc = new Connection();
                            nc.Match.AddRange(tempList);
                            
                            connections.Add(nc);
                        }
                        
                        UpdatePieceConnections(nPieceId, false);
                    }
                }
            }
            
            searched.Add(id);
            searching.Remove(id);
        }
    }
    
    public void ResolveMatch(int pieceId)
    {
        foreach (var connection in connections)
        {
            if (connection.Match.Contains(pieceId))
            {
                DestroyConnection(connection);
            }
        }
        
        //go to resolution of the whole board
        //drop pieces
        //spawn new ones
    }

    private void DestroyConnection(Connection connection)
    {
        foreach (var pieceId in connection.Match)
        {
            Destroy(pieceDict[pieceId].transform.gameObject);
        }
    }
}
