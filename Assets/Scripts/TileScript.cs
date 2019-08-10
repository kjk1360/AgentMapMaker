using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour {


    private MapCreater m_creater;


    public Tile m_tile;

    private Tile.TileShape tileType;
    private int tileHeight;
    private bool isSelect;
    private bool isBlocked;
    //info
    public GameObject heightText;
    public GameObject selectCheck;

    public void SetTileScript(Tile tile, MapCreater creater)
    {
        m_creater = creater;
        m_tile = tile;
        tileHeight = 0;
        tileType = Tile.TileShape.기본;
        isSelect = false;
        isBlocked = false;
        gameObject.GetComponent<Image>().sprite = m_creater.GetTileSprite(0);
    }
    
    public void Clicked()
    {
        if (isSelect)
        {
            UnSelect();
        }
        else
        {
            Select();
        }
    }
    
    public void Select()
    {
        isSelect = true;
        selectCheck.SetActive(true);
        if (!m_creater.SelectTileScripts.Contains(this))
            m_creater.SelectTileScripts.Add(this);
    }
    public void UnSelect()
    {
        isSelect = false;
        selectCheck.SetActive(false);
        if (m_creater.SelectTileScripts.Contains(this))
            m_creater.SelectTileScripts.Remove(this);
    }
    public void ChangeShape(int index)
    {
        if(index == 14)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 60f / 255f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
        gameObject.GetComponent<Image>().sprite = m_creater.GetTileSprite(index);
        switch (index)
        {
            case 0:
                tileType = Tile.TileShape.기본;
                isBlocked = false;
                break;
            case 1:
                tileType = Tile.TileShape.위_왼쪽;
                isBlocked = false;
                break;
            case 2:
                tileType = Tile.TileShape.위_오른쪽;
                isBlocked = false;
                break;
            case 3:
                tileType = Tile.TileShape.아래_왼쪽;
                isBlocked = false;
                break;
            case 4:
                tileType = Tile.TileShape.아래_오른쪽;
                isBlocked = false;
                break;
            case 5:
                tileType = Tile.TileShape.위;
                isBlocked = false;
                break;
            case 6:
                tileType = Tile.TileShape.아래;
                isBlocked = false;
                break;
            case 7:
                tileType = Tile.TileShape.오른쪽;
                isBlocked = false;
                break;
            case 8:
                tileType = Tile.TileShape.왼쪽;
                isBlocked = false;
                break;
            case 9:
                tileType = Tile.TileShape.중앙;
                isBlocked = false;
                break;
            case 10:
                tileType = Tile.TileShape.모퉁이_아래_오른쪽;
                isBlocked = false;
                break;
            case 11:
                tileType = Tile.TileShape.모퉁이_아래_왼쪽;
                isBlocked = false;
                break;
            case 12:
                tileType = Tile.TileShape.모퉁이_위_오른쪽;
                isBlocked = false;
                break;
            case 13:
                tileType = Tile.TileShape.모퉁이_위_왼쪽;
                isBlocked = false;
                break;
            case 14:
                tileType = Tile.TileShape.비어있음;
                isBlocked = true;
                heightText.GetComponent<Text>().text = " ";
                tileHeight = -1;
                break;

        }
        
    }

    public void ChangeHeight(int height)
    {
        if (!isBlocked)
        {
            if (height > 2)
            {
                height = 2;
            }
            heightText.GetComponent<Text>().text = height.ToString();
            tileHeight = height;
        }
        else
        {
            heightText.GetComponent<Text>().text = " ";
            tileHeight = -1;
        }
        
    }

    public void CompeleteMaking()
    {
        m_tile.shape = tileType;
        m_tile.Height = tileHeight;
        m_tile.isBlocked = isBlocked;
    }
}
