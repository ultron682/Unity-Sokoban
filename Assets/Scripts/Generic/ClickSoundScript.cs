using UnityEngine;
using UnityEngine.UI;


public class ClickSoundScript : MonoBehaviour {
    public int i = 0;

    private void Awake() {
        if (i == 0)
            GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.PlayClickSoundNormal(); });
        else
            GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.PlayClickSoundSecond(); });
    }
}
