using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    public Dictionary<Vector2, List<GameObject>> map;

    [SerializeField]
    GameObject levelGroups;
    [SerializeField]
    public List<SpriteTree> spriteTrees;

    [SerializeField]
    Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<Vector2, List<GameObject>>();

        // Load Sprites
        LoadSprites();

        // Load Level Group Blocks
        foreach (Transform gt in levelGroups.transform) {
            AddGroup(gt.gameObject);
            foreach(Transform bt in gt) {
                bt.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void LoadSprites () {
        spriteTrees = new List<SpriteTree>();
        for (int i = 0; i < 4; i++) {
            spriteTrees.Add(CreateTree(new SpriteTree(), 8));
        }

        foreach (Sprite s in sprites) {
            if (s.name.Substring(0, 1).Equals("b")) {
                spriteTrees[(int)Colour.BLUE].Add(s, s.name.Substring(1));
            } else if (s.name.Substring(0, 1).Equals("g")) {
                spriteTrees[(int)Colour.GREEN].Add(s, s.name.Substring(1));
            } else if (s.name.Substring(0, 1).Equals("r")) {
                spriteTrees[(int)Colour.RED].Add(s, s.name.Substring(1));
            } else {
                spriteTrees[(int)Colour.YELLOW].Add(s, s.name.Substring(1));
            }
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
                    map[pos].Add(ct.gameObject);
                    ct.gameObject.GetComponent<Block>().layer = map[pos].Count - 1;
                } else {
                    List<GameObject> blockLayers = new List<GameObject>();
                    blockLayers.Add(ct.gameObject);
                    map.Add(pos, blockLayers);
                }
            }
        }
        UpdateSprites(xMin - 1, yMin - 1, xMax + 1, yMax + 1);
    }

    public void RemoveGroup (GameObject group) {
        int xMax = -100000;
        int yMax = -100000;
        int xMin = 100000;
        int yMin = 100000;
        foreach (Transform ct in group.transform) {
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
            if (ct.gameObject.GetComponent<Block>().isSolid) {
                map[pos].Remove(ct.gameObject);
            }
        }
        UpdateSprites(xMin - 1, yMin - 1, xMax + 1, yMax + 1);
    }

    public void UpdateSprites (int xMin, int yMin, int xMax, int yMax) {
        for (int x = xMin; x <= xMax; x++) {
            for (int y = yMin; y <= yMax; y++) {
                Vector2 pos = new Vector2(x, y);
                if (map.ContainsKey(pos) && map[pos].Count > 0) {
                    for (int i = 0; i < map[pos].Count; i++) {
                        Block block = map[pos][i].GetComponent<Block>();
                        SpriteRenderer sr = block.spriteRenderer;

                        block.layer = i;
                        block.spriteRenderer.sortingOrder = i;
                        sr.enabled = false;
                        if (i == map[pos].Count - 1) {
                            block.UpdateSprite();
                            sr.enabled = true;
                        }
                    }
                }
            }
        }
    }

    public void Render () {
        foreach (Vector2 pos in map.Keys) {
            GameObject block = map[pos][map[pos].Count - 1];
            if (block.GetComponent<Block>().isSolid) {
                block.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
