using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class BoxTest {
    GameObject box;

    [OneTimeSetUp]
    public void Setup() {
        box = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Prefab_Box"));
    }

    [UnityTest]
    public IEnumerator BoxHasCorrectScriptForLevelLoaderScene() {
        yield return null;
        Assert.True(box.GetComponent<BoxScript>() != null);
    }

    [UnityTest]
    public IEnumerator BoxHasEmptyTargetTransformForLevelLoaderScene() {
        yield return null;
        Assert.True(box.GetComponent<BoxScript>().Transform_TargetTile == null);
    }
}
