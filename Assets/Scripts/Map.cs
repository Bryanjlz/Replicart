using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    public Dictionary<Vector2, LinkedList<GameObject>> map;

    [SerializeField]
    GameObject levelGroups;
    [SerializeField]
    public SpriteTree spriteTree;

    [SerializeField]
    Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<Vector2, LinkedList<GameObject>>();

        // Load Sprites
        LoadSprites();

        // Load Level Group Blocks
        foreach (Transform gt in levelGroups.transform) {
            print(gt.gameObject);
            AddGroup(gt.gameObject);
        }
    }

    private void LoadSprites () {
        spriteTree = CreateTree(new SpriteTree(), 8);
        foreach (Sprite s in sprites) {
            spriteTree.Add(s, s.name.Substring(1));
        }
    }

    private SpriteTree CreateTree (SpriteTree tree, int depth) {
        if (depth == 0) {
            return tree;
        }
        tree.left = CreateTree(new SpriteTree(), depth - 1);
        tree.right = CreateTree(new SpriteTree(), depth - 1);
        return tree;
    }

    public void AddGroup (GameObject group) {
        int xMax = -100000;
        int yMax = -100000;
        int xMin = 100000;
        int yMin = 100000;
        foreach (Transform ct in group.transform) {
            if (ct.gameObject.GetComponent<Block>().isSolid) {
                float x = ct.position.x;
                float y = ct.position.y;
                Vector2 pos = new Vector2(x, y);
                if (y < yMin) {
                    yMin = (int)y;
                }
                if (y > yMax) {
                    yMax = (int)y;
                }
                if (x < xMin) {
                    xMin = (int)x;
                }
                if (x > xMax) {
                    xMax = (int)x;
                }

                if (map.ContainsKey(pos)) {
                    map[pos].AddLast(ct.gameObject);
                } else {
                    LinkedList<GameObject> blockLayers = new LinkedList<GameObject>();
                    blockLayers.AddLast(ct.gameObject);
                    map.Add(pos, blockLayers);
                }
            }
        }
        UpdateSprites(xMin, yMin, xMax, yMax);
    }

    public void RemoveGroup (GameObject group) {
        foreach (Transform ct in group.transform) {
            if (ct.gameObject.GetComponent<Block>().isSolid) {
                Vector2 pos = new Vector2(ct.position.x, ct.position.y);
                map[pos].Remove(ct.gameObject);
            }
        }
    }

    public void UpdateSprites (int xMin, int yMin, int xMax, int yMax) {
        for (int x = xMin; x <= xMax; x++) {
            for (int y = yMin; y <= yMax; y++) {
                Vector2 pos = new Vector2(x, y);
                if (map.ContainsKey(pos) && map[pos].Count > 0) {
                    map[pos].Last.Value.GetComponent<Block>().UpdateSprite();
                }
            }
        }
    }

    public void Render () {
        foreach (Vector2 pos in map.Keys) {
            GameObject block = map[pos].Last.Value;
            if (block.GetComponent<Block>().isSolid) {
                block.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
