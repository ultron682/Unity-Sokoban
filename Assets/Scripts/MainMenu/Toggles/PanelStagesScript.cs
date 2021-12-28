using UnityEngine;


namespace MainMenu {
    public class PanelStagesScript : MonoBehaviour {
        public static PanelStagesScript Instance;

        public GameObject[] Panels_Stages;


        private void Awake() {
            Instance = this;
        }

        private void Start() {
            int lastSelectedPanel = PlayerPrefs.GetInt("lastSelectedPanel", 0);
            SwitchPanel(lastSelectedPanel);
        }

        public void SwitchPanel(int panelId) {
            PlayerPrefs.SetInt("lastSelectedPanel", panelId);

            for (int i = 0; i < Panels_Stages.Length; i++) {
                if (i != panelId)
                    Panels_Stages[i].SetActive(false);
            }
            Panels_Stages[panelId].SetActive(true);
        }
    }
}