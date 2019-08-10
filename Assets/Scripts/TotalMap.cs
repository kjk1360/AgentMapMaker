using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TotalMap{

    public enum Rank { D급1, D급2, D급3, C급1, C급2, C급3, B급1, B급2, B급3, A급1, A급2, A급3, S급, SS급, SSS급}

    public string id;
    public string name;
    public List<GameMap> gameMapList;
    public int mapLength;
    public Rank rank;

    public TotalMap(string id, string name, List<GameMap> list, int length, Rank rank)
    {
        this.id = id;
        this.name = name;
        this.gameMapList = list;
        this.mapLength = length;
        this.rank = rank;
    }
}
