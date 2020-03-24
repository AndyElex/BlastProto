using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isMatchable;
    public bool isSpawnable;
    public bool isEffectBlocking;
    public int colourCode;
    public int pieceId;
    public int[] size; //[X, Y]
    public int currentTileId;
    public int[] neighborPieceIds; //[n,e,s,w]

    protected virtual void OnNeighborDestroyed()
    {
        
    }

    public virtual bool IsDroppable()
    {
        return false;
    }
    
    
}
