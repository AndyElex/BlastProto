using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public int tileId;
    public int occupantId;
    public int[] neighborTileIds;
    public bool isSpawner;
    private bool _doOnce;

    
    public void SpawnPiece()
    {
        if (occupantId < 1 && isSpawner)
        {
            var spw = GameManager.Instance.boardManager.spawner;
            spw.SpawnPiece(Random.Range(0,spw.colourCount), tileId);
        }
    }
}
