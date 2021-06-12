using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    public bool isSolid;
    [SerializeField]
    public int layer;
    [SerializeField]
    public SpriteRenderer spriteRenderer;

    Map map;

    string spriteType;

    private void Awake() {
        map = GameObject.Find("Map").GetComponent<Map>();
        print(map);
        if (isSolid) {
            GetComponent<SpriteRenderer>().enabled = true;
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void UpdateSprite () {
        spriteType = "";
        // Made the mistake of doing the check clockwise :(

        // Check top 3
        for (int xOff = -1; xOff < 2; xOff++) {
            spriteType += CheckPos(xOff, 1);
        }

        // Check middle right
        spriteType += CheckPos(1, 0);

        // Check bottom 3
        for (int xOff = 1; xOff > -2; xOff--) {
            spriteType += CheckPos(xOff, -1);
        }

        // Check middle left
        spriteType += CheckPos(-1, 0);

        print(this +" " +  spriteType);
        // Set new sprite
        spriteRenderer.sprite = map.spriteTree.Get(spriteType);
    }

    private string CheckPos (int xOff, int yOff) {
        Vector2 pos = new Vector2(transform.position.x + xOff, transform.position.y + yOff);
        if (map.map.ContainsKey(pos) && map.map[pos].Count > 0) {
            return "0";
        }
        return "1";
    }


}
