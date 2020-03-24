using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect_LineBlastVert : Effect
{
    public Piece holder;
    public override void ResolveEffect()
    {
        var startPos = holder.currentTileId;
        var bm = GameManager.Instance.boardManager;

        for (var i = 1; i < 9; i++)
        {
            if (bm.tileDict.ContainsKey(startPos + (i * 9)))
            {
                var pieceToDestroy = bm.pieceDict[bm.tileDict[startPos + (i * 9)].occupantId];
                if (pieceToDestroy.isEffectBlocking)
                    break;
                bm.DestroyPiece(pieceToDestroy.pieceId);
            }
        }

        for (var j = 0; j < 9; j++)
        {
            if (bm.tileDict.ContainsKey(startPos - (j * 9)))
            {
                var pieceToDestroy = bm.pieceDict[bm.tileDict[startPos - (j * 9)].occupantId];
                if (pieceToDestroy.isEffectBlocking)
                    break;
                bm.DestroyPiece(pieceToDestroy.pieceId);
            }
        }
        
        bm.UpdateBoardState();

    }
}
