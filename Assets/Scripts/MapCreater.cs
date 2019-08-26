using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
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

    //btns
    public GameObject MapListBtn;
    public GameObject ResetBtn;
    public GameObject ReturnBtn;
    public GameObject TileSetBtn;
    public GameObject CompeleteBtn;
    public GameObject TileCompeleteBtn;

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
    public TotalMap.Rank TotalMapRank;

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

    public List<TileScript> SelectTileScripts;

    //map_wave
    public GameObject WavePopup;

    public ToggleEX isWaveToggle;
    public InputField waveTimeField;
    public InputField waveLimitField;
    public InputField waveMaxField;
    
    public InputField firstMonster;
    public InputField secondMonster;
    public InputField thirdMonster;
    public InputField forthMonster;
    public InputField fiveMonster;

    //map_boss
    public GameObject BossPopup;

    public ToggleEX isBossToggle;
    
    public InputField firstBoss;
    public InputField secondBoss;
    public InputField thirdBoss;
    public InputField forthBoss;
    public InputField fiveBoss;

    //map second page
    public ControllTile secondTileScript;

    public Sprite tile_just_middle;
    public Sprite tile_right_up;
    public Sprite tile_left_up;
    public Sprite tile_right_down;
    public Sprite tile_left_down;
    public Sprite tile_just_up;
    public Sprite tile_just_down;
    public Sprite tile_just_left;
    public Sprite tile_just_right;
    public Sprite tile_just_normal;


    public Sprite tile_right_down_pix;
    public Sprite tile_left_down_pix;
    public Sprite tile_right_up_pix;
    public Sprite tile_left_up_pix;
    public Sprite empty;

    public Dictionary<int ,Sprite> tileSpriteDic;

    public GameObject shapePopup;
    public GameObject heightPopup;
    public GameObject ctrPopup;

    //toggleGroups
    public ToggleGroupEX mapGradeGroup;
    public ToggleGroupEX mapBGGroup;
    public ToggleGroupEX mapTypeGroup;

    //util

    private void Awake()
    {
        IsWarning = true;
        CheckWarning();
        currentIndex = 0;

        if (tileSpriteDic == null)
        {
            tileSpriteDic = new Dictionary<int, Sprite>();
        }
        tileSpriteDic.Clear();
        tileSpriteDic.Add(0, tile_just_normal);
        tileSpriteDic.Add(1, tile_right_up);
        tileSpriteDic.Add(2, tile_left_up);
        tileSpriteDic.Add(3, tile_right_down);
        tileSpriteDic.Add(4, tile_left_down);
        tileSpriteDic.Add(5, tile_just_up);
        tileSpriteDic.Add(6, tile_just_down);
        tileSpriteDic.Add(7, tile_just_left);
        tileSpriteDic.Add(8, tile_just_right);
        tileSpriteDic.Add(9, tile_just_middle);
        tileSpriteDic.Add(10, tile_right_down_pix);
        tileSpriteDic.Add(11, tile_left_down_pix);
        tileSpriteDic.Add(12, tile_right_up_pix);
        tileSpriteDic.Add(13, tile_left_up_pix);
        tileSpriteDic.Add(14, empty);
        if (SelectTileScripts == null)
        {
            SelectTileScripts = new List<TileScript>();
        }
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
                TileCompeleteBtn.SetActive(false);
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
                    TileCompeleteBtn.SetActive(false);
                }
                else
                {
                    MapListBtn.SetActive(false);
                    ResetBtn.SetActive(false);
                    ReturnBtn.SetActive(false);
                    TileSetBtn.SetActive(false);
                    CompeleteBtn.SetActive(true);
                    TileCompeleteBtn.SetActive(false);
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
                TileCompeleteBtn.SetActive(false);
                break;

            case 3:
                PageNameText.text = "맵 : " + gameMapsIndex.ToString() + "_타일";
                firstPage.SetActive(false);
                mapListPage.SetActive(false);
                mapPage.SetActive(false);
                mapSecondPage.SetActive(true);

                MapListBtn.SetActive(false);
                ResetBtn.SetActive(false);
                ReturnBtn.SetActive(false);
                TileSetBtn.SetActive(false);
                CompeleteBtn.SetActive(false);
                TileCompeleteBtn.SetActive(true);
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

        TotalMapRank = (TotalMap.Rank)Enum.ToObject(typeof(TotalMap.Rank), key);
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
                        target.type = GameMap.GameType.Normal;
                        break;
                    case 1:
                        target.type = GameMap.GameType.Hidden;
                        break;
                    case 2:
                        target.type = GameMap.GameType.Final;
                        break;
                }
            }
            else
            {
                target.type = GameMap.GameType.Normal;
            }
        }


        CheckWarning();
    }

    public void GetWaveConfig()
    {
        target.isWave = isWaveToggle.isOn;
        target.waveTime = Int32.Parse(waveTimeField.text);
        target.monsterAcountLimit = Int32.Parse(waveLimitField.text);
        target.maxMonsterAcount = Int32.Parse(waveMaxField.text);
        if (target.monsterIndexList == null)
        {
            target.monsterIndexList = new List<int>();
        }
        else
        {
            target.monsterIndexList.Clear();
        }

        if(firstMonster.text != null && firstMonster.text != "")
        {
            target.monsterIndexList.Add(Int32.Parse(firstMonster.text));
        }
        if (secondMonster.text != null && secondMonster.text != "")
        {
            target.monsterIndexList.Add(Int32.Parse(secondMonster.text));
        }
        if (thirdMonster.text != null && thirdMonster.text != "")
        {
            target.monsterIndexList.Add(Int32.Parse(thirdMonster.text));
        }
        if (forthMonster.text != null && forthMonster.text != "")
        {
            target.monsterIndexList.Add(Int32.Parse(forthMonster.text));
        }
        if (fiveMonster.text != null && fiveMonster.text != "")
        {
            target.monsterIndexList.Add(Int32.Parse(fiveMonster.text));
        }
    }
    public void SetWaveConfig()
    {
        isWaveToggle.isOn = target.isWave;
        waveTimeField.text = target.waveTime.ToString();
        waveLimitField.text = target.monsterAcountLimit.ToString();
        waveMaxField.text = target.monsterAcountLimit.ToString();
        if (target.monsterIndexList == null)
        {
            target.monsterIndexList = new List<int>();
        }
        if(target.monsterIndexList.Count > 0)
        {
            for(int i = 0; i < target.monsterIndexList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        firstMonster.text = target.monsterIndexList[i].ToString();
                        break;
                    case 1:
                        secondMonster.text = target.monsterIndexList[i].ToString();
                        break;
                    case 2:
                        thirdMonster.text = target.monsterIndexList[i].ToString();
                        break;
                    case 3:
                        forthMonster.text = target.monsterIndexList[i].ToString();
                        break;
                    case 4:
                        fiveMonster.text = target.monsterIndexList[i].ToString();
                        break;
                }
            }
        }
    }

    public void GetBossConfig()
    {
        target.isBoss = isBossToggle.isOn;
        if (target.bossIndexList == null)
        {
            target.bossIndexList = new List<int>();
        }
        else
        {
            target.bossIndexList.Clear();
        }

        if (firstBoss.text != null && firstBoss.text != "")
        {
            target.bossIndexList.Add(Int32.Parse(firstBoss.text));
        }
        if (secondBoss.text != null && secondBoss.text != "")
        {
            target.bossIndexList.Add(Int32.Parse(secondBoss.text));
        }
        if (thirdBoss.text != null && thirdBoss.text != "")
        {
            target.bossIndexList.Add(Int32.Parse(thirdBoss.text));
        }
        if (forthBoss.text != null && forthBoss.text != "")
        {
            target.bossIndexList.Add(Int32.Parse(forthBoss.text));
        }
        if (fiveBoss.text != null && fiveBoss.text != "")
        {
            target.bossIndexList.Add(Int32.Parse(fiveBoss.text));
        }
    }
    public void SetBossConfig()
    {
        isBossToggle.isOn = target.isBoss;
        if (target.bossIndexList == null)
        {
            target.bossIndexList = new List<int>();
        }
        if (target.bossIndexList.Count > 0)
        {
            for (int i = 0; i < target.bossIndexList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        firstBoss.text = target.bossIndexList[i].ToString();
                        break;
                    case 1:
                        secondBoss.text = target.bossIndexList[i].ToString();
                        break;
                    case 2:
                        thirdBoss.text = target.bossIndexList[i].ToString();
                        break;
                    case 3:
                        forthBoss.text = target.bossIndexList[i].ToString();
                        break;
                    case 4:
                        fiveBoss.text = target.bossIndexList[i].ToString();
                        break;
                }
            }
        }
    }

    public void OpenWavePopup()
    {
        WavePopup.SetActive(true);
        SetWaveConfig();
    }
    public void OpenBossPopup()
    {
        BossPopup.SetActive(true);
        SetBossConfig();
    }
    public void CloseWavePopup()
    {
        GetWaveConfig();
        WavePopup.SetActive(false);
    }
    public void CloseBossPopup()
    {
        GetBossConfig();
        BossPopup.SetActive(false);
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
        SelectTileScripts.Clear();
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
        SelectTileScripts.Clear();
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
                gameMaps.Add(new GameMap(i, GameMap.TileType.None, false, null, true, null, GameMap.GameType.None, -1, -1));
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
            SelectTileScripts.Clear();
            secondTileScript.SetUpTiles(target, this);
            
            currentIndex = 3;
            DoUpdate();
        }
    }


    public void CompeleteMapMake()
    {
        secondTileScript.CompeleteMapMake();


        ReturnMapList();
    }


    public void CompeleteTotalMap()
    {
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Maps");
        FileInfo[] fis = di.GetFiles();
        
        TotalMap totalMap = new TotalMap((fis.Length / 2).ToString(), MapName, gameMaps, MapLength, TotalMapRank);

        BinarySave<TotalMap>(totalMap, Application.dataPath + "/Maps/" + (fis.Length/2).ToString()+".bin");
    }

    public void testLoadMap()
    {
        if (System.IO.File.Exists(Application.dataPath + "/Maps/0.bin"))
        {
            TotalMap testmap = BinaryLoad<TotalMap>(Application.dataPath + "/Maps/map_test.bin");
            Debug.Log(testmap.gameMapList.Count);
        }
    }




    //mapBtnScript
    public Sprite GetTileSprite(int index)
    {
        return tileSpriteDic[index];
    }


    public void TileBtnsClicked(int index)
    {
        switch (index)
        {
            case 0:
                if(SelectTileScripts.Count > 0)
                shapePopup.SetActive(true);
                break;
            case 1:
                if (SelectTileScripts.Count > 0)
                    heightPopup.SetActive(true);
                break;
            case 2:
                if (SelectTileScripts.Count > 0)
                    ctrPopup.SetActive(true);
                break;
            case 3:
                SelectAllTiles();
                break;
            case 4:
                UnSelectAllTiles();
                break;
        }
    }
    public void CloseTilePopup(int index)
    {
        switch (index)
        {
            case 0:
                shapePopup.SetActive(false);
                break;
            case 1:
                heightPopup.SetActive(false);
                break;
            case 2:
                ctrPopup.SetActive(false);
                break;
        }
    }
    public void SelectAllTiles()
    {
        secondTileScript.SelectAll();
    }
    public void UnSelectAllTiles()
    {
        secondTileScript.UnSelectAll();
    }
    public void ChangeShapeTiles(int index)
    {
        for(int i = 0; i < SelectTileScripts.Count; i++)
        {
            SelectTileScripts[i].ChangeShape(index);
        }
        CloseTilePopup(0);
    }
    public void ChangeHeightTiles(int index)
    {
        for (int i = 0; i < SelectTileScripts.Count; i++)
        {
            SelectTileScripts[i].ChangeHeight(index);
        }
        CloseTilePopup(1);
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





    //final save load
    public void BinarySave<T>(T t, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, t);
        stream.Close();
    }

    public T BinaryLoad<T>(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);
        T t = (T)formatter.Deserialize(stream);
        stream.Close();

        return t;
    }

}
