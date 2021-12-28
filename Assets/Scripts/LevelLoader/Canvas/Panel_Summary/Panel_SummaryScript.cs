using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Panel_SummaryScript : MonoBehaviour {
    public Button Button_Continue;
    public Text Text_Title;
    public Text Text_Score;


    void Start() {
        if (GameManager.Instance.CurrentStage != Stages.Stage2 || LevelDataManager.Instance.levelData_Serializable.IsCustomLevel == true || LevelDataManager.Instance.levelData_Serializable.id >= 20) {
            Button_Continue.gameObject.SetActive(false);
            Text_Title.gameObject.SetActive(false);
        }

        LevelDataManager.Instance.levelData_Serializable.IsCompleted = true;

        if (LevelDataManager.Instance.levelData_Serializable.Moves < LevelDataManager.Instance.levelData_Serializable.minMovesToComplete)
            LevelDataManager.Instance.levelData_Serializable.minMovesToComplete = LevelDataManager.Instance.levelData_Serializable.Moves;

        LevelDataManager.Instance.levelData_Serializable.Score = Mathf.Clamp(100 - (LevelDataManager.Instance.levelData_Serializable.Moves - LevelDataManager.Instance.levelData_Serializable.minMovesToComplete), 0, 100);
        Text_Score.text = $"Zdobyto {LevelDataManager.Instance.levelData_Serializable.Score} punktów";

        if (GameManager.Instance.CurrentStage == Stages.Stage2) {
            if (DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Exists(p => p.id == LevelDataManager.Instance.levelData_Serializable.id)) {
                LevelData_Serializable levelData_Serializable = DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Find(p => p.id == LevelDataManager.Instance.levelData_Serializable.id);
                DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Remove(levelData_Serializable);
            }

            DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Add(LevelDataManager.Instance.levelData_Serializable);

            string levelSerializable = JsonUtility.ToJson(DataManager.Instance.Stage2_AllSavedLevelsData_Serializable);
            print(levelSerializable);
            PlayerPrefs.SetString("stage2_savedLevels", levelSerializable);
            PlayerPrefs.Save();
        }
    }

    public void OnClick_ReturnToMainMenu() {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    public void OnClick_NextLevel() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        if (levelDataHolder != null) {
            levelDataHolder.LevelData_Serializable = DataManager.Instance.AllLevelsData_Serializable[LevelDataManager.Instance.levelData_Serializable.id];
        }

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }
}
