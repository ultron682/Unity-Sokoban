using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelsManagementTest {
    [UnityTest]
    public IEnumerator AllLevelsExists() {
        for (int i = 1; i <= 20; i++) {
            TextAsset levelSerialized = Resources.Load<TextAsset>($"level{i}");
            if (levelSerialized == null) {
                Assert.Fail();
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator SerializeTestLevel() {
        yield return null;
        LevelData_Serializable levelData_Serializable = JsonUtility.FromJson<LevelData_Serializable>(Resources.Load<TextAsset>("Level1").text);
        Assert.True(levelData_Serializable != null);
    }
}
