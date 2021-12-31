using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public GameObject Prefab_GlobalManagers;

    void Awake() {
        if (FindObjectOfType<GameManager>() == null) {
            Instantiate(Prefab_GlobalManagers);
        }
    }
}
