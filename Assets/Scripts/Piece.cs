using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isMovable;
    public bool isMatchable;
    public bool isSpawnable;
    public int colourCode;
    public int pieceId;
    public int[] size; //[X, Y]
    public int currentTileId;
    public int[] neighborPieceIds; //[n,e,s,w]
    public List<int> connectedPieceIds = new List<int>();
}
