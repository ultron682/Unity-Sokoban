using UnityEngine;


namespace Creative {
    public class HeroCreativeScript : MonoBehaviour {
        public void ChangeContainer(TileScript tileScript) {
            transform.SetParent(tileScript.transform);
            transform.position = tileScript.transform.position;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - (int)tileScript.transform.position.y;
        }
    }
}