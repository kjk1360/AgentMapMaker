﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*
public class GameMap
{
    public enum TileType { None, 던전, 설원, 숲, 농촌, 사막, 죽음의땅, 용암 }
    public enum GameType { None, 시간제, 몰살, 중간보스, 보스 }

    public int index;
    public TileType tile;
    public GameType type;
    public int xCount;
    public int yCount;



    public GameMap(int mapIndex, TileType tileType, GameType gameType, int x, int y)
    {
        this.index = mapIndex;
        this.tile = tileType;
        this.type = gameType;
        this.xCount = x;
        this.yCount = y;

    }

}
*/
public class MapCreater : MonoBehaviour
{
    static public string FIRST_WARNING_TEXT = "모든 던전 옵션을 체크하십시오";
    static public string SECOND_WARNING_TEXT = "모든 데이터가 초기화됩니다";
    static public string THIRD_WARNING_TEXT = "모든 맵 설정을 완료하십시오";



    private int currentIndex;
    private int MAX_INDEX = 5;
    private bool IsWarning;

    public GameObject warningBG;
    public GameObject warningPopup;
    public GameObject warninInfo;
    public GameObject resetWarningBtn;

    public GameObject MapListBtn;
    public GameObject ResetBtn;
    public GameObject ReturnBtn;
    public GameObject TileSetBtn;
    public GameObject CompeleteBtn;
    
    public Text PageNameText;

    public GameObject firstPage;
    public GameObject mapListPage;
    public GameObject mapPage;
    public GameObject mapSecondPage;

    //firstpage
    public InputField mapNameInput;
    public InputField mapLengthInput;
    private string MapName;
    private int MapLength;
    public int MapRank;

    public List<GameMap> gameMaps;
    public int gameMapsIndex;

    //secondPage
    public GameObject MapBtnPrefab;
    public List<MapBtnScript> MapBtns;
    public GameObject MapBtnScroll;


    //map page
    private GameMap target;

    public InputField mapXField;
    public InputField mapYField;

    //map second page
    public ControllTile secondTileScript; 

    //toggleGroups
    public ToggleGroupEX mapGradeGroup;
    public ToggleGroupEX mapBGGroup;
    public ToggleGroupEX mapTypeGroup;

    private void Awake()
    {
        IsWarning = true;
        CheckWarning();
        currentIndex = 0;
        DoUpdate();
    }
    public void CheckWarning()
    {
        switch (currentIndex)
        {
            case 0:
                if(mapNameInput.text == "" || 
                    mapLengthInput.text == ""||
                    !mapGradeGroup.AnyTogglesOn())
                {
                    IsWarning = true;
                }
                else
                {
                    IsWarning = false;
                }
                break;
            case 1:
                if (isAllMapsCompelete())
                {
                    IsWarning = false;
                }
                else
                {
                    IsWarning = true;
                }
                break;
            case 2:
                if(target.tile == GameMap.TileType.None || 
                    target.type == GameMap.GameType.None || 
                    target.xCount == -1 || 
                    target.yCount == -1)
                {
                    IsWarning = true;
                }
                else
                {
                    IsWarning = false;
                }
                break;
        }
        if (IsWarning)
        {
            warningBG.SetActive(true);
        }
        else
        {
            warningBG.SetActive(false);
        }
    }

    public void DoUpdate()
    {
        switch (currentIndex)
        {
            case 0:
                PageNameText.text = "종합 맵 설정";
                firstPage.SetActive(true);
                mapListPage.SetActive(false);
                mapPage.SetActive(false);
                mapSecondPage.SetActive(false);

                MapListBtn.SetActive(true);
                ReturnBtn.SetActive(false);
                TileSetBtn.SetActive(false);
                ResetBtn.SetActive(false);
                CompeleteBtn.SetActive(false);
                break;
            case 1:
                PageNameText.text = MapName;
                firstPage.SetActive(false);
                mapListPage.SetActive(true);
                mapPage.SetActive(false);
                mapSecondPage.SetActive(false);


                if (!isAllMapsCompelete())
                {
                    MapListBtn.SetActive(false);
                    ResetBtn.SetActive(true);
                    ReturnBtn.SetActive(false);
                    TileSetBtn.SetActive(false);
                    CompeleteBtn.SetActive(false);
                }
                else
                {
                    MapListBtn.SetActive(false);
                    ResetBtn.SetActive(false);
                    ReturnBtn.SetActive(false);
                    TileSetBtn.SetActive(false);
                    CompeleteBtn.SetActive(true);
                }
                


                break;
            case 2:
                PageNameText.text = "맵 : " + gameMapsIndex.ToString() + "_설정";
                firstPage.SetActive(false);
                mapListPage.SetActive(false);
                mapPage.SetActive(true);
                mapSecondPage.SetActive(false);

                MapListBtn.SetActive(false);
                ResetBtn.SetActive(false);
                ReturnBtn.SetActive(true);
                TileSetBtn.SetActive(true);
                CompeleteBtn.SetActive(false);
                break;

            case 3:
                PageNameText.text = "맵 : " + gameMapsIndex.ToString() + "_타일";
                firstPage.SetActive(false);
                mapListPage.SetActive(false);
                mapPage.SetActive(false);
                mapSecondPage.SetActive(true);

                MapListBtn.SetActive(false);
                ResetBtn.SetActive(false);
                ReturnBtn.SetActive(true);
                TileSetBtn.SetActive(true);
                CompeleteBtn.SetActive(false);
                break;
        }

        CheckWarning();
    }



    public bool isAllMapsCompelete()
    {
        for(int i = 0; i < MapBtns.Count; i++)
        {
            if (MapBtns[i].isWarning)
            {
                return false;
            }
        }
        return true;
    }

    public void GetFieldInputByKey(int key)
    {
        switch (key)
        {
            case 0:
                    MapName = mapNameInput.text;
                break;
            case 1:
                if (mapLengthInput.text != "")
                    MapLength = Int32.Parse(mapLengthInput.text);
                else
                    MapLength = 0;
                break;
            case 2:
                if(mapXField.text != "")
                target.xCount = Int32.Parse(mapXField.text);
                break;
            case 3:
                if (mapYField.text != "")
                    target.yCount = Int32.Parse(mapYField.text);
                break;

        }

        CheckWarning();
    }
    public void GetMapGrade(int key)
    {
        MapRank = key;

        CheckWarning();
    }
    public void GetMapTileType(ToggleEX key)
    {
        if (key.IsEnable)
        {
            if (key.isOn)
            {
                switch (key.GetToggleKey())
                {
                    case 0:
                        target.tile = GameMap.TileType.던전;
                        break;
                    case 1:
                        target.tile = GameMap.TileType.설원;
                        break;
                    case 2:
                        target.tile = GameMap.TileType.숲;
                        break;
                    case 3:
                        target.tile = GameMap.TileType.농촌;
                        break;
                    case 4:
                        target.tile = GameMap.TileType.사막;
                        break;
                    case 5:
                        target.tile = GameMap.TileType.죽음의땅;
                        break;
                    case 6:
                        target.tile = GameMap.TileType.용암;
                        break;
                }
            }
            else
            {
                target.tile = GameMap.TileType.None;
            }
        }


        CheckWarning();
    }
    public void GetMapType(ToggleEX key)
    {
        if (key.IsEnable)
        {
            if (key.isOn)
            {
                switch (key.GetToggleKey())
                {
                    case 0:
                        target.type = GameMap.GameType.시간제;
                        break;
                    case 1:
                        target.type = GameMap.GameType.몰살;
                        break;
                    case 2:
                        target.type = GameMap.GameType.중간보스;
                        break;
                    case 3:
                        target.type = GameMap.GameType.보스;
                        break;
                }
            }
            else
            {
                target.type = GameMap.GameType.None;
            }
        }


        CheckWarning();
    }

    //BtnsScript
    public void ResetMaps()
    {
        WarningPopupCheck();
        mapNameInput.text = "";
        mapLengthInput.text = "";
        MapName = null;
        MapLength = 0;
        gameMaps.Clear();
        for (int i = 0; i < MapBtns.Count; i++)
        {
            Destroy(MapBtns[i].gameObject);
        }
        MapBtns.Clear();

        MapRank = 0;
        mapGradeGroup.SetAllIndexOff();
        mapTypeGroup.SetAllIndexOff();
        mapBGGroup.SetAllIndexOff();

        if (currentIndex != 0)
        {
            currentIndex = 0;
            DoUpdate();
        }

    }
    public void LoadMapEach(int index)
    {
        target = gameMaps[index];
        gameMapsIndex = index;
        if(target.xCount == -1)
        {
            mapXField.text = null;
        }
        else
        {
            mapXField.text = target.xCount.ToString();
        }
        if (target.yCount == -1)
        {
            mapYField.text = null;
        }
        else
        {
            mapYField.text = target.yCount.ToString();
        }
        if(target.tile != GameMap.TileType.None)
        {
            int tileType = (int)target.tile;
            mapBGGroup.IndexToggleOn(tileType - 1);
        }
        else
        {
            mapBGGroup.SetAllIndexOff();
        }
        if(target.type != GameMap.GameType.None)
        {
            int mapType = (int)target.type;
            mapTypeGroup.IndexToggleOn(mapType - 1);
        }
        else
        {
            mapTypeGroup.SetAllIndexOff();
        }


        currentIndex = 2;
        DoUpdate();
    }
    public void ReturnMapList()
    {
        for (int i = 0; i < MapLength; i++)
        {
            MapBtns[i].SetTag();
        }
        currentIndex = 1;
        DoUpdate();
    }

    public void GoMapListPage()
    {
        if (!IsWarning)
        {
            if (gameMaps == null)
            {
                gameMaps = new List<GameMap>();
            }
            gameMaps.Clear();
            for (int i = 0; i < MapLength; i++)
            {
                gameMaps.Add(new GameMap(i, GameMap.TileType.None, GameMap.GameType.None, -1, -1));
            }

            if (MapBtns == null)
            {
                MapBtns = new List<MapBtnScript>();
            }

            for (int i = 0; i < MapLength; i++)
            {
                GameObject MapBtn = Instantiate(MapBtnPrefab, Vector3.zero, Quaternion.identity);
                MapBtn.transform.SetParent(MapBtnScroll.transform);
                MapBtns.Add(MapBtn.GetComponent<MapBtnScript>());
                MapBtns[i].SetUp(gameMaps[i]);

                int listenerNum = i;
                MapBtns[i].gameObject.GetComponent<Button>().onClick.AddListener(() => LoadMapEach(listenerNum));
            }


            currentIndex = 1;
            DoUpdate();
        }
        else
        {
            WarningPopupActive();
        }
        
    }

    public void SetMapNext()
    {
        if (IsWarning)
        {
            WarningPopupActive();
        }
        else
        {

            secondTileScript.SetUpTiles(target);


            currentIndex = 3;
            DoUpdate();
        }
    }


















    //warningpopup
    public void WarningPopupActive()
    {
        warningPopup.SetActive(true);
        switch (currentIndex)
        {
            case 0:
                resetWarningBtn.SetActive(false);
                warninInfo.GetComponent<Text>().text = FIRST_WARNING_TEXT;
                break;
            case 1:
                resetWarningBtn.SetActive(true);
                warninInfo.GetComponent<Text>().text = SECOND_WARNING_TEXT;
                break;
            case 2:
                resetWarningBtn.SetActive(false);
                warninInfo.GetComponent<Text>().text = THIRD_WARNING_TEXT;
                break;
        }
        //warninInfo.GetComponent<Text>().text = FIRST_WARNING_TEXT;
    }
    public void WarningPopupCheck()
    {
        resetWarningBtn.SetActive(false);
        warningPopup.SetActive(false);
    }
    public void GoPrevPage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        DoUpdate();


    }
}