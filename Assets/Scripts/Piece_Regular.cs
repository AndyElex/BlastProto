using UnityEngine;

public class Piece_Regular : Piece
{
    public bool isBeingDestroyed = false;
    private BoardManager _boardManager;
    
    public override bool IsDroppable()
    {
        return neighborPieceIds[2] < 1 && _boardManager.tileDict[currentTileId].neighborTileIds[2] > 0;
    }

    private void Awake()
    {
        _boardManager = GameManager.Instance.boardManager;
        
        switch (colourCode)
        {
            case 0:
                GetComponent<Renderer>().material.color = Color.red;
                break;

            case 1:
                GetComponent<Renderer>().material.color = Color.green;
                break;

            case 2:
                GetComponent<Renderer>().material.color = Color.blue;
                break;

            case 3:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;

            case 4:
                GetComponent<Renderer>().material.color = Color.magenta;
                break;

            case 5:
                GetComponent<Renderer>().material.color = Color.cyan;
                break;
        }
    }

    public new void OnMouseUp()
    {
        base.OnMouseUp();
        StartCoroutine( _boardManager.ResolveMatch(pieceId));
    }

    private void OnDestroy()
    {
        isBeingDestroyed = true;
        
    }
}
