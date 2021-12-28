using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Creative {
    public class Panel_ToolsScript : MonoBehaviour {
        public static Panel_ToolsScript Instance;

        public Toggle Toggle_RemoveTile;
        public Toggle Toggle_TileType;
        public Toggle Toggle_Box;
        public Toggle Toggle_HeroSet;

        public int ExistBoxsInCreative = 0;


        private void Awake() {
            Instance = this;
        }
        public void OnClick_Back() {
            SceneManager.LoadScene(0);
        }

        public void OnClick_Save() {
            int countTileWithContainerForBox = CreativeDataManager.Instance.LevelData_Serializable.gridData.FindAll(p => p.tileID == 3).Count;

            if (ExistBoxsInCreative < countTileWithContainerForBox) {
                Canvas_Creative.Instance.Show_Message("Za ma³o skrzyñ dla docelowych platform");
            }
            else if (ExistBoxsInCreative > countTileWithContainerForBox) {
                Canvas_Creative.Instance.Show_Message("Za du¿o skrzyñ dla docelowych platform");
            }
            else if(ExistBoxsInCreative <= 0) {
                Canvas_Creative.Instance.Show_Message("Wymagana co najmniej jedna skrzynka!");
            }
            else {
                Canvas_Creative.Instance.Panel_Save.SetActive(true);
            }
        }

        public void OnSliderCameraChange(float value) {
            Camera.main.orthographicSize = value;
        }
    }
}