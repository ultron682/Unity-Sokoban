using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Creative {
    public class Panel_SaveScript : MonoBehaviour {
        public InputField InputField_LevelName;


        public void OnClick_SaveToFile() {
            int i = 0;
            string levelName = InputField_LevelName.text + " ({0})";
            do {
                LevelData_Serializable existLevelData_Serializable = null;

                if (i == 0)
                    existLevelData_Serializable = CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables.Find(p => p.CustomLevel_Name == InputField_LevelName.text);
                else {
                    existLevelData_Serializable = CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables.Find(p => p.CustomLevel_Name == string.Format(levelName, i));
                }

                if (existLevelData_Serializable != null) {
                    i++;
                }
                else { // jesli nie istnieje
                    break;
                }

            } while (true);


            if (i == 0)
                CreativeDataManager.Instance.LevelData_Serializable.CustomLevel_Name = InputField_LevelName.text;
            else {
                levelName = string.Format(levelName, i);
                CreativeDataManager.Instance.LevelData_Serializable.CustomLevel_Name = levelName;
            }

            CreativeDataManager.Instance.LevelData_Serializable.id = Random.Range(9999, 1000000);

            string levelSerialized = JsonUtility.ToJson(CreativeDataManager.Instance.LevelData_Serializable);
            CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables.Add(CreativeDataManager.Instance.LevelData_Serializable);
            CreativeLevelsManager.Instance.SaveState();
            SceneManager.LoadScene((int)Scenes.MainMenu);
        }
    }
}