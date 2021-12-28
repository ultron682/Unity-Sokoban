using UnityEngine;


namespace Creative {
    public class CreativeGridManager : MonoBehaviour {
        public static CreativeGridManager Instance;

        public GameObject Prefab_TileCreative;
        public Transform Transform_TileContainer;

        public Vector2 PaddingTile = new Vector2(2.131F, 1.028F);


        void Awake() {
            Instance = this;
        }

        public void GenerateGrid(int grid_X, int grid_Y) {
            int ite_id = 0;

            Vector2 calculatedPosition = Vector2.zero;

            for (int ite_x = 0; ite_x < grid_X; ite_x++) {
                int ite_y = 0;
                for (ite_y = 0; ite_y < grid_Y; ite_y++) {
                    Tile_Serializable tile_Serializable = new Tile_Serializable(ite_id);
                    if (tile_Serializable != null) {

                        TileData tileData = DataManager.Instance.TilesData[tile_Serializable.tileID];

                        GameObject tile = Instantiate(Prefab_TileCreative, Transform_TileContainer);
                        tile.name = ite_x + " : " + ite_y + " ; " + tile_Serializable.id.ToString();
                        tile.transform.position = calculatedPosition;
                        TileScript tileScript = tile.GetComponent<TileScript>();
                        tile_Serializable.TileScript = tileScript;
                        tileScript.Initialize(tile_Serializable, tileData);

                        CreativeDataManager.Instance.LevelData_Serializable.gridData.Add(tile_Serializable);

                        ChangeContentOnTile(tileScript);

                    }

                    calculatedPosition += new Vector2(-PaddingTile.x, PaddingTile.y);
                    ite_id++;
                }

                calculatedPosition = new Vector2(PaddingTile.x * (ite_x + 1), PaddingTile.y * (ite_x + 1));
                calculatedPosition -= new Vector2(0.17F * (ite_x + 1), 0.089F * (ite_x + 1));
            }

            Tile_Serializable tile_SerializableWithHero = CreativeDataManager.Instance.LevelData_Serializable.gridData.Find(p => p.contentID == ContentTile.None);
            CameraController.Instance.lastTileScriptWithHero = tile_SerializableWithHero.TileScript;
            CameraController.Instance.Transform_Hero.GetComponent<HeroCreativeScript>().ChangeContainer(tile_SerializableWithHero.TileScript);
            tile_SerializableWithHero.TileScript.Tile_Serializable.contentID = ContentTile.Hero;
            Camera.main.transform.position = new Vector3(CameraController.Instance.Transform_Hero.transform.position.x, CameraController.Instance.Transform_Hero.transform.position.y, -10F);
        }

        public void ChangeContentOnTile(TileScript tileScript) {
            if (tileScript.Tile_Serializable.contentID != ContentTile.None && tileScript.Tile_Serializable.contentID != ContentTile.Hero) {
                GameObject content = Instantiate(DataManager.Instance.ContentTileData[(int)tileScript.Tile_Serializable.contentID]);

                tileScript.SetContent(content.transform);
                Panel_ToolsScript.Instance.ExistBoxsInCreative++;
                content.GetComponent<IContent>().InitializeContent(tileScript);
            }
            else if (tileScript.Tile_Serializable.contentID == ContentTile.None) {
                tileScript.RemoveContent();
            }
        }
    }
}