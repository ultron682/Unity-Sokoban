using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public AudioSource AudioSource;
    public AudioSource AudioSourceEffects;
    public AudioClip AudioClip_Click;
    public AudioClip AudioClip_ClickSecond;
    public AudioClip AudioClip_BgMusic1;
    public AudioClip AudioClip_BgMusic2;
    public AudioClip AudioClip_BgMusic3;


    private void Awake() {
        Instance = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        if (arg0.buildIndex == (int)Scenes.MainMenu) {
            AudioSource.clip = AudioClip_BgMusic1;
        }
        else if (arg0.buildIndex == (int)Scenes.Creative) {
            AudioSource.clip = AudioClip_BgMusic3;
        }
        else {
            AudioSource.clip = AudioClip_BgMusic2;
        }
        AudioSource.Play();
    }

    public void ChangeVolume(float volume) {
        AudioSource.volume = volume;
    }

    public void PlayOnce(AudioClip audioClip) {
        AudioSourceEffects.PlayOneShot(audioClip);
    }

    public void PlayClickSoundNormal() {
        AudioSourceEffects.PlayOneShot(AudioClip_Click);
    }

    public void PlayClickSoundSecond() {
        AudioSourceEffects.PlayOneShot(AudioClip_ClickSecond);
    }
}
