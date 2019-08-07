using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileShape { 기본, 오른쪽, 왼쪽, 위, 아래, 위_오른쪽, 위_왼쪽, 아래_오른쪽, 아래_왼쪽, 중앙 }

    public string ID;
    public int index;
    public TileShape shape;

    public Tile(string id, int index, TileShape shpe)
    {
        this.ID = id;
        this.index = index;
        this.shape = shpe;

    }
}
