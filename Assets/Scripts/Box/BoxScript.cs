using System.Collections;
using UnityEngine;


public class BoxScript : MonoBehaviour, IContent {
    public Transform Transform_TargetTile;

    public TileScript TileScript_Current;


    public void InitializeContent(TileScript tileScript) {
        TileScript_Current = tileScript;
        transform.localPosition = Vector3.zero;
        GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)transform.position.y;

        if (TileScript_Current.TileData.TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke)) {
            TileBoxContainerCounter.Instance.BoxOnPlace++;
        }
    }

    void Update() {
        if (Transform_TargetTile != null) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, DataManager.Instance.MovingDelta * Time.deltaTime);

            // GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)transform.position.y;

            if (transform.localPosition == Vector3.zero) {
                Transform_TargetTile = null;
            }
        }
    }

    public bool ChangeTile(Transform Hero, Direction direction) {
        Vector2 tilePosition = (Vector2)transform.position + new Vector2(0, 0.5F) +
            (direction == Direction.Idle_Forward_Left ? new Vector2(-LevelLoader.Instance.PaddingTile.x, -LevelLoader.Instance.PaddingTile.y) :
             direction == Direction.Idle_Forward_Right ? new Vector2(LevelLoader.Instance.PaddingTile.x, -LevelLoader.Instance.PaddingTile.y) :
             direction == Direction.Idle_Back_Left ? new Vector2(-LevelLoader.Instance.PaddingTile.x, LevelLoader.Instance.PaddingTile.y) :
             new Vector2(LevelLoader.Instance.PaddingTile.x, LevelLoader.Instance.PaddingTile.y));

        RaycastHit2D hit = Physics2D.Raycast(tilePosition,
            Vector2.down,
            0.1f);

        if (hit.collider != null && hit.collider.gameObject != null) {
            TileScript nextTileScript = hit.collider.gameObject.GetComponent<TileScript>();
            if (nextTileScript != null) {
                if (nextTileScript.Tile_Serializable.contentID == ContentTile.None) {

                    TileScript_Current.Tile_Serializable.contentID = ContentTile.None;
                    nextTileScript.Tile_Serializable.contentID = ContentTile.Box;
                    nextTileScript.SetContent(transform);

                    StartCoroutine(Delay_MoveChest(hit.collider.gameObject.transform, nextTileScript.transform.position));

                    GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)nextTileScript.transform.position.y;

                    if (TileScript_Current.TileData.TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke) == false
                        && nextTileScript.TileData.TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke)) {
                        TileBoxContainerCounter.Instance.BoxOnPlace++;
                    }
                    else if (TileScript_Current.TileData.TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke)
                       && nextTileScript.TileData.TileTypes.Exists(p => p == TileTypes.MiejsceNaSkrzynke) == false) {
                        TileBoxContainerCounter.Instance.BoxOnPlace--;
                    }

                    TileScript_Current = nextTileScript;

                    return true;
                }
            }
        }

        return false;
    }

    IEnumerator Delay_MoveChest(Transform transform_TargetTile, Vector3 targetPosition) {
        GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)(targetPosition.y);
        yield return new WaitForSeconds(0.15F);
        Transform_TargetTile = transform_TargetTile;
    }
}


public interface IContent {
    public void InitializeContent(TileScript tileScript);
}