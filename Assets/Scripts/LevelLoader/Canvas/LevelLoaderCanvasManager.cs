using UnityEngine;
using UnityEngine.UI;


public class LevelLoaderCanvasManager : MonoBehaviour {
    public static LevelLoaderCanvasManager Instance;

    public GameObject Panel_MainContainer;
    public GameObject Panel_Menu;
    public GameObject Panel_Summary;
    public GameObject GameObject_EventSystem;
    public Text Text_BoxInRightPlace;
    public Text Text_MovesCount;
    public AudioClip AudioClip_ChestEvent;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        Panel_MainContainer.SetActive(true);
        Panel_Menu.SetActive(false);
        Panel_Summary.SetActive(false);

        TileBoxContainerCounter.Instance.On_BoxPlace_Change += OnBoxPlaceChange;
        LevelDataManager.Instance.OnMovesCountChange += (movesCount) => { Text_MovesCount.text = "Iloœæ ruchów: " + movesCount.ToString(); };
        TileBoxContainerCounter.Instance.Refresh();

        LevelLoaderManager.Instance.OnEndLoadingLevel += (s, e) => {
            Panel_Summary.GetComponent<Panel_SummaryScript>().Text_Title.text = $"Ukoñczono poziom: {LevelDataManager.Instance.levelData_Serializable.id}";
        };
    }

    private void OnBoxPlaceChange(int current, int target) {
        Text_BoxInRightPlace.text = current + "/" + target;
        if (current == target) {
            LevelDataManager.Instance.GameIsEnd = true;
            Panel_MainContainer.SetActive(false);
            Panel_Summary.SetActive(true);
            AudioManager.Instance.PlayOnce(AudioClip_ChestEvent);
        }
    }
}
