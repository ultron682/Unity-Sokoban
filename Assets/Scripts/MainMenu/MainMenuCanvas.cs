using UnityEngine;


public class MainMenuCanvas : MonoBehaviour {
    public static MainMenuCanvas Instance;

    public GameObject GameObject_Title;
    public GameObject GameObject_TogglesStages;


    void Awake() {
        Instance = this;
    }
}
