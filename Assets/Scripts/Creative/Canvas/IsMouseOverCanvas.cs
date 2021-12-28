using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsMouseOverCanvas : MonoBehaviour {
    public static IsMouseOverCanvas Instance;
    public bool MouseIsOverUI;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        if (IsPointerOverUI())
            MouseIsOverUI = true;
        else
            MouseIsOverUI = false;
    }

    bool IsPointerOverUI() {
        PointerEventData eventdataCurrentPosition = new PointerEventData(EventSystem.current);
        eventdataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventdataCurrentPosition, results);
        return results.Count > 0;
    }
}
