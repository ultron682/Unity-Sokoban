using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ThirdStageScript : MonoBehaviour {
    public static ThirdStageScript Instance;

    public GameObject MainContainer;
    public GameObject Panel_CustomLevels;
    public GameObject Prefab_CustomLevel;
    public RectTransform RectTransform_CustomLevelsContent;
    public Button Button_Continue;
    public Button Button_NewGame;
    public Button Button_Remove;

    private List<CustomLevelScript> customLevelScripts = new List<CustomLevelScript>();
    private GameObject selectedGameObjectCustomLevel = null;
    private LevelData_Serializable selectedLevelData_SerializableToLoad;
    private LevelData_Serializable savedSelectedLevelData_SerializableToLoad;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        Panel_CustomLevels.SetActive(false);
        MainContainer.SetActive(true);
        LoadLevels();
    }

    void LoadLevels() {
        for (int i = 0; i < CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables.Count; i++) {
            LevelData_Serializable levelData_Serializable = CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables[i];
            GameObject listElement = Instantiate(Prefab_CustomLevel, RectTransform_CustomLevelsContent);

            CustomLevelScript userLevelScriptInstance = listElement.GetComponent<CustomLevelScript>();
            userLevelScriptInstance.levelData_Serializable = levelData_Serializable;
            userLevelScriptInstance.savedLevelData_Serializable = DataManager.Instance.Stage3_AllSavedLevelsData_Serializable.savedLevels_Serializables.Find(p => p.id == levelData_Serializable.id);

            Text[] texts = listElement.GetComponentsInChildren<Text>();
            texts[0].text = levelData_Serializable.CustomLevel_Name;

            customLevelScripts.Add(userLevelScriptInstance);
        }
    }

    public void SelectCustomLevelToLoad(CustomLevelScript customLevelScript, LevelData_Serializable selectedLevelData_SerializableToLoad, LevelData_Serializable savedSelectedLevelData_SerializableToLoad) {
        selectedGameObjectCustomLevel = customLevelScript.gameObject;
        this.selectedLevelData_SerializableToLoad = selectedLevelData_SerializableToLoad;
        this.savedSelectedLevelData_SerializableToLoad = savedSelectedLevelData_SerializableToLoad;

        Button_NewGame.interactable = true;
        Button_Continue.interactable = (savedSelectedLevelData_SerializableToLoad != null);
        Button_Remove.interactable = true;

        foreach (var element in customLevelScripts) {
            if (element != customLevelScript) {
                element.ChangeSelectionState(false);
            }
        }

        customLevelScript.ChangeSelectionState(true);
    }

    public void OnClick_Continue() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = savedSelectedLevelData_SerializableToLoad;

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }

    public void OnClick_FromNew() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = selectedLevelData_SerializableToLoad;

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }

    public void OnClick_Remove() {
        Button_Remove.interactable = false;
        Button_NewGame.interactable = false;

        if (selectedGameObjectCustomLevel != null) {
            CreativeLevelsManager.Instance.customLevels_Serializable.levelData_Serializables.Remove(selectedLevelData_SerializableToLoad);
            CreativeLevelsManager.Instance.SaveState();
            CustomLevelScript customLevelScript = selectedGameObjectCustomLevel.GetComponent<CustomLevelScript>();
            customLevelScripts.Remove(customLevelScript);
            Destroy(selectedGameObjectCustomLevel);
        }
    }

    public void OnClick_Exit() {
        Application.Quit();
    }

    public void OnClick_Create() {
        SceneManager.LoadScene((int)Scenes.Creative);
    }

    public void OnClick_NewGame() {
        MainContainer.SetActive(false);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(false);
        Panel_CustomLevels.SetActive(true);
    }

    public void OnClick_Back() {
        MainContainer.SetActive(true);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(true);
        Panel_CustomLevels.SetActive(false);
    }
}
