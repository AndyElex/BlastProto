using System;

public class Blocker : Piece
{
    private BoardManager _boardManager;


    private void Awake()
    {
        _boardManager = GameManager.Instance.boardManager;
    }

    public void OnMouseUp()
    {
        StartCoroutine( _boardManager.ResolveMatch(pieceId));
    }

    private void OnDestroy()
    {
        
    }
}
