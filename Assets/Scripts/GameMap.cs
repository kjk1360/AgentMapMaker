using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameMap {

    public enum TileType { None, 던전, 설원, 숲, 농촌, 사막, 죽음의땅, 용암 }
    public enum GameType { None, Normal, Hidden, Final }

    public int index;
    public TileType tile;
    //boss
    public bool isBoss;
    public List<int> bossIndexList;
    //wave
    public bool isWave;
    public int waveTime;
    public int monsterAcountLimit;
    public int maxMonsterAcount;
    public List<int> monsterIndexList;

    public GameType type;
    public int xCount;
    public int yCount;
    public List<Tile> tileList;


    public GameMap(int mapIndex, TileType tileType, bool isBoss, List<int> bossIndexList, bool isWave, List<int> monsterIndexList, GameType gameType, int x, int y)
    {
        this.index = mapIndex;
        this.tile = tileType;
        this.isBoss = isBoss;
        this.isWave = isWave;
        this.bossIndexList = bossIndexList;
        this.monsterIndexList = monsterIndexList;
        this.type = gameType;
        this.xCount = x;
        this.yCount = y;

    }
}
