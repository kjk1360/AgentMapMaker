using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameMap {

    public enum TileType { None, 던전, 설원, 숲, 농촌, 사막, 죽음의땅, 용암 }
    public enum GameType { None, 시간제, 몰살, 중간보스, 보스 }

    public int index;
    public TileType tile;
    public GameType type;
    public int xCount;
    public int yCount;
    public List<Tile> tileList;


    public GameMap(int mapIndex, TileType tileType, GameType gameType, int x, int y)
    {
        this.index = mapIndex;
        this.tile = tileType;
        this.type = gameType;
        this.xCount = x;
        this.yCount = y;

    }
}
