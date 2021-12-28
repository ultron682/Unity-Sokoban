using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelLoader : MonoBehaviour {
    public static LevelLoader Instance;

    public Transform Transform_TileContainer;


    private void Awake() {
        Instance = this;
    }

    public void LoadLevel(LevelData_Serializable levelData) {
        GenerateGrid(levelData.grid_X, levelData.grid_Y, levelData.gridData);
        TileBoxContainerCounter.Instance.MaxBoxPlaces = levelData.gridData.FindAll(p => DataManager.Instance.TilesData[p.tileID].TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke)).Count;
    }

    public Vector2 PaddingTile = new Vector2(2.028F, 1.086F); // editor property

    private void GenerateGrid(int grid_X, int grid_Y, List<Tile_Serializable> gridData) {
        int ite_id = 0;

        Vector2 calculatedPosition = Vector2.zero;

        for (int ite_x = 0; ite_x < grid_X; ite_x++) {
            int ite_y = 0;
            for (ite_y = 0; ite_y < grid_Y; ite_y++) {
                Tile_Serializable tile_Serializable = gridData.Find(p => p.id == ite_id);
                if (tile_Serializable != null) {

                    TileData tileData = DataManager.Instance.TilesData[tile_Serializable.tileID];

                    GameObject tile = Instantiate(DataManager.Instance.Prefab_Tile, Transform_TileContainer);
                    tile.name = ite_x + " : " + ite_y + " ; " + tile_Serializable.id.ToString();
                    tile.transform.position = calculatedPosition;
                    TileScript tileScript = tile.GetComponent<TileScript>();
                    tile_Serializable.TileScript = tileScript;
                    tileScript.Initialize(tile_Serializable, tileData);

                    if (tile_Serializable.contentID != ContentTile.None && tile_Serializable.contentID != ContentTile.Hero) {
                        GameObject content = Instantiate(DataManager.Instance.ContentTileData[(int)tile_Serializable.contentID]);

                        tileScript.SetContent(content.transform);

                        content.GetComponent<IContent>().InitializeContent(tileScript);
                    }

                }

                calculatedPosition += new Vector2(-PaddingTile.x, PaddingTile.y);
                ite_id++;
            }

            calculatedPosition = new Vector2(PaddingTile.x * (ite_x + 1), PaddingTile.y * (ite_x + 1));
            calculatedPosition -= new Vector2(0.17F * (ite_x + 1), 0.089F * (ite_x + 1));
        }
    }
}

[Serializable]
public class LevelData_Serializable {
    public int id;
    public DifficultyLevel difficultyLevel;
    public List<Tile_Serializable> gridData;
    public int grid_X; // max 30
    public int grid_Y; // max 20
    public int maxTime = 100; // time in seconds // To remove?
    public int minMovesToComplete = 40;
    /// Custom Level
    public bool IsCustomLevel;
    public string CustomLevel_Name = string.Empty;
    /// SavedData
    public bool IsCompleted; // czy poziom został ukończony
    public int Score;
    public int Moves;
    public float Time;

    public LevelData_Serializable DeepCopy() {
        var serialized = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<LevelData_Serializable>(serialized);
    }
}

[Serializable]
public class Tile_Serializable {
    public int id;
    public int tileID = 1;
    public ContentTile contentID = ContentTile.None;

    [NonSerialized]
    public Vector3 positionInWorld;
    [NonSerialized]
    public TileScript TileScript;

    public Tile_Serializable() {

    }

    public Tile_Serializable(int id) {
        this.id = id;
    }
}