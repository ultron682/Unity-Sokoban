using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Panel_CampaignScript : MonoBehaviour {
    public static Panel_CampaignScript Instance;

    public GameObject Prefab_CampaignElement;
    public RectTransform RectTransform_CampaignContent;
    public LevelData_Serializable SelectedClearCampaignLevelToLoad;
    public Button Button_Continue;
    public Button Button_NewGame;
    public Sprite[] Sprites_Thumbnails;

    private List<CampaignElementScript> campaignLevelScripts = new List<CampaignElementScript>();


    private void Awake() {
        Instance = this;
    }

    void Start() {
        for (int i = 0; i < DataManager.Instance.AllLevelsData_Serializable.Count; i++) {
            LevelData_Serializable rankingElement_Serializable = DataManager.Instance.AllLevelsData_Serializable[i];
            GameObject listElement = Instantiate(Prefab_CampaignElement, RectTransform_CampaignContent);
            CampaignElementScript campaignElementScript = listElement.GetComponent<CampaignElementScript>();
            campaignElementScript.Initialize(rankingElement_Serializable);
            campaignLevelScripts.Add(listElement.GetComponent<CampaignElementScript>());

            Text[] texts = listElement.GetComponentsInChildren<Text>();
            texts[0].text = "Poziom: " + (i + 1).ToString();
        }

    }

    public void SelectCustomLevelToLoad(CampaignElementScript campaignLevelScript, LevelData_Serializable selectedLevelData_SerializableToLoad, bool isAvailableContinue) {
        Button_Continue.interactable = isAvailableContinue && selectedLevelData_SerializableToLoad.IsCompleted == false;
        Button_NewGame.interactable = true;
        SelectedClearCampaignLevelToLoad = selectedLevelData_SerializableToLoad;

        foreach (var element in campaignLevelScripts) {
            if (element != campaignLevelScript) {
                element.ChangeSelectionState(false);
            }
        }

        campaignLevelScript.ChangeSelectionState(true);
    }

    public void OnClick_Continue() {
        LevelData_Serializable savedLevelData_Serializable = DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Find(p => p.id == SelectedClearCampaignLevelToLoad.id);

        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = savedLevelData_Serializable;

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }

    public void OnClick_FromNew() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        levelDataHolder.LevelData_Serializable = SelectedClearCampaignLevelToLoad;

        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }
}
