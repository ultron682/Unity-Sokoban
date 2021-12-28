using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public Stages CurrentStage;


    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        if (arg0.buildIndex == (int)Scenes.Creative) {
            CurrentStage = Stages.Stage3;
        }
    }
}


public enum Stages {
    Stage1,
    Stage2,
    Stage3
}