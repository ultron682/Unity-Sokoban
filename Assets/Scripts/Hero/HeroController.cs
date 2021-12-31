using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroController : MonoBehaviour {
    public Animator Animator_Hero;
    public SpriteRenderer SpriteRenderer_Hero;
    public AudioClip AudioClip_Breathing;
    public AudioClip AudioClip_Moving;
    public AudioClip AudioClip_Walking;

    private Vector2 paddingTile;
    Transform Transform_TargetTile;

    private TileScript TileScript_Current;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    GameObject touchedControl = null;


    HeroStateAnimation HeroStateAnimation_Current {
        get {
            return _heroStateAnimation_Current;
        }

        set {
            _heroStateAnimation_Current = value;
            Animator_Hero.SetInteger("index", (int)_heroStateAnimation_Current);
        }
    }

    Direction Hero_Direction_Current {
        get {
            return _heroIdleAnimation;
        }

        set {
            _heroIdleAnimation = value;
            Animator_Hero.SetInteger("index_Direction", (int)_heroIdleAnimation);
        }
    }

    private Direction _heroIdleAnimation;
    private HeroStateAnimation _heroStateAnimation_Current;


    void Start() {
        if (LevelLoader.Instance != null) {
            m_Raycaster = LevelLoaderCanvasManager.Instance.GetComponent<GraphicRaycaster>();
            m_EventSystem = LevelLoaderCanvasManager.Instance.GameObject_EventSystem.GetComponent<EventSystem>();
            paddingTile = LevelLoader.Instance.PaddingTile;
            LevelLoaderManager.Instance.OnEndLoadingLevel += Instance_OnEndLoadingLevel;
        }
    }

    void Update() {
        bool sendCommand = false;

        if (Input.touchCount == 1 && LevelDataManager.Instance.GameIsEnd == false && Transform_TargetTile == null) {
            bool succes = false;

            Touch finger = Input.GetTouch(0);
            List<RaycastResult> touchedElements = GetUiElementFromTouchPosition(finger.position);

            KeyCode pressedKeyCode = KeyCode.None;

            foreach (RaycastResult result in touchedElements) {
                if (result.gameObject.CompareTag("TriggerControl")) {
                    touchedControl = result.gameObject.transform.parent.gameObject;
                    print(touchedControl.name);
                    if (touchedControl.name == "a")
                        pressedKeyCode = KeyCode.A;
                    else if (touchedControl.name == "w")
                        pressedKeyCode = KeyCode.W;
                    else if (touchedControl.name == "s")
                        pressedKeyCode = KeyCode.S;
                    else if (touchedControl.name == "d")
                        pressedKeyCode = KeyCode.D;
                }
            }


            if (pressedKeyCode == KeyCode.A || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                MoveTo(Direction.Idle_Back_Left, ref succes);
                sendCommand = true;
            }
            else if (pressedKeyCode == KeyCode.W || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                MoveTo(Direction.Idle_Back_Right, ref succes);
                sendCommand = true;
            }
            else if (pressedKeyCode == KeyCode.S || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                MoveTo(Direction.Idle_Forward_Left, ref succes);
                sendCommand = true;
            }
            else if (pressedKeyCode == KeyCode.D || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
                MoveTo(Direction.Idle_Forward_Right, ref succes);
                sendCommand = true;
            }

            if (/*IsSomeoneKeyDown() == true && */succes == false) {
                HeroStateAnimation_Current = HeroStateAnimation.Idle;
                Animator_Hero.SetBool("isPushing", false);
            }

        }


        if (Transform_TargetTile != null) {
            transform.position = Vector3.MoveTowards(transform.position, Transform_TargetTile.position, DataManager.Instance.MovingDelta * Time.deltaTime);

            GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)transform.position.y;

            if (Transform_TargetTile.position == transform.position) {
                Transform_TargetTile = null;
            }
        }
        else {
            if (Input.touchCount == 0 && sendCommand == false) {
                HeroStateAnimation_Current = HeroStateAnimation.Idle;
                Animator_Hero.SetBool("isPushing", false);
            }
        }

    }

    private List<RaycastResult> GetUiElementFromTouchPosition(Vector2 fingerPosition) {
        List<RaycastResult> results = new List<RaycastResult>();
        m_PointerEventData = new PointerEventData(m_EventSystem) {
            position = fingerPosition
        };
        m_Raycaster.Raycast(m_PointerEventData, results);

        return results;
    }

    //bool IsSomeoneKeyDown() {
    //    return (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    //             || (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    //             || (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    //             || (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
    //}

    private void Instance_OnEndLoadingLevel(object sender, EventArgs e) {
        Tile_Serializable tile_SerializableWithHero = LevelDataManager.Instance.levelData_Serializable.gridData.Find(p => p.contentID == ContentTile.Hero);

        if (tile_SerializableWithHero == null)
            tile_SerializableWithHero = LevelDataManager.Instance.levelData_Serializable.gridData.Find(p => DataManager.Instance.TilesData[p.tileID].TileTypes.Exists(p => p == TileTypes.FootPath));

        transform.position = tile_SerializableWithHero.positionInWorld;
        TileScript_Current = tile_SerializableWithHero.TileScript;
        TileScript_Current.Tile_Serializable.contentID = ContentTile.Hero;

        GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)transform.position.y;
    }

    private void MoveTo(Direction direction, ref bool succes) {
        (Transform nextTileTransform, TileScript nextTileScript) = GetNextTile(direction);

        if (nextTileTransform != null) {
            if (DataManager.Instance.TilesData[nextTileScript.Tile_Serializable.tileID].TileTypes.Exists(p => p != TileTypes.Wall)) {
                if (nextTileScript.Tile_Serializable.contentID == 0) { // box
                    Transform transformContent = nextTileScript.GetContent();
                    if (transformContent != null) {
                        BoxScript boxScript = transformContent.GetComponent<BoxScript>();

                        if (boxScript.ChangeTile(transform, direction) == false) {
                            Hero_Direction_Current = direction;
                            succes = false;
                            return;
                        }
                        else {
                            succes = true;
                            Animator_Hero.SetBool("isPushing", true);
                            GetComponent<AudioSource>().PlayOneShot(AudioClip_Moving);
                        }
                    }
                }
                else {
                    Animator_Hero.SetBool("isPushing", false);
                }

                succes = true;
                GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)transform.position.y;
                TileScript_Current.Tile_Serializable.contentID = ContentTile.None;
                nextTileScript.Tile_Serializable.contentID = ContentTile.Hero;

                TileScript_Current = nextTileScript;
                Hero_Direction_Current = direction;
                Transform_TargetTile = nextTileTransform;
                HeroStateAnimation_Current = HeroStateAnimation.Walk;
                GetComponent<AudioSource>().PlayOneShot(AudioClip_Walking);
                LevelDataManager.Instance.MovesCount++;
            }
            else {
                Hero_Direction_Current = direction;
                HeroStateAnimation_Current = HeroStateAnimation.Idle;
            }
        }
        else {
            Hero_Direction_Current = direction;
            HeroStateAnimation_Current = HeroStateAnimation.Idle;
        }
    }

    private (Transform targetTile, TileScript tileScript) GetNextTile(Direction direction) {
        Vector2 tilePosition = (Vector2)transform.position + new Vector2(0, 0.5F) +
            (direction == Direction.Idle_Forward_Left ? new Vector2(-paddingTile.x, -paddingTile.y) :
           direction == Direction.Idle_Forward_Right ? new Vector2(paddingTile.x, -paddingTile.y) :
           direction == Direction.Idle_Back_Left ? new Vector2(-paddingTile.x, paddingTile.y) :
           new Vector2(paddingTile.x, paddingTile.y));

        RaycastHit2D hit = Physics2D.Raycast(tilePosition,
            Vector2.down,
            0.1f);

        Debug.DrawRay(tilePosition, new Vector2(0.1F, 0.1F), Color.red, 1);

        if (hit.collider != null) {
            TileScript tileScript = hit.collider.gameObject.GetComponent<TileScript>();
            return (hit.collider.transform, tileScript);
        }
        else {
            return (null, null);
        }
    }

    enum HeroStateAnimation {
        Idle,
        Walk
    }
}

public enum Direction {
    Idle_Forward_Left,
    Idle_Forward_Right,
    Idle_Back_Left,
    Idle_Back_Right,
}
