using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;

    public string PlayerNick = string.Empty;

    void Awake() {
        Instance = this;
    }

}
