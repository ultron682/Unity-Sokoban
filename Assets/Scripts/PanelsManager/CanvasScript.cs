using UnityEngine;


public class CanvasScript : MonoBehaviour {
    public static CanvasScript Instance;


    void Awake() {
        Instance = this;
    }
}
