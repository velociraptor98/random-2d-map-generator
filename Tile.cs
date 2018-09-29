using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;


public enum Side
{
    Bottom,
    Right,
    Left,
    Top
}
public class Tile {
    public int AutoTileID;
    public int id = 0;
    public Tile[] neighbours = new Tile[4];
    public void AddNeighbours(Side side, Tile tile)
    {
        neighbours[(int)side] = tile;
        CalculateTileId();
    }
    public void RemoveNeighbour(Tile tile)
    {
        int total = neighbours.Length;
        for(int i=0;i<total;i++)
        {
            if(neighbours[i]!=null)
            {
                if(neighbours[i].id==tile.id)
                {
                    neighbours[i] = null;
                }
            }
        }
        CalculateTileId();
    }
    public void ClearNeighbour()
    {
        int total = neighbours.Length;
        for(int i=0;i<total;++i)
        {
            Tile tile = neighbours[i];
            if(tile!=null)
            {
                tile.RemoveNeighbour(this);
                neighbours[i] = null;
            }
        }
        CalculateTileId();
    }
    private void CalculateTileId()
    {
        var SideValue = new StringBuilder();
        foreach(Tile tile in neighbours)
        {
            SideValue.Append(tile == null ? "0" : "1");
        }
        AutoTileID = Convert.ToInt32(SideValue.ToString(),2);
    }

}
