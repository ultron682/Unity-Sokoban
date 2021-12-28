using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Creative {
    public class Panel_InitializeGrid : MonoBehaviour {
        public Slider Slider_Grid_X;
        public Slider Slider_Grid_Y;
        public Text Text_X;
        public Text Text_Y;


        private void Start() {
            OnSliderXChange(3);
            OnSliderYChange(3);
        }

        public void Submit() {
            CreativeGridManager.Instance.GenerateGrid((int)Slider_Grid_X.value, (int)Slider_Grid_Y.value);
            CreativeDataManager.Instance.LevelData_Serializable.grid_X = (int)Slider_Grid_X.value;
            CreativeDataManager.Instance.LevelData_Serializable.grid_Y = (int)Slider_Grid_Y.value;
            CreativeDataManager.Instance.LevelData_Serializable.IsCustomLevel = true;
            CreativeDataManager.Instance.LevelData_Serializable.id = Random.Range(1, 2000);

            Canvas_Creative.Instance.Panel_Tools.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSliderXChange(float count) {
            Text_X.text = GetStartString((int)count);
        }

        public void OnSliderYChange(float count) {
            Text_Y.text = GetStartString((int)count);
        }

        string GetStartString(int count) {
            if (count <= 4)
                return count + " kafelki";
            else
                return count + " kafelków";
        }
        public void OnClick_Back() {
            SceneManager.LoadScene(0);
        }
    }
}