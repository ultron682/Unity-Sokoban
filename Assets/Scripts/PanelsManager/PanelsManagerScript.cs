using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelsManagerScript : MonoBehaviour {
    public GameObject Panel_MainMenu;
    public GameObject Panel_Campaign;
    public GameObject Panel_Community;


    private void OnEnable() {
        Panel_MainMenu.SetActive(true);
        Panel_Campaign.SetActive(false);
        Panel_Community.SetActive(false);
    }

    void Start() {
        //Button_LoadSavedGames.interactable = (DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Count > 0);
    }

    public void OnClick_Exit() {
        Application.Quit();
    }

    public void OnClick_Campaign() {
        Panel_MainMenu.SetActive(false);
        Panel_Campaign.SetActive(true);
    }

    public void OnClick_Community() {
        Panel_MainMenu.SetActive(false);
        Panel_Community.SetActive(true);
    }

    public void OnClick_Back() {
        Panel_MainMenu.SetActive(true);
        Panel_Campaign.SetActive(false);
        Panel_Community.SetActive(false);
    }
}
