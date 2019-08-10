using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tile
{
    public enum TileShape { 기본, 오른쪽, 왼쪽, 위, 아래, 위_오른쪽, 위_왼쪽, 아래_오른쪽, 아래_왼쪽, 중앙, 모퉁이_아래_왼쪽, 모퉁이_아래_오른쪽, 모퉁이_위_왼쪽, 모퉁이_위_오른쪽, 비어있음 }

    public string ID;
    public int index;
    public TileShape shape;
    public bool isBlocked;
    public int Height;
    

    public Tile(string id, int index, TileShape shape, bool isblocked = false, int height = 0)
    {
        this.ID = id;
        this.index = index;
        this.shape = shape;
        this.isBlocked = isblocked;
        this.Height = height;

    }
}
