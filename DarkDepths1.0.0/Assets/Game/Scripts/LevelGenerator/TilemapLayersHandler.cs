using Edgar.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileKeys
{
    public const string TileFloor = "Floor";
    public const string TileWall = "Walls";
    public const string TileBricks = "Bricks";
    public const string TileForeground = "Foreground";
}

[CreateAssetMenu(menuName = "Edgar/Tilemap Layers Handler", fileName = "Tilemap Layers Handler")]
public class TilemapLayersHandler : TilemapLayersHandlerBaseGrid2D
{
    public override void InitializeTilemaps(GameObject gameObject)
    {

        var grid = gameObject.AddComponent<Grid>();

        CreateTilemapGameObject(TileKeys.TileFloor, gameObject, -10);
        var wallsTilemapObject = CreateTilemapGameObject(TileKeys.TileWall, gameObject, -5);
        var bricksTilemapObject = CreateTilemapGameObject(TileKeys.TileBricks, gameObject, 0);
        CreateTilemapGameObject(TileKeys.TileForeground, gameObject, 20);

        AddCompositeCollider(wallsTilemapObject, RigidbodyType2D.Static);
        AddCompositeCollider(bricksTilemapObject, RigidbodyType2D.Kinematic);
    
    }

    protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
    {
        var tilemapObject = new GameObject(name);
        tilemapObject.transform.SetParent(parentObject.transform);
        tilemapObject.transform.position += new Vector3(0.5f, 0.5f, 0);
       
        tilemapObject.tag = name;
        tilemapObject.layer = LayerMask.NameToLayer(name);

        var tilemap = tilemapObject.AddComponent<Tilemap>();
        var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingOrder = sortingOrder;

        return tilemapObject;
    }

    protected void AddCompositeCollider(GameObject tilemapGameObject, RigidbodyType2D type, bool isTrigger = false)
    {
        var tilemapCollider2D = tilemapGameObject.AddComponent<TilemapCollider2D>();
        tilemapCollider2D.usedByComposite = true;

        var compositeCollider2d = tilemapGameObject.AddComponent<CompositeCollider2D>();
        compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
        compositeCollider2d.isTrigger = isTrigger;

        tilemapGameObject.GetComponent<Rigidbody2D>().bodyType = type;
    }
}