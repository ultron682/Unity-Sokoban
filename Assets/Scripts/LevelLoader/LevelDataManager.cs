using System;
using UnityEngine;


public class LevelDataManager : MonoBehaviour {
    public static LevelDataManager Instance;

    public LevelData_Serializable levelData_Serializable;

    public bool GameIsEnd;
    public delegate void MovesCountChange(int count);
    public event MovesCountChange OnMovesCountChange;
    public int MovesCount {
        get {
            return _movesCount;
        }
        set {
            _movesCount = value;
            levelData_Serializable.Moves = value;
            OnMovesCountChange?.Invoke(_movesCount);
        }
    }

    private int _movesCount = 0;


    private void Awake() {
        Instance = this;
    }

    void Start() {
        LevelLoaderManager.Instance.OnEndLoadingLevel += Instance_OnEndLoadingLevel;
    }

    private void Instance_OnEndLoadingLevel(object sender, EventArgs e) {
        MovesCount = levelData_Serializable.Moves;
    }
}
