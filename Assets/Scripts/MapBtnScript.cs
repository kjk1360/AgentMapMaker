using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBtnScript : MonoBehaviour {

    private GameMap thisGameMap;

    public GameObject indexTag;
    public GameObject tileTag;
    public GameObject typeTag;
    public GameObject sizeTag;

    public Text indexText;
    public Text tileText;
    public Text typeText;
    public Text sizeText;

    public GameObject warningObject;

    public bool isWarning;

    public void SetUp(GameMap gameMap)
    {
        thisGameMap = gameMap;
        SetTag();
    }

    public void SetTag()
    {
        indexTag.SetActive(true);
        tileTag.SetActive(false);
        typeTag.SetActive(false);
        sizeTag.SetActive(false);

        indexText.text = thisGameMap.index.ToString();
        if(thisGameMap.tile != GameMap.TileType.None)
        {
            tileTag.SetActive(true);
            tileText.text = thisGameMap.tile.ToString();
        }
        if (thisGameMap.type != GameMap.GameType.None)
        {
            typeTag.SetActive(true);
            typeText.text = thisGameMap.type.ToString();
        }
        if (thisGameMap.xCount != -1 && thisGameMap.yCount != -1)
        {
            sizeTag.SetActive(true);
            sizeText.text = "(" + thisGameMap.xCount.ToString() + ", " + thisGameMap.yCount.ToString() + ")";
        }

        if(thisGameMap.tile == GameMap.TileType.None || thisGameMap.type == GameMap.GameType.None || thisGameMap.xCount == -1 || thisGameMap.yCount == -1)
        {
            warningObject.SetActive(true);
            isWarning = true;
        }
        else
        {
            warningObject.SetActive(false);
            isWarning = false;
        }
    }
    
}
