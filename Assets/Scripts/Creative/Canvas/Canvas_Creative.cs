using UnityEngine;
using UnityEngine.UI;

namespace Creative {
    public class Canvas_Creative : MonoBehaviour {
        public static Canvas_Creative Instance;

        public GameObject Panel_InitializeGrid;
        public GameObject Panel_Tools;
        public GameObject Panel_Save;
        public GameObject Panel_Message;


        void Awake() {
            Instance = this;
        }

        private void Start() {
            Panel_InitializeGrid.SetActive(true);
            Panel_Tools.SetActive(false);
            Panel_Save.SetActive(false);
            Panel_Message.SetActive(false);
        }

        public void Show_Message(string message) {
            Panel_Message.SetActive(true);
            Panel_Message.GetComponentsInChildren<Text>()[0].text = message;
        }
    }
}