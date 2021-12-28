using UnityEngine;
using UnityEngine.SceneManagement;


public class Panel_MenuScript : MonoBehaviour {
    public GameObject Panel_Stage2;
    public GameObject Panel_Stage3;


    void Start() {
        if (GameManager.Instance.CurrentStage == Stages.Stage2) {
            Panel_Stage2.SetActive(true);
        }
        else if (GameManager.Instance.CurrentStage == Stages.Stage3) {
            Panel_Stage3.SetActive(true);
        }
    }

    public void OnClick_Reload() {
        SceneManager.LoadScene((int)Scenes.LevelLoader);
    }

    public void OnClick_ReturnToMainMenu() {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }
}
