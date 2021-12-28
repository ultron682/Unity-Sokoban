using UnityEngine;


public class TileScript : MonoBehaviour {
    public Tile_Serializable Tile_Serializable;
    public TileData TileData;

    public Transform Transform_ContentContainer;


    public void Initialize(Tile_Serializable tile_Serializable, TileData tileData) {
        this.Tile_Serializable = tile_Serializable;
        this.TileData = tileData;
        GetComponent<SpriteRenderer>().sprite = DataManager.Instance.TilesData[Tile_Serializable.tileID].Tile_Sprite_Bg;
        GetComponent<SpriteRenderer>().sortingOrder = -tile_Serializable.id;
        Tile_Serializable.positionInWorld = transform.position;
        Tile_Serializable.TileScript = this;
    }

    public void SetContent(Transform transformContent) {
        transformContent.SetParent(Transform_ContentContainer);
    }

    public void RemoveContent() {
        if (Transform_ContentContainer != null)
            if (Transform_ContentContainer.childCount > 0) {
                if (GameManager.Instance.CurrentStage == Stages.Stage3) {
                    Creative.Panel_ToolsScript.Instance.ExistBoxsInCreative--;
                }

                Destroy(Transform_ContentContainer.GetChild(0).gameObject);
            }
    }

    public Transform GetContent() {
        if (Transform_ContentContainer.childCount > 0)
            return Transform_ContentContainer.GetChild(0);
        else
            return null;
    }

    /// Creative
    public void Reinitialize() {
        GetComponent<SpriteRenderer>().sprite = DataManager.Instance.TilesData[Tile_Serializable.tileID].Tile_Sprite_Bg;
        //GetComponent<SpriteRenderer>().sortingOrder = -Tile_Serializable.id;
        //Tile_Serializable.positionInWorld = transform.position;
    }
}