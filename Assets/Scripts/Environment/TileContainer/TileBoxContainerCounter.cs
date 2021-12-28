using System;
using UnityEngine;


public class TileBoxContainerCounter : MonoBehaviour {
    public static TileBoxContainerCounter Instance;

    public delegate void BoxPlaceChangeDelegate(int current, int max);
    public event BoxPlaceChangeDelegate On_BoxPlace_Change;

    public int BoxOnPlace {
        get {
            return _boxOnPlace;
        }
        set {
            _boxOnPlace = value;
            On_BoxPlace_Change?.Invoke(_boxOnPlace, MaxBoxPlaces);
        }
    }
    [NonSerialized]
    public int MaxBoxPlaces = 0; // ilosc miejsc na skrzynki

    private int _boxOnPlace = 0;


    void Awake() {
        Instance = this;
    }

    public void Refresh() {
        On_BoxPlace_Change?.Invoke(_boxOnPlace, MaxBoxPlaces);
    }
}
