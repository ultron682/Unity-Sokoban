using UnityEngine;


public class LevelDataHolder : MonoBehaviour {
    public LevelData_Serializable LevelData_Serializable;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public LevelData_Serializable Clone() {
        return LevelData_Serializable.DeepCopy();
    }
}
