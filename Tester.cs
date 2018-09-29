using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {


    [Header("MAP SIZE")]
    public int MapWidth = 20;
    public int MapHeight = 20;
    [Space]
    [Header("Map Visualization")]
    public GameObject container;
    public GameObject TilePrefab;
    public Vector2 TileSize = new Vector2(16,16);
    [Space]
    [Header("MAP SPRITES")]
    public Texture2D islandtexture;
    [Space]
    [Header("Populate Map")]
    [Range(0, 0.9f)]
    public float erode = 0.5f;
    [Range(0, 0.9f)]
    public float tree_percentage = 0.3f;
    [Range(0, 0.9f)]
    public float hill_percentage = 0.2f;
    [Range(0, 0.9f)]
    public float mountain_percentage = 0.1f;
    [Range(0, 0.9f)]
    public float towns_percentage = 0.05f;
    [Range(0, 0.9f)]
    public float monster_percentage = 0.3f;
    [Range(0, 0.9f)]
    public float lake_percentage = 0.05f;
    public int erodeIter = 2;


    public Map map;
	// Use this for initialization
	void Start () {
        map = new Map();
	}
	
	public void MakeMap()
    {
        map.NewMap(MapWidth, MapHeight);
        map.CreateIsland(erode,erodeIter,tree_percentage,hill_percentage,mountain_percentage,towns_percentage,monster_percentage,lake_percentage);
        CreateGrid();
        center(map.castletile.id);
    }
    private void CreateGrid()
    {
        ClearMap();
        Sprite[] sprites = Resources.LoadAll<Sprite>(islandtexture.name);
        int total = map.tiles.Length;
        int MaxColumns = map.columns;
        int col = 0;
        int row = 0;
        for (int i = 0; i < total; i++)
        {
            col = i % MaxColumns;
            float x = col * TileSize.x;
            float y = -row * TileSize.y;
            var obj = Instantiate(TilePrefab);
            obj.transform.SetParent(container.transform);
            obj.transform.position = new Vector3(x, y, 0);
            Tile tile = map.tiles[i];
            int SpriteId = tile.AutoTileID;
            if (SpriteId > 0)
            {
                SpriteRenderer spriterender = obj.GetComponent<SpriteRenderer>();
                spriterender.sprite = sprites[SpriteId];
            }
            if (col == MaxColumns - 1)
                row +=1 ;
        }
}
    private void ClearMap()
    {
        var child = container.transform.GetComponentsInChildren<Transform>();
        for(int i=child.Length-1;i>0;i--)
        {
            Destroy(child[i].gameObject);
        }
    }
    private void center(int index)
    {
        Vector3 pos = Camera.main.transform.position;
        int width = map.columns;
        pos.x = (index % width) * TileSize.x;
        pos.y = -((index % width) * TileSize.y);
        Camera.main.transform.position = pos;
    }
}
