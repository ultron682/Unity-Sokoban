using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MainGameMechanicsTest {
    GameObject MainManagers_Instance;
    GameObject SceneLevelLoader_Instance;

    [OneTimeSetUp]
    public void Setup() {
        MainManagers_Instance = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GlobalManagers"));
        SceneLevelLoader_Instance = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tests/LevelLoaderTests"));
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsGameMangerScript() {
        Assert.That(MainManagers_Instance.GetComponent<GameManager>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsDataMangerScript() {
        Assert.That(MainManagers_Instance.GetComponent<DataManager>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsCreativeLevelsManagerScript() {
        Assert.That(MainManagers_Instance.GetComponent<CreativeLevelsManager>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsRankingManagerScript() {
        Assert.That(MainManagers_Instance.GetComponent<RankingManager>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsAudioManagerScript() {
        Assert.That(MainManagers_Instance.GetComponent<RankingManager>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator GlobalManagerContainsAudioSourceComponent() {
        Assert.That(MainManagers_Instance.GetComponent<AudioSource>());
        yield return null;
    }

    [UnityTest]
    public IEnumerator AudioSourceContainsMainAudioClip() {
        AudioClip audioClip = MainManagers_Instance.GetComponent<AudioSource>().clip;

        Assert.True(audioClip.name == "soundtrack1");
        yield return null;
    }


    [UnityTest]
    public IEnumerator CheckAvailabilityMainObjects() {
        Assert.IsTrue(SceneLevelLoader_Instance.transform.GetChild(0).name == "LevelLoader_Managers"
            && SceneLevelLoader_Instance.transform.GetChild(1).name == "Environment"
            && SceneLevelLoader_Instance.transform.GetChild(2).name == "Canvas"
            && SceneLevelLoader_Instance.transform.GetChild(3).name == "EventSystem"
            && SceneLevelLoader_Instance.transform.GetChild(4).name == "Directional Light"
            );
        yield return null;
    }

    [UnityTest]
    public IEnumerator CanvasHasEnabledOnlyMainContainer() {
        Assert.IsTrue(SceneLevelLoader_Instance.transform.GetChild(2).GetChild(0).gameObject.activeSelf
            && SceneLevelLoader_Instance.transform.GetChild(2).GetChild(1).gameObject.activeSelf == false
            && SceneLevelLoader_Instance.transform.GetChild(2).GetChild(2).gameObject.activeSelf == false);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EventSystemIsReadyToCheck() {
        Assert.IsTrue(SceneLevelLoader_Instance.transform.Find("EventSystem") != null) ;
        yield return null;
    }

    [UnityTest]
    public IEnumerator CanvasHasCorrectlyImageForGuideKeys() {
        yield return null;
        GameObject gameObject = GameObject.Find("Image_guideKeys");

        Assert.IsTrue(gameObject != null && gameObject.GetComponent<Image>().sprite.name == "guide-keys");
    }
}
