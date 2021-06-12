using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpectedResult : MonoBehaviour
{
    // This doesn't appear in editor btw
    public Dictionary<Vector2, Block> solution;
    [Header("Set in Editor")]
    public Map map;

    public void Start() {
        solution = new Dictionary<Vector2, Block>();

        foreach (Transform child in transform) {
            solution[new Vector2((int) child.position.x, (int) child.position.y)] = child.GetComponent<Block>();
            Color current = child.GetComponent<SpriteRenderer>().color;
            child.GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.1f);
            child.GetComponent<BoxCollider2D>().enabled = false;
            child.GetComponent<Block>().isSolid = false;
        }

        foreach (Vector2 pos in solution.Keys) {
            Block preview = solution[pos];
            bool[][] surrounds = new bool[3][];
            for (int i = 0; i < 3; i++) {
                surrounds[i] = new bool[3];
            }

            string spriteType = "";
            // Check top 3
            for (int xOff = -1; xOff < 2; xOff++) {
                surrounds[xOff + 1][2] = CheckPos(pos, xOff, 1);
            }

            // Check middle right
            surrounds[2][1] = CheckPos(pos, 1, 0);

            // Check bottom 3
            for (int xOff = 1; xOff > -2; xOff--) {
                surrounds[xOff + 1][0] = CheckPos(pos, xOff, -1);
            }

            // Check middle left
            surrounds[0][1] = CheckPos(pos, -1, 0);

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
            print(map);
            preview.GetComponent<SpriteRenderer>().sprite = map.spriteTrees[(int)preview.colour].Get(spriteType);
        }
    }

    private bool CheckPos (Vector2 pos, int xOff, int yOff) {
        if (solution.ContainsKey(new Vector2(pos.x + xOff, pos.y + yOff))) {
            return true;
        }
        return false;
    }



    public bool Match() {
        Dictionary<Vector2, bool> isSet = new Dictionary<Vector2, bool>();
        foreach(Vector2 key in solution.Keys) {
            isSet[key] = false;
        }
        foreach(Vector2 key in map.map.Keys) {
            if (solution.ContainsKey(key)) {
                if (map.map[key].Count > 0 && solution[key].colour == map.map[key][map.map[key].Count - 1].GetComponent<Block>().colour) {
                    isSet[key] = true;
                } else {
                    // If wrong colour, fail
                    return false;
                }
            } else {
                // Check if actually empty tile in map
                // If extra tile, fail match
                if (map.map[key].Count != 0) {
                    return false;
                }
            }
        }
        // If not full coverage, fail match
        foreach(Vector2 key in isSet.Keys) {
            if (!isSet[key]) {
                return false;
            }
        }
        return true;
    }
}