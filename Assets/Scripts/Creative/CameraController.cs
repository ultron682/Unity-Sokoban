using UnityEngine;


namespace Creative {
    public class CameraController : MonoBehaviour {
        public static CameraController Instance;
        public IsMouseOverCanvas IMOUI;
        public Transform Transform_Hero;

        Vector2 mouseClickPos;
        Vector2 mouseCurrentPos;
        bool panning = false;
        Vector2 _lastDistance = Vector2.zero;

        Vector2 startClickPosition = Vector2.zero;
        bool wasMoved;
        Color Color_DisabledPlatform = new Color(0.55F, 0.55F, 0.55F);
        public TileScript lastTileScriptWithHero;


        private void Awake() {
            Instance = this;
        }

        private void Update() {
            if (IMOUI.MouseIsOverUI)
                return;

            // When LMB clicked get mouse click position and set panning to true
            if (Input.GetKeyDown(KeyCode.Mouse0) && !panning) {
                mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                panning = true;
                startClickPosition = Input.mousePosition;
            }

            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (panning && Input.GetKeyUp(KeyCode.Mouse0) == false) {

                _lastDistance = mouseCurrentPos - mouseClickPos;
                transform.position += new Vector3(-_lastDistance.x, -_lastDistance.y, 0);
            }

            // If LMB is released, stop moving the camera
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                panning = false;

                Vector2 distance = (Vector2)Input.mousePosition - startClickPosition;
                if (Mathf.Abs(distance.x) < 10 && Mathf.Abs(distance.y) < 10) {
                    OnClick();
                }
            }
        }

        private void OnClick() {
            if (IMOUI.MouseIsOverUI)
                return;

            RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero);
            if (raycastHit.collider != null) {
                if (raycastHit.collider.CompareTag("PlatformTile")) {
                    GameObject lastGameObject = raycastHit.collider.gameObject;
                    TileScript tileScript = lastGameObject.GetComponent<TileScript>();

                    AudioManager.Instance.PlayClickSoundSecond();

                    if (Panel_ToolsScript.Instance.Toggle_TileType.isOn) {
                        tileScript.Tile_Serializable.tileID = tileScript.Tile_Serializable.tileID == 1 ? 3 : 1; // zmiana typu tile
                        tileScript.Reinitialize();
                    }
                    else if (Panel_ToolsScript.Instance.Toggle_RemoveTile.isOn) {
                        if (tileScript.Tile_Serializable.contentID == ContentTile.None) {
                            if (CreativeDataManager.Instance.LevelData_Serializable.gridData.Contains(tileScript.Tile_Serializable)
                                && CreativeDataManager.Instance.LevelData_Serializable.gridData.Count > 2) {
                                CreativeDataManager.Instance.LevelData_Serializable.gridData.Remove(tileScript.Tile_Serializable);
                                tileScript.Tile_Serializable.contentID = ContentTile.None;

                                lastGameObject.GetComponent<SpriteRenderer>().color = Color_DisabledPlatform;
                            }
                            else {
                                if (CreativeDataManager.Instance.LevelData_Serializable.gridData.Contains(tileScript.Tile_Serializable) == false) {
                                    CreativeDataManager.Instance.LevelData_Serializable.gridData.Add(tileScript.Tile_Serializable);

                                    lastGameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                }
                            }
                        }
                    }
                    else if (Panel_ToolsScript.Instance.Toggle_Box.isOn) {
                        if (CreativeDataManager.Instance.LevelData_Serializable.gridData.Contains(tileScript.Tile_Serializable)
                            && tileScript.Tile_Serializable.contentID != ContentTile.Hero) {
                            tileScript.Tile_Serializable.contentID = tileScript.Tile_Serializable.contentID == ContentTile.None ? ContentTile.Box : ContentTile.None;
                            CreativeGridManager.Instance.ChangeContentOnTile(tileScript);
                        }
                    }
                    else if (Panel_ToolsScript.Instance.Toggle_HeroSet.isOn) {
                        if (CreativeDataManager.Instance.LevelData_Serializable.gridData.Contains(tileScript.Tile_Serializable)
                            && tileScript.Tile_Serializable.contentID == ContentTile.None) {
                            if (lastTileScriptWithHero != null)
                                lastTileScriptWithHero.Tile_Serializable.contentID = ContentTile.None;
                            tileScript.Tile_Serializable.contentID = ContentTile.Hero;


                            Transform_Hero.GetComponent<HeroCreativeScript>().ChangeContainer(tileScript);

                            lastTileScriptWithHero = tileScript;
                        }
                    }
                }
            }
        }
    }
}