using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HeroTest {
    GameObject hero;

    [OneTimeSetUp]
    public void Setup() {
        hero = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/HeroController"));
    }

    [UnityTest]
    public IEnumerator HeroContainsHeroControllerScript() {
        HeroController heroController = hero.GetComponent<HeroController>();
        yield return null;

        Assert.IsTrue(heroController != null);
    }

    [UnityTest]
    public IEnumerator HerosFieldsHaveCorrectModifiers() {
        HeroController heroController = hero.GetComponent<HeroController>();
        yield return null;

        FieldInfo[] fields = heroController.GetType().GetFields();
        if (Array.Find(fields, p => p.Name == "AudioClip_Moving").IsPublic) {
            Assert.Pass();
        }
        else {
            Assert.Fail();
        }
    }

    [UnityTest]
    public IEnumerator AnimatorFieldIsnotEmpty() {
        HeroController heroController = hero.GetComponent<HeroController>();

        yield return null;

        Assert.That(heroController.Animator_Hero);
    }

    [UnityTest]
    public IEnumerator MovementKeysDownTest() {
        HeroController heroController = hero.GetComponent<HeroController>();
        yield return null;

        MethodInfo methodInfoKeyDown = heroController.GetType().GetMethod("IsSomeoneKeyDown", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        bool keysDown = (bool)methodInfoKeyDown.Invoke(heroController, null);

        Assert.IsFalse(keysDown);
    }

    [UnityTest]
    public IEnumerator HeroHasCorrectBackgroundSizeForBiggerScreens() {
        SpriteRenderer[] spriteRenderer = hero.GetComponentsInChildren<SpriteRenderer>();
        yield return null;

        Assert.IsTrue(spriteRenderer[1].gameObject.transform.localScale == new Vector3(2, 2, 1));
    }
}

