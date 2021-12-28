using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainPanelScript : MonoBehaviour {
    public GameObject MainContainer;
    public GameObject Panel_Campaign;
    public GameObject Panel_SetNick;
    public GameObject Rank_List;
    //public Button Button_LoadSavedGames;

    public GameObject Prefab_RankingElement;
    public RectTransform RectTransform_RankingContent;
    public InputField InputField_Nickname;


    private void OnEnable() {
        MainContainer.SetActive(true);
        Panel_Campaign.SetActive(false);
        Panel_SetNick.SetActive(false);
    }

    void Start() {
        RankingElement_Serializable rankingElement_Serializable = RankingManager.Instance.ranking_Serializable.rankingElement_Serializables.Find(p => p.id == 1954);
        if (rankingElement_Serializable != null) {
            List<LevelData_Serializable> completedLevelsStage2 = DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.FindAll(p => p.IsCompleted);
            int score = 0;
            foreach (LevelData_Serializable levelData_Serializable in completedLevelsStage2) {
                score += levelData_Serializable.Score;
            }

            rankingElement_Serializable.rankingValue = score;
        }

        LoadRanking();

        //Button_LoadSavedGames.interactable = (DataManager.Instance.Stage2_AllSavedLevelsData_Serializable.savedLevels_Serializables.Count > 0);
    }

    public void OnClick_Exit() {
        Application.Quit();
    }

    public void OnClick_Play() {
        MainContainer.SetActive(false);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(false);

        if (PlayerPrefs.GetString("playerNick", string.Empty) == string.Empty) {
            Panel_SetNick.SetActive(true);
        }
        else {
            Panel_Campaign.SetActive(true);
        }
    }

    public void OnClick_Back() {
        MainContainer.SetActive(true);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(true);
    }

    public void OnClick_Load() {
        MainContainer.SetActive(false);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(false);

    }
    public void OnClick_Ranking() {
        MainContainer.SetActive(false);
        MainMenuCanvas.Instance.GameObject_Title.SetActive(false);

        Rank_List.SetActive(true);
    }


    public void OnClick_SetNick() { // wykonywane raz przy pierwszym zatwierdzaniu nick'u
        if (InputField_Nickname.text != string.Empty) {
            MainContainer.SetActive(false);
            PlayerManager.Instance.PlayerNick = InputField_Nickname.text;
            PlayerPrefs.SetString("playerNick", PlayerManager.Instance.PlayerNick);
            PlayerPrefs.Save();
            Panel_SetNick.SetActive(false);
            Panel_Campaign.SetActive(true);
            RankingManager.Instance.ranking_Serializable.rankingElement_Serializables.Add(new RankingElement_Serializable() { id = 1954, playerName = PlayerManager.Instance.PlayerNick, rankingValue = 0 });
            RankingManager.Instance.SaveState();
        }
    }

    void LoadRanking() {
        if (RankingManager.Instance.ranking_Serializable.rankingElement_Serializables.Count > 0)
            RankingManager.Instance.ranking_Serializable.rankingElement_Serializables.Sort((RankingElement_Serializable x, RankingElement_Serializable y) => {
                if (x.rankingValue < y.rankingValue)
                    return 1;
                else if (x.rankingValue == y.rankingValue)
                    return 0;
                else
                    return -1;
            });

        for (int i = 0; i < RankingManager.Instance.ranking_Serializable.rankingElement_Serializables.Count; i++) {
            RankingElement_Serializable rankingElement_Serializable = RankingManager.Instance.ranking_Serializable.rankingElement_Serializables[i];
            GameObject listElement = Instantiate(Prefab_RankingElement, RectTransform_RankingContent);

            Text[] texts = listElement.GetComponentsInChildren<Text>();
            texts[0].text = (i + 1).ToString();
            texts[1].text = rankingElement_Serializable.playerName.ToString();
            texts[2].text = rankingElement_Serializable.rankingValue.ToString();
        }
    }
}
