using UnityEngine;
using UnityEngine.UI;

namespace MainMenu {
    public class TogglesScript : MonoBehaviour {
        public Toggle Toggle_0;
        public Toggle Toggle_1;
        public Toggle Toggle_2;
        public Image Image_Icon;
        public Toggle Toggle_Sound;
        public Sprite Sprite_SoundOn;
        public Sprite Sprite_SoundOff;


        private void Start() {
            Toggle_Sound.isOn = PlayerPrefs.GetInt("settings_sound", 1) == 1;
        }

        public void SetToggleActive(int index) {
            if (index == 0) {
                Toggle_0.isOn = true;
            }
            else if (index == 1) {
                Toggle_1.isOn = true;
            }
            else if (index == 2) {
                Toggle_2.isOn = true;
            }
        }

        public void OnToggleValueChange0(bool isEnabled) {
            if (isEnabled) {
                PanelStagesScript.Instance.SwitchPanel(0);
                GameManager.Instance.CurrentStage = Stages.Stage1;
            }

            AudioManager.Instance.PlayClickSoundNormal();
        }

        public void OnToggleValueChange1(bool isEnabled) {
            if (isEnabled) {
                PanelStagesScript.Instance.SwitchPanel(1);
                GameManager.Instance.CurrentStage = Stages.Stage2;
            }

            AudioManager.Instance.PlayClickSoundNormal();
        }

        public void OnToggleValueChange2(bool isEnabled) {
            if (isEnabled) {
                PanelStagesScript.Instance.SwitchPanel(2);
                GameManager.Instance.CurrentStage = Stages.Stage3;
            }

            AudioManager.Instance.PlayClickSoundNormal();
        }

        public void OnClick_Sound(bool isEnabled) {
            AudioListener.volume = (isEnabled) ? 1 : 0;
            Image_Icon.sprite = (isEnabled) ? Sprite_SoundOn : Sprite_SoundOff;
            PlayerPrefs.SetInt("settings_sound", isEnabled ? 1 : 0);
        }
    }
}
