using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBricksController : MonoBehaviour
{
    public static TileBricksController Instance { get; private set; }
    public Tilemap TileMap { get => tileMap; set => tileMap = value; }

    [SerializeField]
    Tilemap tileMap;
    [SerializeField]
    SpawnPowerUps spawn;
    [SerializeField]
    GameObject AnimationDestroyBrick;

    void Start()
    {
        Instance = this;
        spawn = GetComponent<SpawnPowerUps>();
    }

    public void DestroyBrick(Vector3 position)
    {
        Vector3Int coordinate = tileMap.WorldToCell(position);
        tileMap.SetTile(coordinate, null);

        Vector3 pos = tileMap.GetCellCenterWorld(coordinate);
        spawn.SpawnPowerUp(pos);
    }

    public void SpawnAnimationDestroy(Vector3 position)
    {
        Vector3Int coordinate = tileMap.WorldToCell(position);
        Vector3 pos = tileMap.GetCellCenterWorld(coordinate);

        Instantiate(AnimationDestroyBrick, position, Quaternion.identity);
    }
}
