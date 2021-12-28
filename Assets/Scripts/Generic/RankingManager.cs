using System;
using System.Collections.Generic;
using UnityEngine;


public class RankingManager : MonoBehaviour {
    public static RankingManager Instance;

    public Ranking_Serializable ranking_Serializable;

    void Awake() {
        Instance = this;
        //PlayerPrefs.DeleteKey("playerNick");
        LoadState();
    }

    public void LoadState() {
        if (PlayerPrefs.HasKey("rankingSerializable"))
            ranking_Serializable = JsonUtility.FromJson<Ranking_Serializable>(PlayerPrefs.GetString("rankingSerializable", string.Empty));
        else {
            ranking_Serializable.rankingElement_Serializables = new List<RankingElement_Serializable>() {
             new RankingElement_Serializable() { id = 0, playerName = "Scania12333", rankingValue=21 },
             new RankingElement_Serializable() { id = 1, playerName = "BluntDancer", rankingValue=106 },
             new RankingElement_Serializable() { id = 2, playerName = "Volume", rankingValue=9 },
             new RankingElement_Serializable() { id = 3, playerName = "Eyelor", rankingValue=89 },
             new RankingElement_Serializable() { id = 4, playerName = "EredinHyper", rankingValue=95 }
            };
        }

        if (ranking_Serializable.rankingElement_Serializables.Count > 0)
            ranking_Serializable.rankingElement_Serializables.Sort((RankingElement_Serializable x, RankingElement_Serializable y) => {
                if (x.rankingValue < y.rankingValue)
                    return 1;
                else if (x.rankingValue == y.rankingValue)
                    return 0;
                else
                    return -1;
            });
    }

    public void SaveState() {
        PlayerPrefs.SetString("rankingSerializable", JsonUtility.ToJson(ranking_Serializable));
    }
}

[Serializable]
public class Ranking_Serializable {
    public List<RankingElement_Serializable> rankingElement_Serializables = new List<RankingElement_Serializable>();
}

[Serializable]
public class RankingElement_Serializable {
    public int id;
    public string playerName;
    public int rankingValue;
}

