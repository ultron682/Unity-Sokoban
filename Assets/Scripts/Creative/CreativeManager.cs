using UnityEngine;


namespace Creative {
    public class CreativeManager : MonoBehaviour {
        public GameObject Prefab_GlobalManagers;


        void Awake() {
            if (FindObjectOfType<GameManager>() == null) {
                Instantiate(Prefab_GlobalManagers);
            }
        }
    }
}