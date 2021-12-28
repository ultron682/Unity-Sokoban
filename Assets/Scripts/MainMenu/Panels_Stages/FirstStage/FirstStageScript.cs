using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstStageScript : MonoBehaviour {
    public GameObject MainContainer;
    public GameObject Panel_DifficultyLevel;

    public Toggle Toggle_Amateur;
    public Toggle Toggle_Intermediate;
    public Toggle Toggle_Expert;

    private void Start() {
        MainContainer.SetActive(true);
        Panel_DifficultyLevel.SetActive(false);
    }

    public void OnClick_Play() {
        MainContainer.SetActive(false);
        Panel_DifficultyLevel.SetActive(true);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(false);
    }

    public void OnClick_Exit() {
        Application.Quit();
    }

    /// DifficultyPanel
    public void OnClick_CloseDifficulty() {
        MainContainer.SetActive(true);
        Panel_DifficultyLevel.SetActive(false);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(true);
    }

    public void OnClick_PlayWithSelectedDifficultyLevel() {
        DifficultyLevel difficultyLevel = Toggle_Amateur.isOn ? DifficultyLevel.Amateur : Toggle_Intermediate.isOn ? DifficultyLevel.Intermediate : DifficultyLevel.Expert;

        List<LevelData_Serializable> levelsData_Serializable = DataManager.Instance.AllLevelsData_Serializable.FindAll(p => p.difficultyLevel == difficultyLevel);

        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = levelsData_Serializable[Random.Range(0, levelsData_Serializable.Count)];

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }
}
