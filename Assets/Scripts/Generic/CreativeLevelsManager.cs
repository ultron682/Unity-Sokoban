using System;
using System.Collections.Generic;
using UnityEngine;


public class CreativeLevelsManager : MonoBehaviour {
    public static CreativeLevelsManager Instance;

    public CustomLevels_Serializable customLevels_Serializable;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        string customLevelsData = PlayerPrefs.GetString("CustomLevels", string.Empty);
        if (customLevelsData != string.Empty)
            customLevels_Serializable = JsonUtility.FromJson<CustomLevels_Serializable>(customLevelsData);
    }

    public void SaveState() {
        PlayerPrefs.SetString("CustomLevels", JsonUtility.ToJson(customLevels_Serializable));
        PlayerPrefs.Save();
    }
}

[Serializable]
public class CustomLevels_Serializable {
    public List<LevelData_Serializable> levelData_Serializables;
}