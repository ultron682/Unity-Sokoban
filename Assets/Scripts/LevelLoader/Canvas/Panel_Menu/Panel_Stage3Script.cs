using UnityEngine;
using UnityEngine.SceneManagement;


public class Panel_Stage3Script : MonoBehaviour {

    public void OnClick_SaveProgress() {
        if (DataManager.Instance.Stage3_AllSavedLevelsData_Serializable.savedLevels_Serializables.Exists(p => p.id == LevelDataManager.Instance.levelData_Serializable.id)) {
            LevelData_Serializable levelData_Serializable = DataManager.Instance.Stage3_AllSavedLevelsData_Serializable.savedLevels_Serializables.Find(p => p.id == LevelDataManager.Instance.levelData_Serializable.id);
            DataManager.Instance.Stage3_AllSavedLevelsData_Serializable.savedLevels_Serializables.Remove(levelData_Serializable);
        }

        DataManager.Instance.Stage3_AllSavedLevelsData_Serializable.savedLevels_Serializables.Add(LevelDataManager.Instance.levelData_Serializable);

        string levelSerializable = JsonUtility.ToJson(DataManager.Instance.Stage3_AllSavedLevelsData_Serializable);
        PlayerPrefs.SetString("stage3_savedLevels", levelSerializable);
        PlayerPrefs.Save();

        SceneManager.LoadScene((int)Scenes.MainMenu);
    }
}
