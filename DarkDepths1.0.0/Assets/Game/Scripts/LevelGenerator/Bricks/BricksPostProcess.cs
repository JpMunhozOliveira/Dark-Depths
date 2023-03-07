using Edgar.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksPostProcess : DungeonGeneratorPostProcessingComponentGrid2D
{
    Tilemap tileMap;
    TileBricksController controller;

    [SerializeField]
    [Range(0f, 1f)]
    private float probability = 0.6f;

    public override void Run(DungeonGeneratorLevelGrid2D level)
    {
        tileMap = GameObject.FindWithTag("Bricks").GetComponent<Tilemap>();
        controller = GameObject.FindWithTag("BricksController").GetComponent<TileBricksController>();

        controller.TileMap = tileMap;

        CreateBricks(tileMap);
    }

    private void CreateBricks(Tilemap tileset)
    {
        foreach (var position in tileset.cellBounds.allPositionsWithin)
        {
            if (tileset.HasTile(position))
                if (UnityEngine.Random.Range(0.1f, 1f) >= probability)
                    tileset.SetTile(position, null); 
        }
    }
}