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

    private void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        if (isSolid) {
            GetComponent<SpriteRenderer>().enabled = true;
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void UpdateSpriteType () {
        spriteType = "";
        // Made the mistake of doing the check clockwise :(

        // Check top 3
        for (int xOff = -1; xOff < 2; xOff++) {
            spriteType += CheckPos(xOff, 1);
        }

        // Check middle right
        spriteType += CheckPos(1, 0);

        // Check bottom 3
        for (int xOff = 1; xOff < -2; xOff--) {
            spriteType += CheckPos(xOff, -1);
        }

        // Check middle left
        spriteType += CheckPos(-1, 0);

        // Get new sprite

    }

    private string CheckPos (int xOff, int yOff) {
        Vector2 pos = transform.position;
        if (map.map[new Vector2(pos.x + xOff, pos.y + yOff)].Count > 0) {
            return "1";
        }
        return "0";
    }


}
