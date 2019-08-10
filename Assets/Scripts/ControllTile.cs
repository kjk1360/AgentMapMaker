using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllTile : MonoBehaviour {
    private GameMap m_Map;
    static public float MAX_SIZE_X = 980f;
    static public float MAX_SIZE_Y = 1320;

    private float m_tileLength;
    private GridLayoutGroup gridGroup;
    public GameObject tilePrefab;

    private List<TileScript> tileList;

    public void SetUpTiles(GameMap target, MapCreater creater)
    {
        m_Map = target;
        gridGroup = gameObject.GetComponent<GridLayoutGroup>();
        if(tileList == null)
        {
            tileList = new List<TileScript>();
        }
        
        for(int i = 0; i < tileList.Count; i++)
        {
            Destroy(tileList[i].gameObject);
        }
        tileList.Clear();

        if(target.tileList == null)
        {
            target.tileList = new List<Tile>();
        }
        else
        {
            target.tileList.Clear();
        }


        if((MAX_SIZE_X) / target.xCount < (MAX_SIZE_Y) / target.yCount)
        {
            m_tileLength = (MAX_SIZE_X) / target.xCount;
        }
        else
        {
            m_tileLength = (MAX_SIZE_Y) / target.yCount;
        }

        gridGroup.cellSize = new Vector2(m_tileLength, m_tileLength);
        gridGroup.constraintCount = target.yCount;

        int indexTile = 0;
        for(int i = 0; i < target.xCount; i++)
        {
            for(int j = 0; j < target.yCount; j++)
            {
                GameObject tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                Tile tileScript = new Tile(i.ToString() + "_" + j.ToString(), indexTile, Tile.TileShape.기본);
                target.tileList.Add(tileScript);
                tile.GetComponent<TileScript>().SetTileScript(tileScript, creater);
                indexTile++;
                

                tileList.Add(tile.GetComponent<TileScript>());

                tile.transform.SetParent(gameObject.transform);

            }
        }

    }

    public List<TileScript> GetAllTiles()
    {
        return tileList;
    }
    public void SelectAll()
    {
        for(int i = 0; i < tileList.Count; i++)
        {
            tileList[i].Select();
        }
    }
    public void UnSelectAll()
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].UnSelect();
        }
    }

    public void CompeleteMapMake()
    {
        foreach(TileScript tilescrt in tileList)
        {
            tilescrt.CompeleteMaking();
        }
    }

}
