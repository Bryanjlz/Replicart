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
    SpriteTree spriteTree;

    [SerializeField]
    Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<Vector2, LinkedList<GameObject>>();
        // Load Level Group Blocks
        foreach (Transform gt in levelGroups.transform) {
            AddGroup(gt.gameObject);
        }
        LoadSprites();
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
        foreach (Transform ct in group.transform) {
            Vector2 pos = new Vector2(ct.position.x, ct.position.y);
            if (map.ContainsKey(pos)) {
                map[pos].AddLast(ct.gameObject);
            } else {
                LinkedList<GameObject> blockLayers = new LinkedList<GameObject>();
                blockLayers.AddLast(ct.gameObject);
                map.Add(pos, blockLayers);
            }
        }
    }

    public void RemoveGroup (GameObject group) {
        foreach (Transform ct in group.transform) {
            Vector2 pos = new Vector2(ct.position.x, ct.position.y);
            map[pos].Remove(ct.gameObject);
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
