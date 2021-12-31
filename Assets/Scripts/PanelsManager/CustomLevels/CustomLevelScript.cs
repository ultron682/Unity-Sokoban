using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CustomLevelScript : MonoBehaviour, IPointerClickHandler {
    public LevelData_Serializable levelData_Serializable;
    public LevelData_Serializable savedLevelData_Serializable = new LevelData_Serializable() { id = -1, IsCustomLevel = true };


    public void OnPointerClick(PointerEventData eventData) {
        Panel_CommunityScript.Instance.SelectCustomLevelToLoad(this, levelData_Serializable, savedLevelData_Serializable);
    }

    public void ChangeSelectionState(bool isSelected) {
        Color SelectedColor = new Color() {
            a = 0.5F,
            r = 0.8588236F,
            g = 0.5450981F,
            b = 0.08627451F
        };
        Color OtherColor = new Color() {
            a = 0.1F,
            r = 1,
            g = 1,
            b = 1
        };
        GetComponent<Image>().color = isSelected ? SelectedColor : OtherColor;
    }
}
