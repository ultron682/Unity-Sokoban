using UnityEngine;


namespace Creative {
    public class CreativeDataManager : MonoBehaviour {
        public static CreativeDataManager Instance;

        public LevelData_Serializable LevelData_Serializable;


        void Awake() {
            Instance = this;
        }

        private void Start() {
            LevelData_Serializable.IsCustomLevel = true;
        }
    }
}