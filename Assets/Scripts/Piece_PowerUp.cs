public class Piece_PowerUp : Piece
{
    private BoardManager _boardManager;
    public Effect effect;

    private void Awake()
    {
        _boardManager = GameManager.Instance.boardManager;
    }

    public override bool IsDroppable()
    {
        return neighborPieceIds[2] < 1 && _boardManager.tileDict[currentTileId].neighborTileIds[2] > 0;
    }
    
    private void OnMouseUp()
    {
        Destroy(transform.gameObject);
        StartCoroutine( _boardManager.ResolveMatch(pieceId));
    }

    private void OnDestroy()
    {
        effect.ResolveEffect();
        _boardManager.powerupResolved = true;
    }
    


}
