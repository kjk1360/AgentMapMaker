using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour {

    public Tile m_tile;

    private int tileNum;


    public void SetTileScript(Tile tile)
    {
        m_tile = tile;
        tileNum = 23;
        GetComponent<Image>().sprite = (Resources.Load("Images/ice/Tiles/0") as Sprite);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RightClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("click1");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("click2");
        }
    }

    public void RightClick()
    {
        if(tileNum < 27)
        {
            tileNum++;
        }
        else
        {
            tileNum = 0;
        }
        GetComponent<Image>().sprite = Resources.Load("Images/ice/Tiles/" + tileNum.ToString()) as Sprite;
        
    }
}
