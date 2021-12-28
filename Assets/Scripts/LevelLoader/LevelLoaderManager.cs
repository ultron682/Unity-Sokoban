using System;
using System.Collections;
using UnityEngine;


public class LevelLoaderManager : MonoBehaviour {
    public static LevelLoaderManager Instance;

    public GameObject Prefab_GlobalManagers;
    public event EventHandler OnEndLoadingLevel;

    private void Awake() {
        Instance = this;

        if (FindObjectOfType<GameManager>() == null) {
            Instantiate(Prefab_GlobalManagers);
        }
    }

    public void Start() {
        LevelDataHolder levelDataHolder = FindObjectOfType<LevelDataHolder>();
        if (levelDataHolder != null) {
            LevelDataManager.Instance.levelData_Serializable = levelDataHolder.Clone();
        }
        else {
            LevelDataManager.Instance.levelData_Serializable = JsonUtility.FromJson<LevelData_Serializable>(Resources.Load<TextAsset>("Level1").text);
        }

        LevelLoader.Instance.LoadLevel(LevelDataManager.Instance.levelData_Serializable);
        StartCoroutine(Delay_Start());
    }

    IEnumerator Delay_Start() {
        yield return new WaitForEndOfFrame();
        OnEndLoadingLevel?.Invoke(null, null);
    }

}
