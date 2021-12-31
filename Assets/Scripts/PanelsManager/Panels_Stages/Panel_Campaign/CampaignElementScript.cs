using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CampaignElementScript : MonoBehaviour, IPointerClickHandler {
    public Image Image_SavedLevel;
    public Text Text_Points;
    public Image Image_LevelThumbnail;
    public Sprite Sprite_LevelCompleted;

    LevelData_Serializable campaignElement_Serializable;
    bool isAvailableContinue;
    LevelData_Serializable savedLevelData = null;

    public void Initialize(LevelData_Serializable campaignElement_Serializable) {
        this.campaignElement_Serializable = campaignElement_Serializable;

        isAvailableContinue = DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Exists(p => p.id == campaignElement_Serializable.id);
        savedLevelData = DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Find(p => p.id == campaignElement_Serializable.id);

        Image_SavedLevel.color = isAvailableContinue ? new Color(0.8588236F, 0.5450981F, 0.08627451F) : new Color(0.5F, 0.5F, 0.5F, 0.5F);

        Text_Points.text = (savedLevelData != null && savedLevelData.IsCompleted) ? $"Zdobyte punkty: {savedLevelData.Score}" : string.Empty;

        if (savedLevelData != null && savedLevelData.IsCompleted)
            Image_SavedLevel.sprite = Sprite_LevelCompleted;
        Image_LevelThumbnail.sprite = Panel_CampaignScript.Instance.Sprites_Thumbnails[campaignElement_Serializable.id - 1];
    }

    public void OnPointerClick(PointerEventData eventData) {
        Panel_CampaignScript.Instance.SelectCustomLevelToLoad(this, campaignElement_Serializable, savedLevelData != null && savedLevelData.IsCompleted == false);
    }

    public void ChangeSelectionState(bool isSelected) {
        Color SelectedColor = new Color() {
            a = 0.9F,
            r = 0.8588236F,
            g = 0.5450981F,
            b = 0.08627451F
        };
        Color OtherColor = new Color() {
            a = 0.2F,
            r = 1,
            g = 1,
            b = 1
        };
        GetComponentInChildren<Image>().color = isSelected ? SelectedColor : OtherColor;
    }
}
