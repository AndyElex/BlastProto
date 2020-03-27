using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect_LineBlastHoriz : Effect
{
    public Piece holder;

    public override void ResolveEffect()
    {
        var startPos = holder.currentTileId;
        var rowStartId = Math.Floor(Decimal.ToDouble(startPos / 9))*9+1;
        var rowEndId = Math.Ceiling(Decimal.ToDouble(startPos / 9)+1)*9;
        var bm = GameManager.Instance.boardManager;
        
        if (startPos % 9 == 1)
        {
            rowStartId = startPos;
            rowEndId = startPos + 8;
        }

        if (startPos % 9 == 0)
        {
            rowStartId = startPos - 8;
            rowEndId = startPos;
        }

        foreach (var tile in bm.tileDict.Values.Where(tile => tile.tileId <= rowEndId && tile.tileId >= rowStartId ))
        {
            var pieceToDestroy = bm.pieceDict[tile.occupantId];
            if (pieceToDestroy.isEffectBlocking)
                break; 
            bm.DestroyPiece(pieceToDestroy.pieceId);
            
        }
        
        bm.UpdateBoardState();
    }
}
