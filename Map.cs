using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum TileType
{
    Empty= -1,
    Grass=15,
    Tree=16,
    Hills=17,
    Mountains=18,
    Towns=19,
    Castle=20,
    Monsters=21
}
public class Map {
    public Tile[] tiles;
    public int rows, columns;
    public Tile[] coastTiles
    {
        get
        {
            return tiles.Where(t => t.AutoTileID < (int)TileType.Grass).ToArray();
        }
    }
    public Tile[] LandTiles
    {
        get
        {
            return tiles.Where(t => t.AutoTileID==(int)TileType.Grass).ToArray();
        }
    }
    public Tile castletile
    {
        get
        {
            return tiles.FirstOrDefault(t => t.AutoTileID == (int)TileType.Castle);
        }
    }
    public void NewMap(int width, int height)
    {
        columns = width;
        rows=height;
        tiles = new Tile[columns * rows];
        CreateTile();
    }
    public void CreateIsland(float erode,int iterations,float trees,float hill,float mountains,float town,float monster,float lake)
    {
        DecorateTiles(LandTiles, lake, TileType.Empty);
        for (int i = 0; i < iterations; i++)
        {
            DecorateTiles(coastTiles, erode, TileType.Empty);
        }
        DecorateTiles(LandTiles, trees, TileType.Tree);
        DecorateTiles(LandTiles, hill, TileType.Hills);
        DecorateTiles(LandTiles, mountains, TileType.Mountains);
        DecorateTiles(LandTiles, town, TileType.Towns);
        DecorateTiles(LandTiles, monster, TileType.Monsters);
        Tile[] open = LandTiles;
        RandomizeTiles(open);
        open[0].AutoTileID = (int)TileType.Castle;
    }
    public void CreateTile()
    {
        int total = tiles.Length;
        for (int i=0;i<total;i++)
        {
            Tile tile = new Tile();
            tile.id = i;
            tiles[i] = tile;
        }
        FindNeighbours();
    }
    private void FindNeighbours()
    {
        for(int r=0;r<rows;++r)
        {
            for(int c=0;c<columns;c++)
            {
                Tile tile = tiles[columns * r + c];
                if(r<rows-1)
                {
                    tile.AddNeighbours(Side.Bottom, tiles[columns * (r + 1) + c]);
                }
                if(c<columns-1)
                {
                    tile.AddNeighbours(Side.Right, tiles[columns * r + c + 1]);
                }
                if(r>0)
                {
                    tile.AddNeighbours(Side.Top, tiles[columns * (r - 1) + c]);
                }
                if(c>0)
                {
                    tile.AddNeighbours(Side.Left, tiles[columns * r + c - 1]);
                }
            }
        }
    }
    public void DecorateTiles(Tile[] tiles,float percent,TileType type)
    {
        int total = Mathf.FloorToInt(tiles.Length * percent);
        RandomizeTiles(tiles);
        for(int i=0;i<total;++i)
        {
            Tile tile = tiles[i];
            if(type==TileType.Empty)
            {
                tile.ClearNeighbour();
            }
            tile.AutoTileID = (int)type;
        }
    }
    public void RandomizeTiles(Tile[] tiles)
    {
        for (int i=0;i<tiles.Length;i++)
        {
            int rand = Random.Range(0, tiles.Length);
            Tile tmp = tiles[i];
            tiles[i] = tiles[rand];
            tiles[rand] = tmp;

        }
    }

}
