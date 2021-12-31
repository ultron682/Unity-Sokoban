using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Panel_CommunityScript : MonoBehaviour {
    public static Panel_CommunityScript Instance;

    public GameObject Prefab_CustomLevel;
    public RectTransform RectTransform_CustomLevelsContent;
    public Button Button_Play;
    public Button Button_Remove;

    private List<CustomLevelScript> customLevelScripts = new List<CustomLevelScript>();
    private GameObject selectedGameObjectCustomLevel = null;
    private LevelData_Serializable selectedLevelData_SerializableToLoad;


    private void Awake() {
        Instance = this;
    }

    void Start() {
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

        Button_Play.interactable = true;
        Button_Remove.interactable = true;

        foreach (var element in customLevelScripts) {
            if (element != customLevelScript) {
                element.ChangeSelectionState(false);
            }
        }

        customLevelScript.ChangeSelectionState(true);
    }

    public void OnClick_Play() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = selectedLevelData_SerializableToLoad;

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }

    public void OnClick_Remove() {
        Button_Remove.interactable = false;
        Button_Play.interactable = false;

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
}
