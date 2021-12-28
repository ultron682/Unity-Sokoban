using System;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour {
    public static DataManager Instance;

    public GameObject Prefab_Tile;
    public List<TileData> TilesData;
    public List<GameObject> ContentTileData;

    [NonSerialized]
    public float MovingDelta = 6F;
    [NonSerialized]
    public List<LevelData_Serializable> AllLevelsData_Serializable = new List<LevelData_Serializable>();

    public Stage2_SavedLevels Stage2_AllSavedLevelsData_Serializable = new Stage2_SavedLevels();

    public Stage3_SavedLevels Stage3_AllSavedLevelsData_Serializable = new Stage3_SavedLevels();


    private void Awake() {
        Instance = this;
        for (int i = 1; i <= 20; i++) {
            AllLevelsData_Serializable.Add(JsonUtility.FromJson<LevelData_Serializable>(Resources.Load<TextAsset>($"level{i}").text));
        }
        //PlayerPrefs.DeleteKey("stage2_savedLevels");
        if (PlayerPrefs.HasKey("stage2_savedLevels"))
            Stage2_AllSavedLevelsData_Serializable = JsonUtility.FromJson<Stage2_SavedLevels>(PlayerPrefs.GetString("stage2_savedLevels", string.Empty));

        if (PlayerPrefs.HasKey("stage3_savedLevels"))
            Stage3_AllSavedLevelsData_Serializable = JsonUtility.FromJson<Stage3_SavedLevels>(PlayerPrefs.GetString("stage3_savedLevels", string.Empty));
    }
}


[Serializable]
public class Stage2_SavedLevels {
    public List<LevelData_Serializable> savedLevels_Serializables;
}


[Serializable]
public class Stage3_SavedLevels {
    public List<LevelData_Serializable> savedLevels_Serializables;
}

[Serializable]
public class TileData {
    public string TileName;
    public List<TileTypes> TileTypes;
    public Sprite Tile_Sprite_Bg;
}

public enum DifficultyLevel {
    Amateur,
    Intermediate, // Średniozaawansowany
    Expert
}

public enum TileTypes {
    FootPath, // Tylko po tym można chodzić
    Wall, // No ściana ¯\_(ツ)_/¯
    MiejsceNaSkrzynke // Typ Tile'sa w którym się umieszcza skrzynke (☞ﾟヮﾟ)☞
}

public enum ContentTile {
    None = -1,
    Box,
    Hero
}

public enum Scenes {
    MainMenu,
    LevelLoader,
    Creative
}
