using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    public bool isSolid;
    [SerializeField]
    public Colour colour;
    [SerializeField]
    public int layer;
    [SerializeField]
    public SpriteRenderer spriteRenderer;

    BlockDragController bdc;
    Map map;

    bool[][] surrounds;
    string spriteType;

    private void Awake() {
        surrounds = new bool[3][];
        for (int i = 0; i < 3; i++) {
            surrounds[i] = new bool[3];
        }
        bdc = transform.parent.gameObject.GetComponent<BlockDragController>();
        map = GameObject.Find("Map").GetComponent<Map>();
        if (isSolid) {
            GetComponent<SpriteRenderer>().enabled = true;
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void CheckMapSurroundings () {
        spriteType = "";
        // Made the mistake of doing the check clockwise :(

        // Check top 3
        for (int xOff = -1; xOff < 2; xOff++) {
            surrounds[xOff + 1][2] = CheckPos(xOff, 1);
        }

        // Check middle right
        surrounds[2][1] = CheckPos(1, 0);

        // Check bottom 3
        for (int xOff = 1; xOff > -2; xOff--) {
            surrounds[xOff + 1][0] = CheckPos(xOff, -1);
        }

        // Check middle left
        surrounds[0][1] = CheckPos(-1, 0);

        //Update Sprite
        UpdateSprite();
    }

    public void UpdateSprite () {
        if (surrounds[0][1] && surrounds[0][2] && surrounds[1][2]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[1][2]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[1][2] && surrounds[2][2] && surrounds[2][1]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[2][1]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[2][1] && surrounds[2][0] && surrounds[1][0]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[1][0]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[1][0] && surrounds[0][0] && surrounds[0][1]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }
        if (surrounds[0][1]) {
            spriteType += "0";
        } else {
            spriteType += "1";
        }

        // Set new sprite
        spriteRenderer.sprite = map.spriteTrees[(int)colour].Get(spriteType);
    }

    private bool CheckPos (int xOff, int yOff) {
        Vector2 pos = new Vector2(transform.position.x + xOff, transform.position.y + yOff);
        if (map.map.ContainsKey(pos) && map.map[pos].Count > 0) {
            return true;
        }
        return false;
    }

    private void OnMouseDown() {
        bdc.PickUp();
    }
    private void OnMouseEnter() 
    {
        bdc.Highlight();
    }

    private void OnMouseExit()
    {
        bdc.LowLight();
    }

    private void OnMouseUp() {
        bdc.Release();
    }

}
