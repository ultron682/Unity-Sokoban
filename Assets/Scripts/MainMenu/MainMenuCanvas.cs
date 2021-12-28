using UnityEngine;


public class MainMenuCanvas : MonoBehaviour {
    public static MainMenuCanvas Instance;

    public GameObject GameObject_Title;


    void Awake() {
        Instance = this;
    }
}
